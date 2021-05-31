using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_piotr_libera.Models
{
    public enum ShapeType
    {
        Triangle,
        Square,
        Circle
    }

    public class Shape : INotifyPropertyChanged
    {
        public static string shapeToText(ShapeType type)
        {
            if (type == ShapeType.Triangle)
                return "Triangle";
            if (type == ShapeType.Square)
                return "Square";
            else
                return "Circle";
        }

        public static ShapeType textToShape(string text)
        {
            if (text == "Triangle")
                return ShapeType.Triangle;
            if (text == "Square")
                return ShapeType.Square;
            if (text == "Circle")
                return ShapeType.Circle;
            throw new Exception("Unknown shape type");
        }

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

        public Shape(string color, ShapeType type, double x, double y, double area, string text)
        {
            Color = color;
            Type = shapeToText(type);
            XCoordinate = x;
            YCoordinate = y;
            Area = area;
            Text = text;
        }
    }
}
