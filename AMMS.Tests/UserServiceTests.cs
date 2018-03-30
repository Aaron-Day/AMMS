using AMMS.Models;
using AMMS.Repository;
using AMMS.Services;
using FakeItEasy;
using NUnit.Framework;

namespace AMMS.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IAccountRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = A.Fake<IAccountRepository>();
        }

        [Test]
        public void Pid_IsValid()
        {
            // Arrange
            A.CallTo(() => _repository.GetUserById(A<string>.Ignored)).Returns(new ApplicationUser
            {
                FirstName = "Aaron",
                LastName = "Day",
                SocialSecurityNumber = "000001234"
            });

            // Act
            var service = new AccountService(_repository);
            var view = service.GetUserById("a");

            // Assert
            Assert.AreEqual(view.Pid, "AD1234");
        }
    }
}
