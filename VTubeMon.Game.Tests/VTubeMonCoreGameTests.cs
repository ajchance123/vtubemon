using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using VTubeMon.API;
using VTubeMon.Game;

namespace VTubeMon.Common.Tests
{
    [TestClass]
    public class VTubeMonCoreGameTests
    {
        private Mock<IVTubeMonCommandFactory> _mockVtubeMonCommandfactory;
        private VTubeMonCoreGame _vTubeMonCoreGame;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockVtubeMonCommandfactory = new Mock<IVTubeMonCommandFactory>();

            _vTubeMonCoreGame = new VTubeMonCoreGame(_mockVtubeMonCommandfactory.Object);
        }
        
        [TestMethod]
        public void Test_Register()
        {
            //Arrange
            ulong user = 2000;
            ulong guild = 1000;
            var cash = _vTubeMonCoreGame.RegistrationValue;

            var result = new CommandResult(CommandResultType.Failure);

            _mockVtubeMonCommandfactory.Setup(m => m.RegisterCommand(user, guild, cash)).Returns(result);

            //Act
            var registerResult = _vTubeMonCoreGame.Register(user, guild);

            //Assert
            _mockVtubeMonCommandfactory.Verify(m => m.RegisterCommand(user, guild, cash));
            Assert.AreSame(result, registerResult);
        }



        [TestMethod]
        public void Test_DailyCheckin()
        {
            //Arrange
            ulong user = 2000;
            ulong guild = 1000;
            var utcNow = DateTime.UtcNow;

            var result = new CommandResult(CommandResultType.Success);

            //_mockVtubeMonCommandfactory.Setup(m => m.DailyCheckinCommand(user, guild, utcNow)).Returns(result);

            //Act
            var dailyCheckinResult = _vTubeMonCoreGame.DailyCheckIn(user, guild, utcNow);

            //Assert
            _mockVtubeMonCommandfactory.VerifyAll();
            Assert.ReferenceEquals(result, dailyCheckinResult);
        }
    }
}
