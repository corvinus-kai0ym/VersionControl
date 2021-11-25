using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

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



    }
}
