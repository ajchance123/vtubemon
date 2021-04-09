using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTubeMon.Common;
using VTubeMon.Data.Commands;

namespace VTubeMon.Data.Tests
{
    [TestClass]
    public class WhereStatementTests
    {
        [TestMethod]
        public void Test_Where_Statement()
        {
            //arrange
            var where = new WhereStatement()
            {
                Equality = Equality.LessThan,
                Target = "id",
                Value = "4"
            };

            string expected = "id < 4";
            //act

            //assert
            Assert.AreEqual(expected, where.Statement);
        }
    }
}
