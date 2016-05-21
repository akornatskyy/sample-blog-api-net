using System.Data;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Blog.Repository.Infrastructure;
using Blog.Service.Interface;
using Blog.Web.Facade;
using Blog.Web.Models;

namespace Blog.Web.Tests.Facade
{
    [TestClass]
    public sealed class SignInFacadeTest
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IUserService> mockUserService;
        private readonly SignInFacade signInFacade;

        public SignInFacadeTest()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            var mockUnitOfWork = this.mockRepository.Create<IUnitOfWork>();
            var mockUnitOfWorkProvider = this.mockRepository.Create<IUnitOfWorkProvider>();
            this.mockUserService = this.mockRepository.Create<IUserService>();
            mockUnitOfWorkProvider.Setup(p => p.Create("ro", IsolationLevel.ReadCommitted)).ReturnsAsync(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(w => w.Dispose());
            this.signInFacade = new SignInFacade(mockUnitOfWorkProvider.Object, this.mockUserService.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task SignIn_Fails()
        {
            var req = new SignInRequest();
            this.mockUserService.Setup(u => u.Authenticate(req.Username, req.Password)).ReturnsAsync(false);

            var res = await this.signInFacade.Authenticate(req);

            Assert.IsNull(res);
        }

        [TestMethod]
        public async Task SignIn()
        {
            var req = new SignInRequest 
            { 
                Username = "demo",
                Password = "password"
            };
            this.mockUserService.Setup(u => u.Authenticate(req.Username, req.Password)).Returns(Task.FromResult(true));

            var res = await this.signInFacade.Authenticate(req);

            Assert.AreEqual(req.Username, res.Username);
        }
    }
}
