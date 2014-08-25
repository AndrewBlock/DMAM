using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DMAM.Application
{
    public partial class MainWindow
    {
        private MainViewModel _viewModel = new MainViewModel();
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;

            _viewModel.Initialize();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _viewModel.Shutdown();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element == null)
            {
                return;
            }

            _viewModel.NotifyDoubleClick(element.DataContext);
        }
    }
}
