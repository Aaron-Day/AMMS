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
        private IUserRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = A.Fake<IUserRepository>();
        }

        [Test]
        public void Pid_IsValid()
        {
            // Arrange
            A.CallTo(() => _repository.GetUser(A<string>.Ignored)).Returns(new ApplicationUser
            {
                FirstName = "Aaron",
                LastName = "Day",
                SocialSecurityNumber = "000001234"
            });

            // Act
            var service = new UserService(_repository);
            var view = service.GetUser("a");

            // Assert
            Assert.AreEqual(view.Pid, "AD1234");
        }
    }
}
