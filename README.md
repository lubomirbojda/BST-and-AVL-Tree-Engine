# WHO PCR Tests Information System (BST & AVL Tree Engine)

[![C#](https://img.shields.io/badge/Language-C%23%2013-239120.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET 9](https://img.shields.io/badge/Framework-.NET%209.0-512BD4.svg)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
[![UI](https://img.shields.io/badge/UI-WPF%20(Windows)-0078D6.svg)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
[![IDE](https://img.shields.io/badge/IDE-Visual%20Studio%202022-purple.svg)](https://visualstudio.microsoft.com/)
[![Course](https://img.shields.io/badge/Course-Algorithms%20%26%20Data%20Structures%202-orange.svg)](https://fri.uniza.sk)

High-performance in-memory C# / WPF desktop application and custom **non-recursive** tree data structures library (`BST` and `AVL Tree`) designed for centralized tracking, querying, and epidemiological evaluation of PCR test results and patients.

This project was developed as a university semester project (**Semester Project S1**) for the **Algorithms and Data Structures 2 (AUS 2)** course at the Faculty of Management Science and Informatics, University of Žilina (**FRI UNIZA**).

---

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Data Structures & Multi-Level Architecture](#data-structures--multi-level-architecture)
- [Implemented Operations & Time Complexity](#implemented-operations--time-complexity)
- [Performance Benchmarks (BST vs. AVL vs. SortedSet)](#performance-benchmarks-bst-vs-avl-vs-sortedset)
- [Installation & How to Run](#installation--how-to-run)
- [GUI Controls & Usage](#gui-controls--usage)
- [Documentation](#documentation)
- [Screenshots](#screenshots)

---

## Overview

The system models an in-memory centralized epidemiological database for the World Health Organization (WHO) to record patients (`Person`) and their PCR test results (`PCRTest`) across regions (`kraje`), districts (`okresy`), and testing stations (`pracoviská`).

To achieve maximum throughput without external database engines or recursion overhead, the entire core is built from scratch around custom **strictly iterative (non-recursive) Binary Search Trees (`BST<T>`) and self-balancing AVL Trees (`AVLT<T>`)**. By nesting secondary AVL trees inside primary AVL tree nodes (multi-level indexing), all 21 domain operations execute in logarithmic or near-logarithmic time relative to dataset size.

---

## Key Features

- **100% Non-Recursive Tree Implementation:**
  - Custom generic `BaseBST<TNode, TData>`, `BST<T>`, and `AVLT<T>` implementations avoiding call-stack overhead.
  - Iterative `Insert`, `Delete` (leaf, single-child, and two-children replacement via in-order successor), `Find`, `FindMinimum`, `FindMaximum`, and pruned `FindInterval`.
  - Parent-pointer based `InOrder` traversal ($O(1)$ auxiliary memory without stack/recursion) and queue-based `LevelOrder` traversal.
  - Early-termination AVL rebalancing (`RebalanceAfterInsert` & `RebalanceAfterDelete`) using single ($L$, $R$) and double ($LR$, $RL$) rotations.
- **Multi-Level Composite AVL Indexing:**
  - 6 primary in-memory AVL trees cross-indexing patients, tests, regions, districts, and testing stations, with nested secondary AVL trees per entity for instantaneous range and top-$k$ queries.
- **Complete 21 Epidemiological & Administrative Operations:**
  - Time-interval queries for all or positive-only PCR tests filtered by district, region, station, or globally.
  - Active sickness evaluation (identifying patients within $X$ days of a positive test at any reference date), including sorting by cycle/test value (`Value`) or ranking districts/regions by active case count.
  - Cascading deletion of tests or patients (removing a patient automatically purges all associated PCR tests across all index trees).
- **Minimal-Footprint Level-Order CSV Persistence:**
  - Built-in `SaveToCSV` and `LoadFromCSV` directly on the tree structure using `LevelOrder` traversal so re-importing preserves tree shape and minimizes rebalancing overhead.
- **Synthetic Data Generator & Automated Stress Tester:**
  - One-click generator capable of populating **100,000 patients** and **500,000 PCR tests** across 200 regions, 1,000 districts, and 5,000 stations.
  - Randomized property-based verification (`TestAVLandBST`) and 10M-element benchmark suite (`TestOperationsTime`) comparing `BST<int>`, `AVLT<int>`, and .NET's `SortedSet<int>` (Red-Black Tree).

---

## Data Structures & Multi-Level Architecture

Because records must be queried by multiple composite keys (ID, timestamp, district, region, station, and descending test value), core domain entities (`Person` and `PCRTest`) are kept as lightweight data containers and referenced by specialized `IComparable<T>` wrapper classes inside **6 primary AVL trees**:

```
PCRTestsSystem
 ├── AVLT<PersonByID> (key: PersonID)
 │    ├── AVLT<PCRTestByID> (patient's tests by TestID)
 │    └── AVLT<PCRTestByDateAndTime> (patient's tests by DateAndTime, TestID)
 │
 ├── AVLT<PCRTestByID> (key: TestID — global test index)
 │
 ├── AVLT<PCRTestByDateAndTime> (key: DateAndTime, TestID — global chronological index)
 │
 ├── AVLT<PCRTestByDistrict> (key: DistrictID)
 │    ├── AVLT<PCRTestByDateAndTime> (district's tests by DateAndTime, TestID)
 │    └── AVLT<PCRTestByValueReverse> (district's tests by Value DESC, TestID)
 │
 ├── AVLT<PCRTestByRegion> (key: RegionID)
 │    └── AVLT<PCRTestByDateAndTime> (region's tests by DateAndTime, TestID)
 │
 └── AVLT<PCRTestByTestStation> (key: TestStationID)
      └── AVLT<PCRTestByDateAndTime> (station's tests by DateAndTime, TestID)
```

| Wrapper / Structure | Sorting Key(s) | Nested Subtrees | Purpose in System |
| :--- | :--- | :--- | :--- |
| `PersonByID` | `PersonID` (`string`, ordinal) | `AVLT<PCRTestByID>`, `AVLT<PCRTestByDateAndTime>` | $O(\log N)$ patient lookup and instant access to a patient's test history. |
| `PCRTestByID` | `TestID` (`int`) | *None* | Global unique PCR test lookup and CSV serialization. |
| `PCRTestByDateAndTime` | `DateAndTime`, tie-breaker `TestID` | *None* | Chronological ordering and pruned `[minDate, maxDate]` interval queries. |
| `PCRTestByValueReverse` | `Value` (descending via epsilon `DoubleComparator`), tie-breaker `TestID` | *None* | Direct descending iteration by test value (e.g., highest viral load per district). |
| `PCRTestByDistrict` | `DistrictID` (`int`) | `AVLT<PCRTestByDateAndTime>`, `AVLT<PCRTestByValueReverse>` | Groups tests by district for date-range and value-sorted queries. |
| `PCRTestByRegion` | `RegionID` (`int`) | `AVLT<PCRTestByDateAndTime>` | Groups tests by region for regional date-range and active sickness queries. |
| `PCRTestByTestStation` | `TestStationID` (`int`) | `AVLT<PCRTestByDateAndTime>` | Groups tests by workplace/station for interval reporting. |
| `TerrUnitBySickPeopleCount` | `SickPeopleCount`, tie-breaker `TerrUnitID` | *None* | Auxiliary AVL wrapper used to sort districts/regions by active sick count. |

---

## Implemented Operations & Time Complexity

All 21 required operations from the assignment specification are implemented in `PCRTestsSystem`:

| # | Operation Description | Time Complexity | Variables Legend |
| :-: | :--- | :--- | :--- |
| **1** | Insert a PCR test result into the system | $O(\log n)$ | $n$ = total tests in system |
| **2** | Find a specific test (`TestID`) for a given patient (`PersonID`) | $O(\log n + \log k)$ | $n$ = total persons, $k$ = patient's tests |
| **3** | List all PCR tests for a patient ordered by date and time | $O(\log n + k)$ | $n$ = total persons, $k$ = patient's tests |
| **4** | List all **positive** tests in a date interval for a district | $O(\log n + k)$ | $n$ = unique districts, $k$ = district tests in interval |
| **5** | List **all** tests in a date interval for a district | $O(\log n + k)$ | $n$ = unique districts, $k$ = district tests in interval |
| **6** | List all **positive** tests in a date interval for a region | $O(\log n + k)$ | $n$ = unique regions, $k$ = region tests in interval |
| **7** | List **all** tests in a date interval for a region | $O(\log n + k)$ | $n$ = unique regions, $k$ = region tests in interval |
| **8** | List all **positive** tests in a date interval (globally) | $O(\log n + k)$ | $n$ = total tests, $k$ = tests in interval |
| **9** | List **all** tests in a date interval (globally) | $O(\log n + k)$ | $n$ = total tests, $k$ = tests in interval |
| **10** | List sick persons in a district at a given date (sick for $X$ days) | $O(\log n + \log k + m \log h)$ | $n$ = districts, $k$ = tests in district, $m$ = tests in window, $h$ = sick persons |
| **11** | List sick persons in a district at a given date **ordered by test value** | $O(\log n + k \log m)$ | $n$ = districts, $k$ = tests in district, $m$ = sick persons in window |
| **12** | List sick persons in a region at a given date (sick for $X$ days) | $O(\log n + \log k + m \log h)$ | $n$ = regions, $k$ = tests in region, $m$ = tests in window, $h$ = sick persons |
| **13** | List all sick persons globally at a given date (sick for $X$ days) | $O(\log n + k \log m)$ | $n$ = total tests, $k$ = tests in window, $m$ = sick persons in window |
| **14** | Find one sick person with the **highest test value from each district** at date | $O(n + k \log m)$ | $n$ = total tests, $k$ = unique districts, $m$ = total persons |
| **15** | List all districts ordered by count of sick persons at a given date | $O(n \cdot (\log n + \log k + m \log h))$ | $n$ = districts, $k$ = district tests, $m$ = window tests, $h$ = sick persons |
| **16** | List all regions ordered by count of sick persons at a given date | $O(n \cdot (\log n + \log k + m \log h))$ | $n$ = regions, $k$ = region tests, $m$ = window tests, $h$ = sick persons |
| **17** | List all tests in a date interval at a specific testing station | $O(\log n + k)$ | $n$ = unique stations, $k$ = station tests in interval |
| **18** | Find a PCR test by its unique `TestID` | $O(\log n + \log k)$ | $n$ = total tests, $k$ = total persons |
| **19** | Insert a new person into the system | $O(\log n)$ | $n$ = total persons |
| **20** | Permanently delete a PCR test result by `TestID` | $O(\log n)$ | $n$ = total tests |
| **21** | Delete a person by `PersonID` along with all their PCR tests | $O(\log n + k \log m)$ | $n$ = total persons, $k$ = person's tests, $m$ = total tests |

---

## Performance Benchmarks (BST vs. AVL vs. SortedSet)

The custom non-recursive `BST<int>` and `AVLT<int>` structures were benchmarked against .NET's standard `SortedSet<int>` (Red-Black Tree) according to the assignment specification:

| Operation | Dataset / Workload | `BST<int>` | `AVLT<int>` | `SortedSet<int>` (.NET) |
| :--- | :--- | :---: | :---: | :---: |
| **Insert** | 10,000,000 unique random keys | 10,814.79 ms (1.08 $\mu$s/op) | 13,597.94 ms (1.36 $\mu$s/op) | **9,689.26 ms (0.97 $\mu$s/op)** |
| **Delete** | 2,000,000 random existing keys | **2,005.89 ms (1.00 $\mu$s/op)** | 2,463.02 ms (1.23 $\mu$s/op) | 9,274.05 ms (4.64 $\mu$s/op) |
| **Point Find** | 5,000,000 random existing keys | 4,305.77 ms (0.86 $\mu$s/op) | **3,650.04 ms (0.73 $\mu$s/op)** | 4,176.61 ms (0.84 $\mu$s/op) |
| **Interval Find** | 1,000,000 queries ($\ge 500$ hits each) | 53,906.83 ms (53.91 $\mu$s/op) | 50,950.74 ms (50.95 $\mu$s/op) | **8,118.95 ms (8.12 $\mu$s/op)** |
| **Find Minimum** | 2,000,000 operations | **31.33 ms (0.016 $\mu$s/op)** | 39.76 ms (0.020 $\mu$s/op) | 155.44 ms (0.078 $\mu$s/op) |
| **Find Maximum** | 2,000,000 operations | **22.82 ms (0.011 $\mu$s/op)** | 252.22 ms (0.126 $\mu$s/op) | 244.22 ms (0.122 $\mu$s/op) |

---

## Installation & How to Run

### Prerequisites

- **Operating System:** Windows 10 / 11
- **SDK:** [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (`net9.0` and `net9.0-windows`)
- **IDE:** [Visual Studio 2022](https://visualstudio.microsoft.com/) (with *.NET desktop development* workload)

### Steps

1. **Clone the repository:**
   ```bash
   git clone https://github.com/lubomirbojda/BST-and-AVL-Tree-Engine
   cd BST-and-AVL-Tree-Engine
   ```

2. **Open the solution:**
   - Double-click `AUS2_S_1.sln` to open the solution in Visual Studio 2022.

3. **Choose the Startup Project:**
   - **To launch the WPF Desktop Application:** Right-click `GUI_PCR_System` in Solution Explorer &rarr; **Set as Startup Project**.
   - **To run the Tree Tester & Benchmarks (Console):** Right-click `AUS2_S_1` in Solution Explorer &rarr; **Set as Startup Project** (toggle `tester.TestOperationsTime()` or `tester.TestAVLandBST()` in `Program.cs`).

4. **Build & Run:**
   - Select `Release` (recommended for large datasets/benchmarks) or `Debug` configuration and press `F5` or `Ctrl + F5`.
   - Or run directly from the CLI:
     ```bash
     # Launch the WPF GUI application
     dotnet run --project GUI_PCR_System -c Release

     # Run the console functional & performance tester
     dotnet run --project AUS2_S_1 -c Release
     ```

---

## GUI Controls & Usage

The WPF application (`GUI_PCR_System`) provides an intuitive interface divided into top-level data management and four domain views:

### 1. Top Menu (`File`)
- **`File -> Generate`:** Populates the in-memory system with **100,000 random patients** and **500,000 PCR tests** across 200 regions, 1,000 districts, and 5,000 testing stations.
- **`File -> Save as...`:** Selects a target folder and exports all records in `LevelOrder` into `persons.csv` and `pcr_tests.csv`.
- **`File -> Open`:** Selects a folder and imports `persons.csv` and `pcr_tests.csv` back into the multi-level AVL trees.

### 2. Category Selector (`Searching for:`)
Switch the active workspace using the dropdown menu:
- **PCR Tests (`PCRTestsOperations`):**
  - Insert new test, Find test for person, Find all tests for person, Find all tests in district/region/globally/station in time interval (with checkbox for *Only positive* where applicable), Find test by ID, Delete test.
  - Displays full test details alongside associated patient data (`FirstName`, `LastName`, `BirthDate`, `PersonID`) and total result count (`Results count`).
- **Persons (`PersonsOperations`):**
  - Find sick persons in district/region/globally at date (with configurable $X$ days of positiveness and optional sorting by test value), Find one sick person with highest test value from every district at date, Insert person, Delete person.
- **Districts (`DistrictsOperations`):**
  - Find all districts ordered by sick people count at a given date.
- **Regions (`RegionsOperations`):**
  - Find all regions ordered by sick people count at a given date.

---

## Documentation

Complete technical documentation (in Slovak) is included in the repository root:

- **[Project Documentation](https://github.com/lubomirbojda/BST-and-AVL-Tree-Engine/blob/main/Documentation.pdf)**
  - Visual walkthrough of non-recursive AVL rotations ($L$, $R$, $LR$, $RL$) and balance factor updates in `RebalanceAfterInsert` and `RebalanceAfterDelete`
  - Detailed empirical benchmark tables and charts (`BST` vs. `AVL` vs. `SortedSet`)
  - Multi-level AVL tree architecture diagrams and wrapper class explanations
  - Asymptotic time complexity proofs for all 21 operations and full UML Class Diagram

> **Note:** PDF documentation is written in the **Slovak language**.

---

## Screenshots

<!-- Replace or add screenshots of the WPF GUI and benchmark charts below -->

<p align="center">
  <img width="857" height="455" alt="image" src="https://github.com/user-attachments/assets/7d6e35b3-29de-4f24-bede-6903d60cdabb" />

</p>
