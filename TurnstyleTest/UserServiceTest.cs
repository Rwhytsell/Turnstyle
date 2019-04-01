using System;
using Xunit;
using Turnstyle.Services;

namespace TurnstyleTest
{
    public class UserServiceTest
    {
        private UserService userService;
        const string examplePassword = "P455w0rd";

        [Fact]
        public void HashPasswordTest()
        {

            userService = new UserService();
            var hashedPassword = UserService.HashPassword(examplePassword);
            Assert.NotEqual(examplePassword, hashedPassword);
        }

        [Fact]
        public void CheckPasswordTest()
        {
            userService = new UserService();
            var hashedPassword = UserService.HashPassword(examplePassword);
            Assert.True(UserService.CheckPassword(hashedPassword, examplePassword));
        }
    }
}
