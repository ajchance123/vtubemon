using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace VTubeMon.Common.Tests
{
    [TestClass]
    public class EventLoggerTests
    {
        [TestMethod]
        public void TestLog_String()
        {
            // arrange
            string output = null;
            EventLogger logger = new EventLogger();

            logger.OnLog += (s, str) => output = str;

            string expected = "abcdefg";

            // act
            logger.Log(expected);

            // assert
            Assert.IsNotNull(output);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void TestLog_Exception()
        {
            // arrange
            string output = null;
            EventLogger logger = new EventLogger();

            logger.OnLog += (s, str) => output = str;

            string str = "abcdefg";
            var exception = new Exception(str);
            string expected = exception.ToString();

            // act
            logger.Log(exception);

            // assert
            Assert.IsNotNull(output);
            Assert.AreEqual(expected, output);
        }
    }
}
