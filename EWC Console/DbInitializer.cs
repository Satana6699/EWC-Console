using System;
using System.Linq;

//Класс для инициализации базы данных путем заполнения ее таблиц тестовым набором записей
namespace EWC_Console
{
    public static class DbInitializer
    {
        public static void Initialize(MedicinalProductsContext db)
        {
            db.Database.EnsureCreated();

            // Проверка занесены ли виды топлива
            if (db.DiseasesAndSymptoms.Any())
            {
                Console.WriteLine("====== База данных уже инициализирована ========");
                return;
            }
            return;
        }
    }

}


