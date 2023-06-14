using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();

        Shape square = new Square("Red", 5);
        shapes.Add(square);

        Shape rectangle = new Rectangle("Blue", 3, 4);
        shapes.Add(rectangle);

        Shape circle = new Circle("Green", 2);
        shapes.Add(circle);

        foreach (Shape shape in shapes)
        {
            string color = shape.Color;
            double area = shape.GetArea();
            Console.WriteLine($"The {color} shape has an area of {area}.");
            Console.WriteLine();
        }
    }
}