using System;

public class Fraction
{
    private int numerator;
    private int denominator;

    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    public Fraction(int top)
    {
        numerator = top;
        denominator = 1;
    }

    public Fraction(int top, int bottom)
    {
        numerator = top;
        denominator = bottom;
    }

    public int GetNumerator()
    {
        return numerator;
    }

    public void SetNumerator(int top)
    {
        numerator = top;
    }

    public int GetDenominator()
    {
        return denominator;
    }

    public void SetDenominator(int bottom)
    {
        denominator = bottom;
    }

    public string GetFractionString()
    {
        return numerator + "/" + denominator;
    }

    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}
class Program
{
    public static void Main()
    {
        Fraction fraction1 = new Fraction();
        Console.WriteLine(fraction1.GetFractionString());
        Console.WriteLine(fraction1.GetDecimalValue());

        Fraction fraction2 = new Fraction(5);
        Console.WriteLine(fraction2.GetFractionString());
        Console.WriteLine(fraction2.GetDecimalValue());

        Fraction fraction3 = new Fraction(3, 4);
        Console.WriteLine(fraction3.GetFractionString());
        Console.WriteLine(fraction3.GetDecimalValue());

        Fraction fraction4 = new Fraction(1, 3);
        Console.WriteLine(fraction4.GetFractionString());
        Console.WriteLine(fraction4.GetDecimalValue());
    }
}