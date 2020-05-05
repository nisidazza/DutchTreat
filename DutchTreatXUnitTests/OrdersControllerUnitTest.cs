using AutoMapper;
using DutchTreat.Controllers;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using DutchTreatXUnitTests.FakeContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DutchTreatXUnitTests
{
    public class OrdersControllerUnitTest : IDisposable
    {
        private readonly OrdersController _sut;
        private readonly List<Order> _orders;
        private readonly Mock<IDutchRepository> _mockRepo;
        private readonly Mock<IMyLogger<OrdersController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly List<OrderViewModel> _orderViewModel;
        private const string _currentUserName = "Pippo";

        public OrdersControllerUnitTest()
        {
            _mockLogger = new Mock<IMyLogger<OrdersController>>();
            _mockUserRepo = new Mock<IUserRepository>();

            _orders = new List<Order>();
            _orders.Add(new Order
            {
                Id = 1,
                OrderDate = new DateTime(2020, 1, 1),
                OrderNumber = "abc123",
                Items = new List<OrderItem>()
            });

            _mockRepo = new Mock<IDutchRepository>();
            _mockRepo.Setup(r => r.GetOrderById(It.IsAny<string>(), It.IsAny<int>()))
                .Returns<string, int>((username, id) => _orders.FirstOrDefault(order => order.Id == id));

            _orderViewModel = new List<OrderViewModel>();
            _orderViewModel.Add(new OrderViewModel
            {
                OrderId = 1,
                OrderDate = new DateTime(2020, 1, 1),
                OrderNumber = "abc123"
            });

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_orders))
                .Returns(_orderViewModel);

            var httpContextBuilder = new FakeHttpContextBuilder();
            httpContextBuilder.Append(new ConfigureFakeIdentity(_currentUserName));

            _sut = new OrdersController(_mockRepo.Object, _mockLogger.Object, _mockMapper.Object, _mockUserRepo.Object);
            _sut.ApplyFakeHttpContext(httpContextBuilder);
        }

        public void Dispose()
        {
        }

        [Fact]
        public void GetOrder_OrderDoesExist_ReturnOk()
        {
            var orderId = 1;
            var result = _sut.Get(orderId);
            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }

        [Fact]
        public void GetOrder_OrderDoesExist_MapperHasBeenCalledOnce()
        {
            var orderId = 1;
            var result = _sut.Get(orderId);
            _mockMapper.Verify(m => m.Map<Order, OrderViewModel>(_orders[0]),
            Times.Once()
            );
        }

        [Fact]
        public void GetOrder_OrderDoesNotExist_ReturnNotFound()
        {
            var orderId = -1;
            var result = _sut.Get(orderId);
            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }

        [Fact]
        public void GetOrder_RepositoryThrowsException_LogErrorIsCalled()
        {
            //simulate DB is not responding
            _mockRepo.Setup(r => r.GetOrderById(It.IsAny<string>(), It.IsAny<int>()))
                .Throws<Exception>();

            var result = _sut.Get(1);

            _mockLogger.Verify(x =>
                x.LogError(It.IsAny<string>()),
                Times.Once()
            );
        }

        [Fact]
        public void GetOrder_RepositoryThrowsException_ReturnBadRequest()
        {
            //simulate DB is not responding
            _mockRepo.Setup(r => r.GetOrderById(It.IsAny<string>(), It.IsAny<int>()))
                .Throws<Exception>();

            var result = _sut.Get(1);

        }
    }
}
