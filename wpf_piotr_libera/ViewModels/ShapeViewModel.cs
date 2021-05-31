using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private string color;
        public string Color
        {
            get { return color; }
            set { color = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color")); }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type")); }
        }

        private double xCoordinate;
        public double XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("XCoordinate")); }
        }

        private double yCoordinate;
        public double YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("YCoordinate")); }
        }

        private double area;
        public double Area
        {
            get { return area; }
            set { area = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Area")); }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text")); }
        }


        public RelayCommand<ShapeViewModel> OkCommand { get; } = new RelayCommand<ShapeViewModel>
            (
                (shapeViewModel) => { shapeViewModel.Ok(); }
            );
        public RelayCommand<ShapeViewModel> CancelCommand { get; } = new RelayCommand<ShapeViewModel>
            (
                (shapeViewModel) => { shapeViewModel.Cancel(); }
            );
        public RelayCommand<ShapeViewModel> NextShapeTypeCommand { get; } = new RelayCommand<ShapeViewModel>
            (
                (shapeViewModel) => { shapeViewModel.OnClickShapeControl(); }
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

        public void OnClickShapeControl()
        {
            switch (Type)
            {
                case "Circle":
                    Type = "Square";
                    break;
                case "Square":
                    Type = "Triangle";
                    break;
                case "Triangle":
                    Type = "Circle";
                    break;
                default:
                    throw new Exception("Unknown shape type");
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