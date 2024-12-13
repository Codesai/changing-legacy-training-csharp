### Kata Objective

To practice how to add a feature to legacy code.

## An ultra urgent feature arrives 
======================================

The `WavExtractor32v` class needs to be updated to exclude files containing a keyword.
- Main receives multiples args
- First argument is the directory where the wav files are located.
- The other arguments are optional parameters, and they use the following pattern "name=value"
- Exclude filenames with a keyword is an optional parameter with name "exclude" (example: "exclude=keyword").

## Constraints

- You cannot modify caller code.
- You don’t have time to test/refactor `WavExtractor32v` or `Main`.


#  Useful commands

- Run this command to install stryker (only the first time):

> `$ dotnet tool restore`
 
- Run this command to execute coverage:

> `$ dotnet msbuild -target:Coverlet`
 
- Run this command to execute mutation testing:

> `$ dotnet stryker --open-report`
