# CSE 445 – Assignment 4  
Hotel Directory XML Validation & JSON Conversion

## Overview
This project satisfies Assignment 4 for ASU CSE 445. It delivers a C# 7.0 console application that:

1. **Validates** an XML hotel directory (`Hotels.xml`) against its schema (`Hotels.xsd`).
2. **Reports** all validation and well-formedness errors found in an intentionally corrupted copy (`HotelErrors.xml`).
3. **Converts** the valid XML into the JSON structure specified in the assignment.

All three XML-related files are maintained at the repository root so their raw GitHub URLs can be hard-coded in `submission.cs`.

## Repository Layout

```
.  
├── Hotels.xml          # Valid instance document  
├── HotelErrors.xml     # Same document with five injected mistakes  
├── Hotels.xsd          # XML Schema definition  
├── ConsoleApp1/        # C# source (submission.cs, csproj, etc.)  
└── README.md
```

## Build & Run

### Using Visual Studio
1. Open **ConsoleApp1.sln** (requires .NET Framework 4.7).
2. Restore NuGet packages (Newtonsoft.Json 13.0.3).
3. Build and run the **ConsoleApp1** project.

## Dependencies
- .NET Framework 4.7
- Newtonsoft.Json 13.0.3 (Installed via NuGet)

## License
This project is released under the MIT License.

**Academic-Integrity Notice:**  
© 2025 Bryce Verberne – ASU CSE 445 Assignment 4.  
Re-using this code in any academic submission without proper attribution violates ASU’s Academic Integrity Policy.
