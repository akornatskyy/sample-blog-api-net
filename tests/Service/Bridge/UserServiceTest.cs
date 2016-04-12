using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Blog.Models;
using Blog.Repository.Interface;
using Blog.Service.Bridge;
using Blog.Service.Interface;

namespace Blog.Service.Tests.Bridge
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IErrorState> mockErrorState;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly IUserService userService;

        public UserServiceTest()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.mockErrorState = this.mockRepository.Create<IErrorState>();
            this.mockUserRepository = this.mockRepository.Create<IUserRepository>();
            this.userService = new UserService(this.mockErrorState.Object, this.mockUserRepository.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.mockRepository.VerifyAll();
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

            var succeed = await this.userService.Authenticate("demo", "valid");

            Assert.IsTrue(succeed);
        }
    }
}
