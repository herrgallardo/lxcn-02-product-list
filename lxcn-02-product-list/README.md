# Product List Application

## Course Context

- **Program**: Arbetsmarknadsutbildning - IT Påbyggnad/Programmerare
- **Course**: C# .NET Fullstack System Developer
- **Minitask**: Weekly Assignment #2 - Product List Application

## Learning Objectives

This console application demonstrates key C# programming concepts:

- Object-Oriented Programming
- Console Application Development
- Input Validation
- Error Handling
- LINQ Operations
- Basic Data Management

## Overview

A console-based product management application built as part of a C# learning exercise, focusing on creating a robust and user-friendly inventory management system.

## Features

- Add products with detailed validation
- View products sorted by price
- Search products by name or category
- Real-time input validation
- Error handling

## Key Components

### Classes

- `Product`: Individual product representation
- `ProductManager`: Product collection management

## Validation Rules

- Category: Required, max 25 characters
- Product Name: Required, max 25 characters
- Price: Non-negative, maximum 1,000,000

## Technical Skills Demonstrated

- C# programming
- Object-Oriented Design
- Exception handling
- LINQ
- Console application development

## Application Flow

1. Main Menu
   - Add products
   - View products
   - Search products
   - Quit application

## Requirements

- .NET SDK 6.0 or later
- Basic understanding of C# programming

## How to Run

```bash
dotnet build
dotnet run
```

## Usage Examples

### Adding a Product

Input example:

```
Enter Product details (or 'q' to return to main menu)
Category: Electronics
Product Name: Smartphone
Price: 599.99
```

### Searching Products

Search example:

```
Enter search term (category or product name): Smart
Found 2 product(s) matching 'Smart'
```

## Learning Notes

This application is part of a comprehensive C# .NET Fullstack System Developer training program, designed to build practical programming skills through hands-on mini-projects.

## Educational Program Details

- **Type**: Arbetsmarknadsutbildning (Labor Market Training)
- **Focus**: IT Påbyggnad/Programmerare (IT Advanced/Programmer)
- **Course**: C# .NET Fullstack System Developer

## Potential Improvements for Future Learning

- Implement persistent storage
- Add more advanced search capabilities
- Create unit tests
- Develop a graphical user interface
