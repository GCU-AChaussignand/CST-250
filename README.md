# CST-250: Programming in C# II

This repository contains coursework, programming activities, documentation, unit tests, and project milestones completed for **CST-250: Programming in C# II** at Grand Canyon University.

**Course dates:** July 13, 2026 – August 30, 2026  
**Credits:** 4  
**Primary language:** C#  
**Development environment:** Microsoft Visual Studio  
**Main semester project:** Minesweeper

---

## Course Overview

CST-250 focuses on advanced C# programming and the development of complex desktop applications. The course emphasizes object-oriented programming, separation of concerns, N-layer architecture, graphical user interfaces, recursion, event-driven programming, file handling, LINQ, data binding, testing, and maintainable software design.

This repository documents my progress through the course and demonstrates the use of:

- C# and the .NET Framework
- Windows Forms applications
- Console applications
- Object-oriented programming
- Abstraction, encapsulation, inheritance, and polymorphism
- Separation of concerns
- N-layer application architecture
- Test-driven development
- xUnit unit testing
- Two-dimensional arrays
- Recursive algorithms
- Dynamic user-interface controls
- Timer-driven events
- File input and output
- JSON and CSV serialization
- LINQ queries
- Regular expressions
- Data binding
- Git and GitHub source control
- UML class diagrams
- Program flowcharts

---

## Repository Goals

The goals of this repository are to:

1. Maintain organized source code for all CST-250 activities and milestones.
2. Demonstrate clear separation between presentation, business logic, data access, and model layers.
3. Track the development of the Minesweeper application throughout the course.
4. Preserve UML diagrams, flowcharts, screenshots, and written analyses.
5. Demonstrate consistent use of Git version control.
6. Apply GCU coding guidelines and professional C# development practices.

---

## Suggested Repository Structure

```text
CST-250/
├── Activities/
│   ├── Activity-1-Car-Store/
│   ├── Activity-2-Chessboard/
│   ├── Activity-3-Recursion/
│   ├── Activity-4-Pizza-Order-System/
│   ├── Activity-5-Whack-A-Mole/
│   └── Activity-6-Bible-Verse-Inventory/
│
├── Minesweeper/
│   ├── Milestone-1-Architecture/
│   ├── Milestone-2-Console-Game/
│   ├── Milestone-3-Recursive-Flood-Fill/
│   ├── Milestone-4-Windows-Forms-GUI/
│   ├── Milestone-5-Statistics-and-High-Scores/
│   └── Milestone-6-Advanced-Features/
│
├── Documentation/
│   ├── Flowcharts/
│   ├── UML-Diagrams/
│   ├── Screenshots/
│   └── Written-Analyses/
│
├── Tests/
│   └── xUnit-Test-Projects/
│
├── .gitignore
├── LICENSE
└── README.md
```

The exact folder names may vary as the repository develops.

---

## Weekly Topics and Assignments

### Topic 1 — Separation of Concerns in Multi-Layer Application Design

**Activity:** Car Store application  
**Minesweeper milestone:** Architecture and initial console implementation

Key concepts:

- Separation of concerns
- N-layer architecture
- Models, business logic, and data access
- Abstraction
- Inheritance
- Polymorphism
- xUnit testing
- Test-driven development

---

### Topic 2 — Integrating Data Structures with User Interfaces

**Activity:** Chessboard application using a two-dimensional array  
**Minesweeper milestone:** Interactive console version

Key concepts:

- Two-dimensional arrays
- Console and GUI front ends
- Shared back-end logic
- Board and cell models
- Encapsulation
- Input validation
- Legal-move calculations

---

### Topic 3 — Recursive Solutions

**Activity:** Recursive console applications  
**Minesweeper milestone:** Recursive flood-fill implementation

Recursive programs include:

- Count to One
- Factorial
- Greatest Common Divisor
- Flood Fill

Key concepts:

- Base cases
- Recursive calls
- Call-stack behavior
- Debugging recursion
- Input validation
- Real-world recursive problem solving

---

### Topic 4 — User Interface Controls and Data Structures

**Activity:** Data-driven Pizza Order System  
**Minesweeper milestone:** Windows Forms graphical user interface

Key concepts:

- Windows Forms controls
- Form properties
- User input
- Multiple forms
- Data structures aligned with UI controls
- Presentation layer
- Business logic layer
- Data access layer
- Form navigation

---

### Topic 5 — Timer Events in Application Development

**Activity:** Whack-A-Mole game  
**Minesweeper milestone:** Statistics and high-score tracking

Key concepts:

- Timer controls
- Event-driven programming
- Dynamic controls
- Scoring
- Penalties
- Levels
- Player information
- Game-state management
- High-score storage

---

### Topic 6 — Advanced Data Handling in .NET

**Activity:** Bible Verse Inventory application

Key concepts:

- Text-file input and output
- JSON data
- CSV data
- NuGet packages
- Serialization
- LINQ query syntax
- LINQ method syntax
- Sorting and filtering
- Regular expressions
- DataGridView data binding
- Custom Windows Forms controls

---

### Topic 7 — Exploration and Final Enhancements

**Minesweeper milestone:** Advanced features and custom enhancements

Key concepts:

- Applying concepts from previous topics
- Designing open-ended solutions
- Testing new features
- Extending an existing application
- Improving usability and maintainability
- Final project refinement

---

## Minesweeper Project Progression

The main project is developed incrementally across six milestones.

| Milestone | Description | Primary Concepts |
|---|---|---|
| 1 | Create the initial Minesweeper architecture and console application | Classes, models, board setup, separation of concerns |
| 2 | Develop an interactive and playable console version | User input, game loop, cell selection, validation |
| 3 | Implement recursive flood fill | Recursion, base cases, neighboring-cell logic |
| 4 | Create a Windows Forms GUI | Forms, buttons, events, visual board generation |
| 5 | Add game statistics and high-score tracking | Player data, scoring, persistence, additional forms |
| 6 | Add advanced features and custom enhancements | Independent design, testing, usability improvements |

---

## Application Architecture

Projects in this repository generally follow an N-layer structure.

```text
Presentation Layer
        ↓
Business Logic Layer
        ↓
Data Access Layer
        ↓
Files or In-Memory Data
```

### Presentation Layer

Responsible for:

- Displaying information
- Collecting user input
- Handling form and control events
- Calling business logic methods
- Avoiding direct data-management responsibilities

### Business Logic Layer

Responsible for:

- Application rules
- Calculations
- Validation
- Game logic
- Sorting and filtering
- Coordinating data operations

### Data Access Layer

Responsible for:

- Reading data
- Writing data
- JSON and CSV serialization
- Managing stored records
- Isolating file operations from the user interface

### Model Layer

Responsible for:

- Representing application data
- Defining properties and object state
- Passing structured data between layers

---

## Design Documentation

Each major activity may include:

- Flowcharts
- UML class diagrams
- Architecture diagrams
- Screenshots
- Written explanations
- Testing evidence

UML diagrams should identify:

- Class names
- Properties
- Fields
- Methods
- Parameters
- Return types
- Access modifiers
- Inheritance
- Composition
- Aggregation
- Associations between application layers

Flowcharts should identify:

- Program start and end
- User input
- Major processes
- Decisions
- Validation paths
- Method calls
- Data flow
- Error-handling paths
- Output displayed to the user

---

## Getting Started

### Prerequisites

Install the following before opening the projects:

- Microsoft Visual Studio
- The C# and .NET desktop development workload
- Git
- A GitHub account
- Any NuGet packages required by the individual project
- The Visual Studio version specified by the instructor

### Clone the Repository

Use the green **Code** button on the GitHub repository page to copy the repository URL. Then clone it with:

```bash
git clone <repository-url>
```

After cloning, open the downloaded repository folder in Visual Studio.

### Open a Project

1. Open Visual Studio.
2. Select **Open a project or solution**.
3. Browse to the activity or milestone folder.
4. Open the appropriate `.sln` file.
5. Restore NuGet packages when prompted.
6. Set the correct console or Windows Forms project as the startup project.
7. Build and run the solution.

### Run from the Command Line

For compatible projects:

```bash
dotnet restore
dotnet build
dotnet run --project path/to/ProjectName.csproj
```

---

## Running Tests

Projects that include xUnit tests can be tested through Visual Studio.

1. Open the solution.
2. Select **Test** from the Visual Studio menu.
3. Open **Test Explorer**.
4. Select **Run All Tests**.

Tests may also be executed from the command line:

```bash
dotnet test
```

Unit tests should verify individual logic components without requiring the graphical user interface to run.

---

## Git Workflow

A typical workflow for this repository is:

```bash
git status
git add .
git commit -m "Complete Milestone 1 board initialization"
git push origin main
```

Recommended commit messages should describe the work completed.

Examples:

```text
Add recursive flood-fill logic
Create Windows Forms Minesweeper board
Add xUnit tests for neighboring mine counts
Implement JSON high-score storage
Update UML class diagram
Fix input-validation errors
```

---

## Coding Standards

Projects should follow consistent C# coding practices.

- Use meaningful class, method, property, and variable names.
- Use PascalCase for classes, methods, and public properties.
- Use camelCase for local variables and parameters.
- Keep methods focused on a single responsibility.
- Avoid placing business logic directly inside form event handlers.
- Validate user input.
- Handle exceptions appropriately.
- Add comments when the purpose of code is not immediately clear.
- Remove unused code and unnecessary variables.
- Keep presentation, business logic, data access, and model classes separated.
- Write unit tests for reusable application logic.
- Update documentation when the implementation changes.

---

## Build Status

The repository is under active development during the CST-250 course.

| Component | Status |
|---|---|
| Activity 1 — Car Store | Planned |
| Milestone 1 — Minesweeper Architecture | Planned |
| Activity 2 — Chessboard | Planned |
| Milestone 2 — Interactive Console Game | Planned |
| Activity 3 — Recursion | Planned |
| Milestone 3 — Recursive Flood Fill | Planned |
| Activity 4 — Pizza Order System | Planned |
| Milestone 4 — Windows Forms GUI | Planned |
| Activity 5 — Whack-A-Mole | Planned |
| Milestone 5 — Statistics and High Scores | Planned |
| Activity 6 — Bible Verse Inventory | Planned |
| Milestone 6 — Advanced Features | Planned |

Update the status values to `In Progress` or `Complete` as work is finished.

---

## Screenshots

Screenshots of completed applications can be stored in the `Documentation/Screenshots` directory and displayed here.

Add completed application screenshots to the `Documentation/Screenshots` directory and reference them from this section when needed.

---

## Academic Use

This repository is intended to document my learning and coursework. Code should not be copied and submitted by another student as original work. Any external libraries, references, tutorials, or reused resources should be credited where appropriate.

---

## Course Information

**Course:** CST-250 — Programming in C# II  
**Institution:** Grand Canyon University  
**Term:** July–August 2026

---

## Acknowledgments

Course activities and milestone requirements are based on the CST-250 course materials provided by Grand Canyon University. Additional implementation details, source code, diagrams, tests, and documentation are created as part of the course learning process.
