using Autofac;
using System.Windows;
using VTubeMon.Wpf.Core.Components.Database;
using VTubeMon.Wpf.Core.Components.Discord;
using VTubeMon.Wpf.Core.Components.Settings;

namespace VTubeMon.Wpf.Core.IOC
{
    public class ViewModelLocator : DependencyObject
    {
        public IContainer Container { get; set; }

        #region Container Dependency Property

        public static readonly DependencyProperty ContainerProperty =
            DependencyProperty.Register("Container", typeof(IContainer), typeof(ViewModelLocator),
                new PropertyMetadata(OnMainModulePropertyChanged));

        private static void OnMainModulePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var locator = (ViewModelLocator)d;
            locator.Container = (IContainer)e.NewValue;
        }

        #endregion

        public MainWindowViewModel MainWindowViewModel { get { return Container.Resolve<MainWindowViewModel>(); } }
        public DatabaseViewModel DatabaseViewModel { get { return Container.Resolve<DatabaseViewModel>(); } }
        public DiscordViewModel DiscordViewModel { get { return Container.Resolve<DiscordViewModel>(); } }
        public SettingsViewModel SettingsViewModel { get { return Container.Resolve<SettingsViewModel>(); } }
    }
}