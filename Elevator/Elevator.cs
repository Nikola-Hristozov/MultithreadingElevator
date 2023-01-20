using System.Threading.Channels;
using System.Transactions;
using Microsoft.VisualBasic;

namespace Elevator;

public class Elevator
{
    private bool _stopped;
    public Person? Caller { get; set; }
    public Person? Occupant { get; set; }
    public Floor CurrentFloor { get; set; }

    public void Check()
    {
        while (!_stopped)
        {
            if (Occupant != null)
            {
                if (Occupant.DestinationFloor == CurrentFloor)
                {
                    var hasAccess = CheckAccess(Occupant);
                    if (hasAccess)
                    {
                        Occupant.Moves++;
                        Occupant.CurrentFloor = CurrentFloor;
                        Occupant.ChangeDestination();
                        Console.WriteLine($"{Occupant.Name} got off on {CurrentFloor}\n");
                        Occupant = null;
                    }
                    else
                    {
                        Console.WriteLine($"{Occupant.Name} can't go off on {CurrentFloor}");
                        Occupant.ChangeDestination();
                        Console.WriteLine($"{Occupant.Name} headed to {Occupant.DestinationFloor}");
                    }
                }
                else
                {
                    Move(Occupant.DestinationFloor);
                }
            }
            else
            {
                if (Caller != null)
                {
                    if (Caller.CurrentFloor == CurrentFloor)
                    {
                        Occupant = Caller;
                        Console.WriteLine($"\n{Occupant.Name} got on to {Occupant.DestinationFloor}");
                        Caller = null;
                    }
                    else
                    {
                        Move(Caller.CurrentFloor);
                    }
                }   
            }

        }
    }

    private void Move(Floor nextFloor)
    {
        Thread.Sleep(1000);
        Console.Write($"Elevator moved from {CurrentFloor}");
        CurrentFloor += CurrentFloor > nextFloor ? -1 : 1;
        Console.WriteLine($" to {CurrentFloor}");
    }

    public static bool CheckAccess(Person person)
    {
        return (int)person.AccessLevel >= (int)person.DestinationFloor;
    }

    public void Stop()
    {
        _stopped = true;
    }
}