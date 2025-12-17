using System;

interface IShape {
    void Draw();
    void Rotate();
}

public abstract class Shape : IShape  // The Shape class has a method called Draw that just prints the type of shape.
{
    public virtual void Draw()
    {
        Console.WriteLine("Drawing a Shape");
    }

    public virtual void Rotate()
    {
        Console.WriteLine("Rotating a Shape");
    }
}


public class Circle : Shape
{
    
}

public class Square : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a Square");
    }

    public void DrawPortait()
    {
        Console.WriteLine("Drawing a Square Portrait");
    }

}


namespace LiskovSubstitutionExample
{

    class Program
    {
        static void Main(string[] args)
        {
            IShape[] shapes = new Shape[3];
            shapes[0] = new Shape();
            shapes[1] = new Circle();
            shapes[2] = new Square();

            foreach (Shape shape in shapes)
            {
                shape.Draw();
            }
        }
    }
}

// The output of this program is:
// Drawing a Shape
// Drawing a Circle
// Drawing a Square

// We are creating shapes of type shape, circle, and square, but the array is of type shape.
// This is because we can use the Liskov Substitution Principle to substitute a base type for a subtype.
// This means that we can use a square anywhere we can use a shape, and we can use a circle anywhere we can use a shape.