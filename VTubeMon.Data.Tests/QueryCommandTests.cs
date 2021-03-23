using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTubeMon.API;
using VTubeMon.Data.Commands;

namespace VTubeMon.Data.Tests
{
    [TestClass]
    public class QueryCommandTests
    {
        [TestMethod]
        public void Test_Query_No_Where()
        {
            //arrange
            string table = "vtubers";
            string columns = "*";

            var query = new QueryCommand<MockDataObject>(table, columns);

            var expected = "SELECT * FROM vtubers";

            //act

            //assert
            Assert.AreEqual(expected, query.Statement);
        }

        [TestMethod]
        public void Test_Query_No_Single_Where()
        {
            //arrange
            string table = "vtubers";
            string columns = "*";

            var query = new QueryCommand<MockDataObject>(table, columns, new WhereStatement()
            {
                Equality = Core.Equality.EqualTo,
                Target = "en_name",
                Value = "haaaachamaaaa",
                UseQuotes = true
            });

            var expected = "SELECT * FROM vtubers WHERE en_name = 'haaaachamaaaa'";

            //act

            //assert
            Assert.AreEqual(expected, query.Statement);
        }


        [TestMethod]
        public void Test_Query_No_Multi_Where()
        {
            //arrange
            string table = "vtubers";
            string columns = "*";

            var query = new QueryCommand<MockDataObject>(table, columns, 
                new WhereStatement()
                {
                    Equality = Core.Equality.EqualTo,
                    Target = "en_name",
                    Value = "haaaachamaaaa",
                    UseQuotes = true
                }, 
                new WhereStatement()
                {
                    Equality = Core.Equality.GreaterThan,
                    Target = "id_vtuber",
                    Value = "3"
                });

            var expected = "SELECT * FROM vtubers WHERE en_name = 'haaaachamaaaa' AND id_vtuber > 3";

            //act

            //assert
            Assert.AreEqual(expected, query.Statement);
        }
    }
}
