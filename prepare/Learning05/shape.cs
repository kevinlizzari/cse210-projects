public class Shape
{
    private string color;

    public string Color
    {
        get { return color; }
        set { color = value; }
    }

    public Shape(string color)
    {
        Color = color;
    }

    public virtual double GetArea()
    {
        return 0;
    }
}