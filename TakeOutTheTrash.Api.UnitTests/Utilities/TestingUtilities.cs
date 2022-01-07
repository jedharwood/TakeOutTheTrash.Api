using System;
using System.Linq;

namespace TakeOutTheTrash.Api.UnitTests.Utilities
{
    public class TestingUtilities
    {
        private static Random random = new Random();

        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyv0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
