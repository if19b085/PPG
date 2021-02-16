using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace PPBTest
{
    [TestFixture]
    class UserTest
    {
        PPB.User user = new PPB.User("ibims", "1passwort");
        [Test]
        public void userNotNull()
        {
            Assert.IsNotNull(user);
        }
    }
}
