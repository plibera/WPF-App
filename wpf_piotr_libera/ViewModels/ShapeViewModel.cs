using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using wpf_piotr_libera.Models;
using wpf_piotr_libera.MVVM;

namespace wpf_piotr_libera.ViewModels
{
    public class ShapeViewModel : MVVM.IViewModel, INotifyPropertyChanged
    {
        private ShapesModel ShapesModel { get; }
        private Shape Shape { get; }
        public Action Close { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Color { get; set; }
        public string Type { get; set; }
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public double Area { get; set; }
        public string Text { get; set; }


        public RelayCommand<ShapeViewModel> OkCommand { get; } = new RelayCommand<ShapeViewModel>
            (
                (shapeViewModel) => { shapeViewModel.Ok(); }
            );
        public RelayCommand<ShapeViewModel> CancelCommand { get; } = new RelayCommand<ShapeViewModel>
            (
                (shapeViewModel) => { shapeViewModel.Cancel(); }
            );

        public ShapeViewModel(ShapesModel shapesModel, Shape shape)
        {
            ShapesModel = shapesModel;
            Shape = shape;
            Type = "Triangle";
            if (Shape != null)
            {
                Color = Shape.Color;
                Type = Shape.Type;
                XCoordinate = Shape.XCoordinate;
                YCoordinate = Shape.YCoordinate;
                Area = Shape.Area;
                Text = Shape.Text;
            }
        }

        public void Ok()
        {
            if (Shape == null)
            {
                Shape shape = new Shape(Color, Shape.textToShape(Type),
                                    XCoordinate, YCoordinate, Area, Text);
                ShapesModel.Shapes.Add(shape);
            }
            else
            {
                Shape.Color = Color;
                Shape.Type = Type;
                Shape.XCoordinate = XCoordinate;
                Shape.YCoordinate = YCoordinate;
                Shape.Area = Area;
                Shape.Text = Text;
            }
            Close?.Invoke();
        }
        public void Cancel() => Close?.Invoke();
    }


    public class InverseAndBooleansToBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.LongLength > 0)
            {
                foreach (var value in values)
                {
                    if (value is bool && (bool)value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}