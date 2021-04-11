using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VTubeMon.Common.Tests
{
    [TestClass]
    public class EqualityTests
    {

        [TestMethod]
        public void Test_Equal()
        {
            Test_Equality(Equality.EqualTo);
        }

        [TestMethod]
        public void Test_GreaterThan()
        {
            Test_Equality(Equality.GreaterThan);
        }

        [TestMethod]
        public void Test_GreaterThanOrEqualTo()
        {
            Test_Equality(Equality.GreaterThanOrEqualTo);
        }

        [TestMethod]
        public void Test_LessThan()
        {
            Test_Equality(Equality.LessThan);
        }

        [TestMethod]
        public void Test_LessThanOrEqualTo()
        {
            Test_Equality(Equality.LessThanOrEqualTo);
        }

        [TestMethod]
        public void Test_NotEqualTo()
        {
            Test_Equality(Equality.NotEqualTo);
        }

        private void Test_Equality(Equality equality)
        {
            //arrange
            var expected = equality;
            string s = expected.Display();

            //act
            Equality e = s.ToEquality();

            //assert
            Assert.AreEqual(expected, e);
        }
    }
}
