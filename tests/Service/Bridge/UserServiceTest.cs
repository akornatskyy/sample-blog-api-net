using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Blog.Service.Interface;
using Blog.Service.Bridge;
using Blog.Repository.Interface;
using Blog.Models;

namespace Blog.Service.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly Mock<IErrorState> mockErrorState;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly IUserService userService;

        public UserServiceTest()
        {
            this.mockErrorState = new Mock<IErrorState>(MockBehavior.Strict);
            this.mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            this.userService = new UserService(this.mockErrorState.Object, this.mockUserRepository.Object);
        }

        [TestMethod]
        public async Task Authenticate_Not_Found()
        {
            this.mockUserRepository.Setup(u => u.FindAuthInfo("demo")).Returns(Task.FromResult<AuthInfo>(null));
            this.mockErrorState.Setup(s => s.AddError(It.IsAny<string>()));

            var succeed = await this.userService.Authenticate("demo", "invalid");

            Assert.IsFalse(succeed);
        }

        [TestMethod]
        public async Task Authenticate_Wrong_Password()
        {
            this.mockUserRepository.Setup(u => u.FindAuthInfo("demo")).Returns(Task.FromResult(new AuthInfo 
            { 
                Password = "valid" 
            }));
            this.mockErrorState.Setup(s => s.AddError(It.IsAny<string>()));

            var succeed = await this.userService.Authenticate("demo", "invalid");

            Assert.IsFalse(succeed);
        }

        [TestMethod]
        public async Task Authenticate_Locked()
        {
            this.mockUserRepository.Setup(u => u.FindAuthInfo("demo")).Returns(Task.FromResult(new AuthInfo 
            { 
                Password = "valid",
                IsLocked = true 
            }));
            this.mockErrorState.Setup(s => s.AddError(It.IsAny<string>()));

            var succeed = await this.userService.Authenticate("demo", "valid");

            Assert.IsFalse(succeed);
        }

        [TestMethod]
        public async Task Authenticate()
        {
            this.mockUserRepository.Setup(u => u.FindAuthInfo("demo")).Returns(Task.FromResult(new AuthInfo
            {
                Password = "valid",
                IsLocked = false
            }));
            this.mockErrorState.Setup(s => s.AddError(It.IsAny<string>()));

            var succeed = await this.userService.Authenticate("demo", "valid");

            Assert.IsTrue(succeed);
        }
    }
}
