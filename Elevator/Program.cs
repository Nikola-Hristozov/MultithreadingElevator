namespace Elevator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var elevator = new Elevator();
            var workers = new List<Person>();

            for (var i = 0; i < 5; i++)
            {
                var person = new Person(elevator);
                Console.WriteLine($"{person.Name} with Access {person.AccessLevel}\n");
                workers.Add(person);
            }

            var threadElevator = new Thread(elevator.Check);
            var threads = new List<Thread>();
            threads.Add(threadElevator);
            threadElevator.Start();

            foreach (var worker in workers)
            {
                var thread = new Thread(worker.Work);
                threads.Add(thread);
                thread.Start();
            }


            while (true)
            {
                if (workers.All(worker => worker.FinishedWork))
                {
                    elevator.Stop();
                    break;
                }
            }
        }
    }
}