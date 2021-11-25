using Moq;
using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Abstractions;
using UnitTestExample.Controllers;
using UnitTestExample.Entities;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
       [Test,
            TestCase("password123", false),
    TestCase("princess@gmail", false),
    TestCase("princess.gmail.com", false),
    TestCase("princess@gmail.com", true)
            ]
        public void TestValidateEmail(string email, bool expectedResult)
        {

            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidateEmail(email);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test,
            TestCase("ABCDEFGH", false),
            TestCase("abcdefgh", false),
            TestCase("Abcdefgh", false),
            TestCase("Abcd1", false),
            TestCase("aBCDEFG8", true)
            ]
        public void TestValidateEmailPassword(string password, bool expectedResult)
        {

            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidatePassword(password);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [
    Test,
    TestCase("princess@gmail.com", "aBCDEFG8"),
    TestCase("princess@gmail.com", "Abcdefgh1234"),
]
        public void TestRegisterHappyPath(string email, string password)
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountServiceMock
                .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                .Returns<Account>(a => a);
            var accountController = new AccountController();
            accountController.AccountManager = accountServiceMock.Object;


            // Act
            var actualResult = accountController.Register(email, password);

            // Assert
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
            accountServiceMock.Verify(m => m.CreateAccount(actualResult), Times.Once);
        }



    


    [
    Test,
    TestCase("princess@gmail", "aBCDEFG8"),
    TestCase("princess.gmail.com", "aBCDEFG8"),
    TestCase("princess@gmail.com", "ABCDEFG8"),
    TestCase("princess@gmail.com", "abcdef8"),
    TestCase("princess@gmail.com", "aBCDEFGH"),
    TestCase("princess@gmail.com", "aBFG8"),
]
    public void TestRegisterValidateException(string email, string password)
    {
        // Arrange
        var accountController = new AccountController();

        // Act
        try
        {
            var actualResult = accountController.Register(email, password);
            Assert.Fail();
        }
        catch (Exception ex)
        {
            Assert.IsInstanceOf<ValidationException>(ex);
        }

        // Assert
    }
    }
}
