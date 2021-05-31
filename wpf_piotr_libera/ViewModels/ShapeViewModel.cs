using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_piotr_libera.Models;
using wpf_piotr_libera.MVVM;

namespace wpf_piotr_libera.ViewModels
{
    public class ShapeViewModel : MVVM.IViewModel
    {
        private ShapesModel ShapesModel { get; }
        private Shape Shape { get; }
        public Action Close { get; set; }

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
}
