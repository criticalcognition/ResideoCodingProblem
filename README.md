# Resideo Coding Problem

This project solves the Resideo take-home coding problem by analyzing weather data from three cities (Los Angeles, New York, and London) and finding the city with the lowest minimum temperature for each day in the specified date range.

## Problem Statement

Given weather data for three cities covering the period 03/28/2019 - 04/02/2019, the program generates a JSON document containing the city with the lowest minimum temperature for each of the 6 days. Each row contains the date, minimum temperature, maximum temperature, and name of the city, sorted in ascending date order.

## Project Structure

```
ResideoCodingProblem/
├── ResideoCodingProblem/                    # Main application project
│   ├── Models/
│   │   ├── WeatherData.cs                   # Individual weather data record
│   │   ├── CityWeatherData.cs               # City-level weather data record
│   │   └── OutputRecord.cs                  # Output format record
│   ├── Services/
│   │   ├── WeatherDataProcessorBase.cs      # Abstract base class with shared processing logic
│   │   ├── IWeatherDataProcessor.cs         # Base interface for weather data processing
│   │   ├── IWeatherDataService.cs           # Interface for local file weather data operations
│   │   ├── WeatherDataService.cs            # Implementation of local file weather data processing
│   │   ├── IWeatherDataUrlService.cs        # Interface for URL-based weather data operations
│   │   ├── WeatherDataUrlService.cs         # Implementation of URL-based weather data processing
│   │   ├── IJsonOutputService.cs            # Interface for JSON output operations
│   │   └── JsonOutputService.cs             # Implementation of JSON serialization
│   ├── JsonFiles/                           # Input JSON data files
│   │   ├── 401024.json                      # New York weather data
│   │   ├── 746560.json                      # Los Angeles weather data
│   │   └── 77536.json                       # London weather data
│   ├── Program.cs                           # Main application entry point
│   └── README.md                            # This file
└── ResideoCodingProblem.Tests/              # Test project
    ├── Models/
    │   ├── WeatherDataTests.cs              # Tests for WeatherData model
    │   ├── CityWeatherDataTests.cs          # Tests for CityWeatherData model
    │   └── OutputRecordTests.cs             # Tests for OutputRecord model
    ├── Services/
    │   ├── WeatherDataProcessorBaseTests.cs # Tests for processing logic
    │   └── JsonOutputServiceTests.cs        # Tests for JSON output service
    └── Integration/
        └── WeatherDataProcessingIntegrationTests.cs # End-to-end integration tests
```

## Architecture

The solution follows .NET best practices with:

- **Separation of Concerns**: Models, services, and main program are separated
- **Single Responsibility**: Each model file contains one record type
- **Inheritance**: Abstract base class eliminates code duplication between data sources
- **Interface Segregation**: Clean interface hierarchy with shared processing capability
- **Dependency Injection**: Services are designed with interfaces for testability
- **Async/Await**: File and HTTP operations are performed asynchronously
- **Error Handling**: Comprehensive exception handling throughout
- **C# Records**: Modern C# features for immutable data models
- **Multiple Data Sources**: Support for both local files and remote URLs
- **DRY Principle**: Shared processing logic is centralized in the base class
- **Test-Driven Development**: Comprehensive test coverage with unit and integration tests

## Class and Interface Hierarchy

### Interface Hierarchy
```
IWeatherDataProcessor (base)
├── IWeatherDataService (local file operations)
└── IWeatherDataUrlService (URL-based operations)
```

### Class Hierarchy
```
WeatherDataProcessorBase (abstract)
├── WeatherDataService (inherits from base)
└── WeatherDataUrlService (inherits from base)
```

Both concrete classes inherit the shared `ProcessWeatherData()` and `FindCityWithLowestMinTemp()` methods from the base class, while implementing their own data loading logic. The interfaces follow the same inheritance pattern, with `IWeatherDataProcessor` providing the common processing capability.

## Data Models

- **WeatherData**: Represents individual weather entries with date, min/max temperatures
- **CityWeatherData**: Represents city-level weather data with title and consolidated weather array
- **OutputRecord**: Represents the final output format with date, temperatures, and city name

All models use proper C# PascalCase naming with `JsonPropertyName` attributes for JSON serialization compatibility.

## Testing

### Test Technologies

The project includes a comprehensive test suite built with:

- **xUnit**: Modern, extensible testing framework for .NET
- **Moq**: Popular mocking framework for creating test doubles
- **C# 12 Features**: Collection expressions, raw string literals, and primary constructors
- **Async/Await Testing**: Full support for testing asynchronous operations

### Test Structure

```
ResideoCodingProblem.Tests/
├── Models/                                  # Unit tests for data models
│   ├── WeatherDataTests.cs                 # JSON serialization/deserialization tests
│   ├── CityWeatherDataTests.cs             # Complex model tests
│   └── OutputRecordTests.cs                # Output format tests
├── Services/                                # Unit tests for business logic
│   ├── WeatherDataProcessorBaseTests.cs    # Core processing logic tests
│   └── JsonOutputServiceTests.cs           # JSON output service tests
└── Integration/                             # End-to-end tests
    └── WeatherDataProcessingIntegrationTests.cs # Complete workflow tests
```

### Test Coverage

- **Model Tests**: JSON serialization/deserialization, record equality, data validation
- **Service Tests**: Business logic, error handling, edge cases
- **Integration Tests**: Complete workflow testing with file I/O operations

### Running Tests

#### Run All Tests
```bash
# From the solution root directory
dotnet test

# Or from the test project directory
cd ResideoCodingProblem.Tests
dotnet test
```

#### Run Specific Test Categories
```bash
# Run only model tests
dotnet test --filter "FullyQualifiedName~Models"

# Run only service tests
dotnet test --filter "FullyQualifiedName~Services"

# Run only integration tests
dotnet test --filter "FullyQualifiedName~Integration"
```

#### Run Tests with Verbose Output
```bash
dotnet test --verbosity normal
```

#### Run Tests in Parallel
```bash
dotnet test --maxcpucount:0
```

## How to Run

### Prerequisites
Ensure you have .NET 9.0 or later installed

### Command Line Options

The application supports the following command line options:

```bash
# Load data from local JsonFiles directory (default)
dotnet run

# Load data from S3 URLs
dotnet run -- --url
dotnet run -- -u

# Show help
dotnet run -- --help
```

### Data Sources

1. **Local Files (Default)**: Reads all JSON files from the `JsonFiles/` directory
2. **S3 URLs**: Downloads data from the following URLs:
   - New York: `https://s3.amazonaws.com/connectedsavings.com/coding/weather/401024.json`
   - Los Angeles: `https://s3.amazonaws.com/connectedsavings.com/coding/weather/746560.json`
   - London: `https://s3.amazonaws.com/connectedsavings.com/coding/weather/77536.json`

## Output

The program outputs a JSON array containing the city with the lowest minimum temperature for each day:

```json
[
  {
    "date": "2019-03-28",
    "min_temp": 0.195,
    "max_temp": 5.97,
    "city": "New York"
  },
  {
    "date": "2019-03-29",
    "min_temp": 2.6300000000000003,
    "max_temp": 17.42,
    "city": "London"
  }
  // ... additional days
]
```

## Key Features

- **Date Range Processing**: Handles the specific date range (03/28/2019 - 04/02/2019)
- **Temperature Comparison**: Finds the city with the lowest minimum temperature for each day
- **JSON Output**: Produces properly formatted JSON with exact field names
- **Error Handling**: Graceful handling of missing files, network errors, and parsing errors
- **Extensible Design**: Easy to add more cities or modify processing logic
- **Multiple Data Sources**: Support for both local files and remote URLs
- **Command Line Interface**: Flexible command line options for different data sources
- **Dynamic File Loading**: Automatically discovers and loads all JSON files from the local directory
- **Code Reuse**: Shared processing logic eliminates duplication through inheritance
- **Clean Model Structure**: Each data model is in its own file for better organization
- **Interface Inheritance**: Clean interface hierarchy with shared processing capability
- **Comprehensive Testing**: Full test coverage with unit and integration tests

## Error Handling

The application handles various error scenarios:

- **Missing Files**: Gracefully handles missing JSON files with informative messages
- **Network Issues**: Handles HTTP timeouts and connection errors when loading from URLs
- **Invalid JSON**: Provides clear error messages for malformed JSON data
- **Empty Data**: Checks for empty data sets and provides appropriate feedback

## Development Workflow

1. **Run Tests**: Always run tests before making changes
   ```bash
   dotnet test
   ```

2. **Make Changes**: Implement new features or bug fixes

3. **Add Tests**: Write tests for new functionality

4. **Run Tests Again**: Ensure all tests pass
   ```bash
   dotnet test
   ```

5. **Run Application**: Test the application manually
   ```bash
   dotnet run
   dotnet run -- --url
   ``` 