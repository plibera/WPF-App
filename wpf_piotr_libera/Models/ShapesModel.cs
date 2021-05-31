using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_piotr_libera.Models
{
    public class ShapesModel
    {
        public ObservableCollection<Shape> Shapes { get; private set; } = new ObservableCollection<Shape>();
        public ShapesModel()
        {
            Shapes.Add(new Shape("blue", ShapeType.Square, 1, 2, 15, "My square"));
            Shapes.Add(new Shape("red", ShapeType.Circle, -1, 0, 10, "My circle"));
        }
    }
}
