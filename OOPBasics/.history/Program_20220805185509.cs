// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


public class Fruit
{
    public string Name { get; set; }
    public string Color { get; set; }
}

public class Apple : Fruit
{
    public string Taste { get; set; }

    public Apple()
    {
        Name = "Apple";
        Color = "Red";
        Taste = "Sweet";
    }
}

public class Mango : Fruit
{
    public string Taste { get; set; }

    
}