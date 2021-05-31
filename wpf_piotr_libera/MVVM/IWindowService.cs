using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_piotr_libera.MVVM
{
    public interface IWindowService
    {
        void Show(IViewModel viewModel, double minWidth = 0, double minHeight = 0);
        void ShowDialog(IViewModel viewModel, double minWidth = 0, double minHeight = 0);
    }
}
