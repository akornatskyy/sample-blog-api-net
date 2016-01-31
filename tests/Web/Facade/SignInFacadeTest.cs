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
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IUnitOfWorkProvider> mockUnitOfWorkProvider;
        private readonly Mock<IUserService> mockUserService;
        private readonly SignInFacade signInFacade;

        public SignInFacadeTest()
        {
            this.mockUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            this.mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>(MockBehavior.Strict);
            this.mockUserService = new Mock<IUserService>(MockBehavior.Strict);
            this.mockUnitOfWorkProvider.Setup(p => p.Create("ro")).Returns(this.mockUnitOfWork.Object);
            this.mockUnitOfWork.Setup(w => w.Dispose());
            this.signInFacade = new SignInFacade(this.mockUnitOfWorkProvider.Object, this.mockUserService.Object);
        }

        [TestMethod]
        public async Task SignIn_Fails()
        {
            var req = new SignInRequest();
            this.mockUserService.Setup(u => u.Authenticate(req.Username, req.Password)).Returns(Task.FromResult(false));

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
