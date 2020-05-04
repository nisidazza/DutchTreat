using AutoMapper;
using DutchTreat.Controllers;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using DutchTreatXUnitTests.FakeContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DutchTreatXUnitTests
{
    public class OrderItemsControllerUnitTest : IDisposable
    {
        private readonly OrderItemsController _sut;
        private readonly Mock<IDutchRepository> _mockRepo;
        private readonly Mock<ILogger<OrderItemsController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly List<Order> _orders;
        private readonly List<OrderItemViewModel> _orderItemsViewModel;
        private const string _currentUserName = "Pippo";
        private readonly OrderItem _orderItem22;

        public OrderItemsControllerUnitTest()
        {

            _mockLogger = new Mock<ILogger<OrderItemsController>>();

            _orders = new List<Order>();
            _orderItem22 = new OrderItem
            {
                Id = 22,
                Quantity = 17,
                UnitPrice = 12
            };
            _orders.Add(new Order
            {
                Id = 1,
                Items = new List<OrderItem>(new OrderItem[]{
                    new OrderItem {
                    Id = 20,
                    Quantity = 10,
                    UnitPrice = 30
                    },
                    new OrderItem {
                    Id = 21,
                    Quantity = 15,
                    UnitPrice = 45
                    },
                    _orderItem22
                }),

                OrderDate = DateTime.Now,
                OrderNumber = "12345",
                User = new StoreUser()
            });


            _mockRepo = new Mock<IDutchRepository>();
            _mockRepo.Setup(r => r.GetOrderById(It.IsAny<string>(), It.IsAny<int>()))
                .Returns<string, int>((username, id) =>
                     _orders.FirstOrDefault(order => order.Id == id)
                );


            _orderItemsViewModel = new List<OrderItemViewModel>();
            _orderItemsViewModel.Add(new OrderItemViewModel
            {
                Id = 123,
                Quantity = 10,
                UnitPrice = 25
            });

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(_orders[0].Items))
                .Returns(_orderItemsViewModel);

            var httpContextBuilder = new FakeHttpContextBuilder();
            httpContextBuilder.Append(new ConfigureFakeIdentity(_currentUserName));

            _sut = new OrderItemsController(_mockRepo.Object, _mockLogger.Object, _mockMapper.Object);
            _sut.ApplyFakeHttpContext(httpContextBuilder);
        }

        public void Dispose()
        {
        }

        [Fact]
        public void GetOrder_OrderDoesNotExist_ReturnNotFound()
        {
            var orderId = -1;
            var result = _sut.Get(orderId);
            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }

        [Fact]
        public void GetOrder_OrderExists_ReturnOk()
        {
            var orderId = 1;
            var result = _sut.Get(orderId);
            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }

        [Fact]
        public void GetOrder_OrderExists_ReturnOkWithCorrectValue()
        {
            var orderId = 1;
            var result = _sut.Get(orderId);
            var okResult = (OkObjectResult)result;
            Assert.Equal(_orderItemsViewModel, okResult.Value);
        }

        [Fact]
        public void GetOrder_OrderExists_MapperHasBeenCalledOnce()
        {
            var orderId = 1;
            var result = _sut.Get(orderId);
            _mockMapper.Verify(
                mock => mock.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(_orders[0].Items),
                Times.Once()
                );
        }


        [Fact]
        public void GetOrderItem_OrderAndItemExist_ReturnOk()
        {
            var orderId = 1;
            var itemId = 22;
            var result = _sut.Get(orderId, itemId);
            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }

        [Fact]
        public void GetOrderItem_OrderAndItemExist_MapperHasBeenCalledOnce()
        {
            var orderId = 1;
            var itemId = 22;
            var result = _sut.Get(orderId, itemId);
            _mockMapper.Verify(
                mock => mock.Map<OrderItem, OrderItemViewModel>(_orderItem22),
                Times.Once()
                );
        }

        [Fact]
        public void GetOrderItem_OrderDoesNotExist_ReturnNotFound()
        {
            var orderId = -1;
            var itemId = 22;
            var result = _sut.Get(orderId, itemId);
            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }

        [Fact]
        public void GetOrderItem_OrderDoesNotExist_MapperNeverCalled()
        {
            var orderId = -1;
            var itemId = 22;
            var result = _sut.Get(orderId, itemId);
            _mockMapper.Verify(
                mock => mock.Map<OrderItem, OrderItemViewModel>(_orderItem22),

                Times.Never()
                );
        }

        [Fact]
        public void GetOrderItem_ItemDoesNotExist_ReturnNotFound()
        {
            var orderId = 1;
            var itemId = 5;
            var result = _sut.Get(orderId, itemId);
            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }



    }
}
