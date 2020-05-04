using DutchTreat.Controllers;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Moq;
using System;
using Xunit;

namespace DutchTreatXUnitTests
{
    public class AppControllerUnitTest : IDisposable
    {
        private readonly AppController _sut;
        private readonly Mock<IMailService> _mockMailService;
        private readonly Mock<IDutchRepository> _mockRepo;
        private readonly ContactViewModel _model;

        public AppControllerUnitTest()
        {
            _mockRepo = new Mock<IDutchRepository>();
            _mockMailService = new Mock<IMailService>();
            _sut = new AppController(_mockMailService.Object, _mockRepo.Object);

            //Initialize model to a valid model
            _model = new ContactViewModel
            {
                Name = "Abcde",
                Email = "mail@mail.mail",
                Subject = "subject",
                Message = "MaxLength250"
            };
        }

        public void Dispose()
        {
        }

        [Fact]
        public void Contact_ModelIsValid_VerifyUserMessage()
        {
            _sut.BindViewModel(_model);
            var result = _sut.Contact(_model);
            var resultAsViewResult = (Microsoft.AspNetCore.Mvc.ViewResult)result;
            Assert.Equal("Mail Sent", resultAsViewResult.ViewData["UserMessage"]);
        }

        [Fact]
        public void Contact_ModelIsValid_VerifyEmailIsSent()
        {
            _sut.BindViewModel(_model);
            var result = _sut.Contact(_model);
            var resultAsViewResult = (Microsoft.AspNetCore.Mvc.ViewResult)result;
            _mockMailService.Verify(mock => mock.SendMessage(
                "ross@smith.com", _model.Subject, It.IsAny<string>()
                ),
                Times.Once()
            );
        }

        [Fact]
        public void Contact_NameIsTooShort_VerifyEmailIsNotSent()
        {
            _model.Name = "A";
            _sut.BindViewModel(_model);
            var result = _sut.Contact(_model);
            var resultAsViewResult = (Microsoft.AspNetCore.Mvc.ViewResult)result;
            _mockMailService.Verify(mock => mock.SendMessage(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                ),
                Times.Never()
            );
        }

        [Fact]
        public void Index_ActionReturnsViewForIndex()
        {
            var result = _sut.Index();
            Assert.NotNull(result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.ViewResult>(result);
        }

        [Fact]
        public void Contact_ActionReturnsViewForContact()
        {
            var result = _sut.Contact();
            Assert.NotNull(result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.ViewResult>(result);
        }

        [Fact]
        public void About_ActionReturnsViewForAbout()
        {
            var result = _sut.About();
            Assert.NotNull(result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.ViewResult>(result);
        }

        [Fact]
        public void Shop_ActionReturnsViewForShop()
        {
            var result = _sut.Shop();
            Assert.NotNull(result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.ViewResult>(result);
        }
    }
}
