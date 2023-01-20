using Microsoft.VisualBasic;

namespace Elevator;

public class Person
{
    private readonly Random _random = new();
    private readonly Elevator _elevator;


    public string Name { get; set; }

    public bool FinishedWork => CurrentFloor == Floor.G && Moves > 5;

    public Floor CurrentFloor { get; set; } = Floor.G;
    public Floor DestinationFloor { get; set; }
    public AccessLevel AccessLevel { get; set; }
    public int Moves { get; set; }

    public Person(Elevator elevator)
    {
        //Product of laziness
        do
        {
            AccessLevel = (AccessLevel)_random.Next(4);
        } while ((int)AccessLevel == 2);

        ChangeDestination();

        _elevator = elevator;

        Name = GetName();
    }

    public void Work()
    {
        while (true)
        {
            while (_elevator.Caller == this || _elevator.Occupant == this)
            {
            }
            CallElevator();
            if (FinishedWork)
            {
                break;
            }
        }

        Console.WriteLine($"{Name} finished work");
    }

    public void CallElevator()
    {
        while (_elevator.Caller != null || _elevator.Occupant == this)
        {
        }

        if (FinishedWork)
        {
            return;
        }

        _elevator.Caller = this;
    }

    public void ChangeDestination()
    {
        DestinationFloor = (Floor)_random.Next(4);
    }
    private string GetName()
    {
        //If I used female names it would overcomplicate so..
        var firstNames = new[]
            { "John", "Nikola", "Vasil", "Ismail", "Sava", "Martin", "Simeon", "Samoil", "Mavricii", "Pencho", "Gosho", "Tosho", "Pesho", "Ivan" };
        var lastNames = new[]
            { "Ivanov", "Vasilev", "Borislavov", "Avramov", "Eshkenazi", "Ognqnov", "Hristov", "Penchev" };

        return $"{firstNames[_random.Next(firstNames.Length)]} {lastNames[_random.Next(lastNames.Length)]}";
    }
}

