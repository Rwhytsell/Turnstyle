using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Turnstyle.Models;
using Xunit;
using Turnstyle.Services;

namespace TurnstyleTest
{
    public class SessionManagerTests
    {
        private readonly Faker<Account> _fakeAccountGen = new Faker<Account>()
            .RuleFor(a => a.Id, f => f.IndexFaker)
            .RuleFor(a => a.Email, f => f.Person.Email)
            .RuleFor(a => a.Username, f => f.Person.UserName)
            .RuleFor(a => a.Password, f => f.Random.String(32, 64, char.MinValue, char.MaxValue));
        
        [Fact]
        public void TestKeyGeneration()
        {
            var key = SessionService.GenerateKey();
            Assert.True(key.Length > 31);
        }

        [Fact]
        public void TestUniqueKeys()
        {
            var keys = new List<string>();
            for (var i = 0; i < 100000; i++)
            {
                keys.Add(SessionService.GenerateKey());
            }
            Assert.True(keys.Count == keys.Distinct().Count());
        }

        [Fact]
        public void TestInvalidateToken()
        {
            var target = "This is here to instantiate the string";
            
            for (var i = 0; i < 1000; i++)
            {
                var newValue = _fakeAccountGen.Generate();
                var newKey = SessionService.NewSession(newValue);
                if (i != 500) continue;
                target = newKey;
            }
            Assert.True(SessionService.CheckToken(target));
            
            SessionService.InvalidateToken(target);
            Assert.False(SessionService.CheckToken(target));
        }

        [Fact]
        public void TestValueRetrieval()
        {
            var newValue = _fakeAccountGen.Generate();
            var targetKey = SessionService.NewSession(newValue);
            var (output, _) = SessionService.GetValue(targetKey);
            Assert.True(output.Email == newValue.Email);
        }
        
        [Fact]
        public void TestTimeToLive()
        {
            // TODO: Figure out how to test this
        }
    }
}
