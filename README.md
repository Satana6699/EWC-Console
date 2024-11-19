[![build and test](https://github.com/Satana6699/EWC-Console/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/Satana6699/EWC-Console/actions/workflows/dotnet-desktop.yml)

# Диаграмма сущностей и связей

```mermaid
erDiagram
Manufacturers {
    int Id
    string Name
}

Medicines {
    int Id
    string Name
    string Indications
    string Contraindications
    int ManufacturerId
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
    int GenderId
}

Genders {
    int Id
    string Name
}

Prescriptions {
    int Id
    int FamilyMemberId
    int DiseaseId
    date Date
    bool Status
}

Manufacturers ||--o{ Medicines : "produces"
Medicines ||--o{ MedicinePrices : "has"
Diseases ||--o{ DiseaseSymptoms : "has"
Symptoms ||--o{ DiseaseSymptoms : "describes"
Diseases ||--o{ Treatment : "has"
FamilyMembers ||--o{ Prescriptions : "receives"
Diseases ||--o{ Prescriptions : "prescribed"
Genders ||--o{ FamilyMembers : "defines"
