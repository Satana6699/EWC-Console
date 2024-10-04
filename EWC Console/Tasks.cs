using EWC_Console.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWC_Console
{
    public static class Tasks
    {
        public static void Task_1(MedicinalProductsContext db, int take = 0)
        {
            StartTask(1);
            var medicines = (take > 0) ? db.Medicines.Take(take).ToList() : db.Medicines.ToList();
            Console.WriteLine("Table Medicines:");
            foreach (var m in medicines)
            {
                Console.WriteLine($"{m.Id}.{m.Name} - {m.Manufacturer}, {m.Indications}, {m.Contraindications}");
            }
            EndTask(1);
        }

        public static void Task_2(MedicinalProductsContext db, int take = 0)
        {
            StartTask(2);
            var ageLimit = 80;
            var familyMembers = (take > 0) ? db.FamilyMembers.Where(m => m.Age >= ageLimit).Take(take).ToList() :
                db.FamilyMembers.Where(m => m.Age >= ageLimit).ToList();
            Console.WriteLine("Table FamilyMembers (older than 30):");
            foreach (var m in familyMembers)
            {
                Console.WriteLine($"{m.Id}.{m.Name} - {m.Age}, {m.Gender}");
            }
            EndTask(2);

        }

        public static void Task_3(MedicinalProductsContext db)
        {
            StartTask(3);
            var prescriptions = db.Prescriptions
            .Where(p => p.Date.HasValue) //Проверка на null
            .GroupBy(p => new { p.Date.Value.Year, p.Date.Value.Month })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                CountPrescriptions = g.Count()
            })
            .OrderBy(g => g.Year)
            .ThenBy(g => g.Month)
            .ToList();

            Console.WriteLine("Grouped Prescriptions by Month:");
            foreach (var group in prescriptions)
            {
                Console.WriteLine($"Year: {group.Year}, Month: {group.Month}, Count: {group.CountPrescriptions}");
            }
            EndTask(3);

        }

        public static void Task_4(MedicinalProductsContext db, int take = 0)
        {
            StartTask(4);
            var prescriptions = (take > 0) ? db.Prescriptions.Take(take).ToList() : db.Prescriptions.ToList();
            Console.WriteLine("Table Prescriptions:");
            foreach (var p in prescriptions)
            {
                var familyMember = db.FamilyMembers.FirstOrDefault(fm => fm.Id == p.FamilyMemberId);
                var disease = db.Diseases.FirstOrDefault(d => d.Id == p.DiseasesId);

                string familyMemberName = familyMember != null ? familyMember.Name : "Unknown Family Member";
                string diseaseName = disease != null ? disease.Name : "Unknown Disease";

                Console.WriteLine($"{p.Id} - Family: {familyMemberName}, Diseases: {diseaseName}, Date: {p.Date}");
            }
            EndTask(4);
        }

        public static void Task_5(MedicinalProductsContext db, int take = 0)
        {
            StartTask(5);
            DateOnly filterDate = new(2023, 12, 1);
            var prescriptions = (take > 0) ? db.Prescriptions
            .Where(p => p.Date.HasValue && p.Date.Value >= filterDate)
            .OrderBy(p => p.Date.HasValue ? p.Date.Value.Year : 0)
            .ThenBy(p => p.Date.HasValue ? p.Date.Value.Month : 0)
            .Take(take)
            .ToList() :
            db.Prescriptions
            .Where(p => p.Date.HasValue && p.Date.Value >= filterDate)
            .OrderBy(p => p.Date.HasValue ? p.Date.Value.Year : 0)
            .ThenBy(p => p.Date.HasValue ? p.Date.Value.Month : 0)
            .ToList();

            Console.WriteLine("Prescriptions:");
            foreach (var p in prescriptions)
            {
                var familyMember = db.FamilyMembers.FirstOrDefault(fm => fm.Id == p.FamilyMemberId);
                var disease = db.Diseases.FirstOrDefault(d => d.Id == p.DiseasesId);

                string familyMemberName = familyMember != null ? familyMember.Name : "Unknown Family Member";
                string diseaseName = disease != null ? disease.Name : "Unknown Disease";

                Console.WriteLine($"{p.Id} - Family: {familyMemberName}, Diseases: {diseaseName}, Date: {p.Date}");
            }
            EndTask(5);
        }

        public static void Task_6(MedicinalProductsContext db)
        {
            StartTask(6);
            int countBefore = db.Medicines.Count();
            Console.WriteLine($"Records before insert: {countBefore}");
            var newMedicine = new Medicine
            {
                Name = "Aspirin",
                Manufacturer = "Pharma Co.",
                Indications = "Headache",
                Contraindications = "Allergy"
            };
            db.Medicines.Add(newMedicine);
            db.SaveChanges();
            int countAfter = db.Medicines.Count();
            Console.WriteLine($"Records after insert: {countAfter}");
            Console.WriteLine($"New record added in Medicines: {newMedicine.Id}.{newMedicine.Name} - {newMedicine.Manufacturer}, {newMedicine.Indications}, {newMedicine.Contraindications}");
            EndTask(6);
        }

        public static void Task_7(MedicinalProductsContext db)
        {
            StartTask(7);
            var randomFamilyMember = db.FamilyMembers.OrderBy(f => Guid.NewGuid()).FirstOrDefault();
            var randomDisaese = db.Diseases.OrderBy(m => Guid.NewGuid()).FirstOrDefault();
            int countBefore = db.Prescriptions.Count();
            Console.WriteLine($"Records before insert: {countBefore}");
            var newPrescription = new Prescription
            {
                FamilyMemberId = randomFamilyMember.Id,
                DiseasesId = randomDisaese.Id,
                Date = new(new Random().Next(2020, 2024), new Random().Next(1, 12), new Random().Next(0, 29))
            };
            db.Prescriptions.Add(newPrescription);
            db.SaveChanges();
            int countAfter = db.Prescriptions.Count();

            var familyMember = db.FamilyMembers.FirstOrDefault(fm => fm.Id == newPrescription.FamilyMemberId);
            var disease = db.Diseases.FirstOrDefault(d => d.Id == newPrescription.DiseasesId);

            string familyMemberName = familyMember != null ? familyMember.Name : "Unknown Family Member";
            string diseaseName = disease != null ? disease.Name : "Unknown Disease";

            Console.WriteLine($"Records after insert: {countAfter}");
            Console.WriteLine($"{newPrescription.Id} - Family: {familyMemberName}, Diseases: {diseaseName}, Date: {newPrescription.Date}");
            EndTask(7);
        }

        public static void Task_8(MedicinalProductsContext db)
        {
            StartTask(8);
            int countBefore = db.Medicines.Count();
            Console.WriteLine($"Records before insert: {countBefore}");
            var medicine = db.Medicines.OrderBy(f => Guid.NewGuid()).FirstOrDefault();

            if (medicine != null)
            {
                db.Medicines.Remove(medicine);
                db.SaveChanges();
                Console.WriteLine("Deletion complited");
            }
            int countAfter = db.Medicines.Count();
            Console.WriteLine($"Records after insert: {countAfter}");
            EndTask(8);
        }

        public static void Task_9(MedicinalProductsContext db)
        {
            StartTask(9);
            int countBefore = db.Prescriptions.Count();
            Console.WriteLine($"Records before insert: {countBefore}");
            var prescriptions = db.Prescriptions.OrderBy(f => Guid.NewGuid()).FirstOrDefault();

            if (prescriptions != null)
            {
                db.Prescriptions.Remove(prescriptions);
                db.SaveChanges();
                Console.WriteLine("Deletion complited");
            }
            int countAfter = db.Prescriptions.Count();
            Console.WriteLine($"Records after insert: {countAfter}");
            EndTask(9);
        }

        public static void Task_10(MedicinalProductsContext db)
        {
            StartTask(10);
            var randomFamilyMemberId = db.FamilyMembers.OrderBy(f => Guid.NewGuid()).FirstOrDefault().Id;
            var randomDisaeseId = db.Diseases.OrderBy(m => Guid.NewGuid()).FirstOrDefault().Id;

            var prescription = db.Prescriptions.OrderBy(f => Guid.NewGuid()).FirstOrDefault();

            var familyMember = db.FamilyMembers.FirstOrDefault(fm => fm.Id == prescription.FamilyMemberId);
            var disease = db.Diseases.FirstOrDefault(d => d.Id == prescription.DiseasesId);

            string familyMemberName = familyMember != null ? familyMember.Name : "Unknown Family Member";
            string diseaseName = disease != null ? disease.Name : "Unknown Disease";

            Console.WriteLine($"Prescription before: {prescription.Id} - Family: {familyMemberName}, " +
                $"Diseases: {diseaseName}, Date: {prescription.Date}");

            if (prescription != null)
            {
                prescription.DiseasesId = randomDisaeseId;
                prescription.FamilyMemberId = randomFamilyMemberId;
                prescription.Date = new(new Random().Next(2020, 2024), new Random().Next(1, 12), new Random().Next(1, 29));
                db.Prescriptions.Update(prescription);
                db.SaveChanges();
                Console.WriteLine("Updation complited");
            }

            familyMember = db.FamilyMembers.FirstOrDefault(fm => fm.Id == prescription.FamilyMemberId);
            disease = db.Diseases.FirstOrDefault(d => d.Id == prescription.DiseasesId);

            familyMemberName = familyMember != null ? familyMember.Name : "Unknown Family Member";
            diseaseName = disease != null ? disease.Name : "Unknown Disease";

            Console.WriteLine($"Prescription after: {prescription.Id} - Family: {familyMemberName}, " +
                $"Diseases: {diseaseName}, Date: {prescription.Date}");
            EndTask(10);
        }


        private static void StartTask(int numberTask)
        {
            Console.WriteLine($"+======================START TASK {numberTask}:======================+");
        }
        private static void EndTask(int numberTask)
        {
            Console.WriteLine($"+======================END TASK {numberTask}======================+");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
