
using NUnit.Framework;
using System;

namespace PPBTest
{
    [TestFixture]
    class UserTest
    {
        
        [Test]
        public void ClassExists()
        {
            PPB.User user = new PPB.User("ibims", "VVVVV");
            Assert.IsNotNull(user);
        }

        [Test]
        public void WrongHandtypesFail()
        {
            Assert.Throws(Is.TypeOf<InvalidOperationException>().And.Message.EqualTo("Wrong handtype entered."), delegate { PPB.User user = new PPB.User("ibims", "HHHHH"); });
        }
        [Test]
        public void WrongHandtypeCountFail()
        {
            Assert.Throws(Is.TypeOf<InvalidOperationException>().And.Message.EqualTo("Wrong amount of handtype entered."), delegate { PPB.User user = new PPB.User("ibims", "VVV"); });
        }
    }
}
