[![build and test](https://github.com/Satana6699/EWC-Console/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/Satana6699/EWC-Console/actions/workflows/dotnet-desktop.yml)

erDiagram
    Medicines {
        int Id
        string Name
        string Indications
        string Contraindications
        string Manufacturer
        string Packaging
        string Dosage
    }
    MedicinePrices {
        int Id
        int MedicineId
        decimal Price
        date Date
    }
    Diseases {
        int Id
        string Name
        string Duration
        string Symptoms
        string Consequences
    }
    Symptoms {
        int Id
        string Name
    }
    DiseaseSymptoms {
        int Id
        int DiseaseId
        int SymptomId
    }
    DiseaseMedicines {
        int Id
        int DiseaseId
        int MedicineId
    }
    Treatment {
        int Id
        int DiseaseId
        int MedicineId
        string Dosage
        int DurationDays
        int IntervalHours
        string Instructions
    }
    FamilyMembers {
        int Id
        string Name
        date DateOfBirth
        string Gender
    }
    Prescriptions {
        int Id
        int FamilyMemberId
        int DiseaseId
        date Date
    }

    Medicines ||--o{ MedicinePrices : "has"
    Diseases ||--o{ DiseaseSymptoms : "has"
    Symptoms ||--o{ DiseaseSymptoms : "describes"
    Diseases ||--o{ DiseaseMedicines : "treated by"
    Medicines ||--o{ DiseaseMedicines : "uses"
    Diseases ||--o{ Treatment : "has"
    FamilyMembers ||--o{ Prescriptions : "receives"
    Diseases ||--o{ Prescriptions : "prescribed"
