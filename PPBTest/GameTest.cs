using NUnit.Framework;


namespace PPBTest
{
    [TestFixture]
    public class Tests
    {
        PPB.User primus = new PPB.User("primus", "password", "V,V,V,V,V");
        PPB.User secundus = new PPB.User("secundus", "password", "R,R,R,R,R");
        PPB.User tertius = new PPB.User("tertius", "password", "S,P,S,P,S");
        PPB.User quartus = new PPB.User("quartus", "password", "L,P,V,V,V");
        PPB.User quintus = new PPB.User("quintus", "password", "R,S,V,V,V");
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}