using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpf_piotr_libera.Models;

namespace wpf_piotr_libera
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MVVM.IWindowService WindowService { get; } = new MVVM.WindowService();
        private ShapesModel ShapesModel { get; } = new ShapesModel();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            wpf_piotr_libera.ViewModels.ShapesViewModel shapesViewModel = new wpf_piotr_libera.ViewModels.ShapesViewModel(ShapesModel);
            WindowService.Show(shapesViewModel);
        }
    }
}
