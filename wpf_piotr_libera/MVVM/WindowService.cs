using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf_piotr_libera.MVVM
{
    public class WindowService : IWindowService
    {
        public void Show(IViewModel viewModel, double minWidth=0, double minHeight=0)
        {
            Window window = new Window();
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Title = "Shapes database";
            window.Content = viewModel;
            viewModel.Close = window.Close;
            window.MinHeight = minHeight;
            window.MinWidth = minWidth;
            window.Show();
        }

        public void ShowDialog(IViewModel viewModel, double minWidth, double minHeight)
        {
            Window window = new Window();
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Title = "Shape data";
            window.Content = viewModel;
            viewModel.Close = window.Close;
            window.MinHeight = minHeight;
            window.MinWidth = minWidth;
            window.ShowDialog();
        }
    }
}
