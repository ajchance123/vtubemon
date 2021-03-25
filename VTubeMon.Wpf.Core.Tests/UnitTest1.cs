using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VTubeMon.API;
using VTubeMon.Data;
using VTubeMon.Wpf.Core.Components;

namespace VTubeMon.Wpf.Core.Tests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            _loggerMock = new Mock<ILogger>();
            _vTubeMonDbConnectionMock = new Mock<IVTubeMonDbConnection>();
            _dataCache = new DataCache(_vTubeMonDbConnectionMock.Object);
            _vTubeMonServerConnectionMock = new Mock<IVTubeMonServerConnection>();
            _databaseWorkspace = new DatabaseWorkspace();

            _mainWindowViewModel = new MainWindowViewModel(_loggerMock.Object, _vTubeMonDbConnectionMock.Object, _dataCache, _vTubeMonServerConnectionMock.Object, _databaseWorkspace);
        }

        private MainWindowViewModel _mainWindowViewModel;
        private Mock<ILogger> _loggerMock;
        private Mock<IVTubeMonDbConnection> _vTubeMonDbConnectionMock;
        private DataCache _dataCache;
        private Mock<IVTubeMonServerConnection> _vTubeMonServerConnectionMock;
        private DatabaseWorkspace _databaseWorkspace;

        [TestMethod]
        public void TestInitialValues()
        {
            //arrange

            //act

            //assert
            Assert.IsTrue(_mainWindowViewModel.CanConnectDiscord);
            Assert.IsFalse(_mainWindowViewModel.CanDisconnectDiscord);

            Assert.IsFalse(_mainWindowViewModel.ShowDatabaseView);
            Assert.IsTrue(_mainWindowViewModel.ShowDiscordView);
            Assert.IsFalse(_mainWindowViewModel.ShowGameView);
            Assert.IsFalse(_mainWindowViewModel.ShowSettingsView);

            Assert.IsFalse(_mainWindowViewModel.ShowLogOutputWindow);
        }

        [TestMethod]
        public void Test_SetDataBaseView()
        {
            //arrange

            //act
            _mainWindowViewModel.ShowDatabaseView = true;

            //assert
            Assert.IsTrue(_mainWindowViewModel.CanConnectDiscord);
            Assert.IsFalse(_mainWindowViewModel.CanDisconnectDiscord);

            Assert.IsTrue(_mainWindowViewModel.ShowDatabaseView);
            Assert.IsFalse(_mainWindowViewModel.ShowDiscordView);
            Assert.IsFalse(_mainWindowViewModel.ShowGameView);
            Assert.IsFalse(_mainWindowViewModel.ShowSettingsView);

            Assert.IsFalse(_mainWindowViewModel.ShowLogOutputWindow);
        }

        [TestMethod]
        public void Test_SetGameView()
        {
            //arrange

            //act
            _mainWindowViewModel.ShowGameView = true;

            //assert
            Assert.IsTrue(_mainWindowViewModel.CanConnectDiscord);
            Assert.IsFalse(_mainWindowViewModel.CanDisconnectDiscord);

            Assert.IsFalse(_mainWindowViewModel.ShowDatabaseView);
            Assert.IsFalse(_mainWindowViewModel.ShowDiscordView);
            Assert.IsTrue(_mainWindowViewModel.ShowGameView);
            Assert.IsFalse(_mainWindowViewModel.ShowSettingsView);

            Assert.IsFalse(_mainWindowViewModel.ShowLogOutputWindow);
        }

        [TestMethod]
        public void Test_SetShowSettingsView()
        {
            //arrange

            //act
            _mainWindowViewModel.ShowSettingsView = true;

            //assert
            Assert.IsTrue(_mainWindowViewModel.CanConnectDiscord);
            Assert.IsFalse(_mainWindowViewModel.CanDisconnectDiscord);

            Assert.IsFalse(_mainWindowViewModel.ShowDatabaseView);
            Assert.IsFalse(_mainWindowViewModel.ShowDiscordView);
            Assert.IsFalse(_mainWindowViewModel.ShowGameView);
            Assert.IsTrue(_mainWindowViewModel.ShowSettingsView);

            Assert.IsFalse(_mainWindowViewModel.ShowLogOutputWindow);
        }

        [TestMethod]
        public void Test_VTubeMonServerConnection_OnDisconnect()
        {

        }
    }
}
