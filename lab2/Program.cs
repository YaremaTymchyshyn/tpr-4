using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static double F12(double x1, double x2)
    {
        return Math.Round(32 * Math.Pow(x1, 2) + x1 + 10 * Math.Pow(x2, 2) - 14.5 * x2 + 52, 5);
    }

    static double F21(double x1, double x2)
    {
        return Math.Round(-18 * Math.Pow(x2, 2) + 27 * x2 + 10 * Math.Pow(x1, 2) - 17.5 * x1 + 3, 5);
    }

    static void Main()
    {
        double x1Lower = 2, x1Upper = 6, x2Lower = 1, x2Upper = 10;
        double x1Step = 0.01, x2Step = 0.02;

        List<Table> tables = new List<Table>();
        Console.WriteLine("|    x1   |    x2   |       f12       |       f21       |");

        for (double x1 = x1Lower; x1 <= x1Upper; x1 = Math.Round(x1 + x1Step, 5))
        {
            for (double x2 = x2Lower; x2 <= x2Upper; x2 = Math.Round(x2 + x2Step, 5))
            {
                Table table = new Table(x1, x2, F12(x1, x2), F21(x1, x2));
                tables.Add(table);
                Console.WriteLine(table);
            }
        }

        var guaranteedF12 = tables.GroupBy(q => q.X1).Select(q => q.MinBy(g => g.F12)).MaxBy(q => q.F12);
        var guaranteedF21 = tables.GroupBy(q => q.X2).Select(q => q.MinBy(g => g.F21)).MaxBy(q => q.F21);

        if (guaranteedF12 != null && guaranteedF21 != null)
        {
            Console.WriteLine($"\nGuaranteed result f12* = {guaranteedF12.F12}:");
            Console.WriteLine("|    x1   |    x2   |       f12       |       f21       |");
            Console.WriteLine(guaranteedF12);

            Console.WriteLine($"\nGuaranteed result f21* = {guaranteedF21.F21}:");
            Console.WriteLine("|    x1   |    x2   |       f12       |       f21       |");
            Console.WriteLine(guaranteedF21);
        }
    }
}

class Table
{
    public double X1 { get; set; }
    public double X2 { get; set; }
    public double F12 { get; set; }
    public double F21 { get; set; }

    public Table(double x1, double x2, double f12, double f21)
    {
        X1 = x1;
        X2 = x2;
        F12 = f12;
        F21 = f21;
    }

    public override string ToString()
    {
        return string.Format("| {0,7:F2} | {1,7:F2} | {2,15:F5} | {3,15:F5} |", X1, X2, F12, F21);
    }
}
