using EWC_Console;
using System;
using System.Linq;

namespace FirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MedicinalProductsContext())
            {
                // получаем объекты из бд и выводим на консоль
                var medicines = db.Medicines.ToList();
                Console.WriteLine("Список объектов:");
                foreach (var m in medicines)
                {
                    Console.WriteLine($"{m.Id}.{m.Name} - {m.Manufacturer}, {m.Indications}, {m.Contraindications}");
                }
            }
            Console.ReadKey();
        }
    }
}
