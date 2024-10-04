using EWC_Console;
using System;
using System.Linq;

namespace EWC_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using MedicinalProductsContext db = new();
            DbInitializer.Initialize(db);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            Tasks.Task_1(db, 5);
            Tasks.Task_2(db, 5);
            Tasks.Task_3(db);
            Tasks.Task_4(db, 5);
            Tasks.Task_5(db, 5);
            Tasks.Task_6(db);
            Tasks.Task_7(db);
            Tasks.Task_8(db);
            Tasks.Task_9(db);
            Tasks.Task_10(db);
        }
    }
}
