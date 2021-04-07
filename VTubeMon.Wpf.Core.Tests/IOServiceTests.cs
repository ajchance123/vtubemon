using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using VTubeMon.API;
using VTubeMon.Wpf.Core.IO;

namespace VTubeMon.Wpf.Core.Tests
{
    [TestClass]
    public class IOServiceTests
    {
        [TestInitialize]
        public void TestsInitialize()
        {
            _fileServiceMock = new Mock<IFileService>();
            _ioService = new IOService(_fileServiceMock.Object);
        }

        private Mock<IFileService> _fileServiceMock;
        private IOService _ioService;


        [TestMethod]
        public void TestSerialize()
        {
            string file = "test.json";
            string path = "path1";
            TestSerializeJsonClass testSerializeJsonClass = new TestSerializeJsonClass()
            {
                Property1 = "test123",
                Property2 = 5
            };

            string json = "{\"Property1\":\"test123\",\"Property2\":5}";

            _fileServiceMock.Setup(f => f.PathCombine(path, file)).Returns(Path.Combine(path, file));
            _ioService.SetPath(path);
            _ioService.SerializeJsonToFile(file, testSerializeJsonClass);

            _fileServiceMock.Verify((f) => f.PathCombine(path, file));
            _fileServiceMock.Verify((f) => f.WriteAllText(Path.Combine(path, file), json));
        }


        [TestMethod]
        public void TestDeserialize()
        {
            string json = "{\"Property1\":\"test123\",\"Property2\":5}";

            string file = "test.json";
            string path = "path1";

            _fileServiceMock.Setup(f => f.PathCombine(path, file)).Returns(file);
            _ioService.SetPath(path);
            _fileServiceMock.Setup(f => f.ReadAllText(file)).Returns(json);
            var jsonTestClass = _ioService.DeserializeFileToJson<TestSerializeJsonClass>(file);

            Assert.AreEqual("test123", jsonTestClass.Property1);
            Assert.AreEqual(5, jsonTestClass.Property2);
        }
    }

    public class TestSerializeJsonClass
    {
        public string Property1 { get; set; }

        public int Property2 { get; set; }
    }
}
