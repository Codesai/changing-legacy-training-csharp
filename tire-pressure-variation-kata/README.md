# Tire Pressure Monitoring System Variation

## Goal
Killing all surviving mutants.

## Tools
### Install dependencies and tools

`dotnet restore`

### Coverage

`dotnet msbuild -target:Coverlet`

### Mutation testing

`dotnet stryker --open-report`

## References

Based on an exercise from [Luca Minudel](https://twitter.com/lukadotnet?lang=en)'s [TDD with Mock Objects And Design Principles exercises](https://github.com/lucaminudel/TDDwithMockObjectsAndDesignPrinciples)
