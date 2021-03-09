using Autofac;
using System.Windows;

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
    }
}