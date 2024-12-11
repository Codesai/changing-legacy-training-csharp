Kata Objective
======================================
Add the new requirement:  the "Conjured" item.

Before to changing the legacy code we need to cover its current behaviour to ensure we don't introduce any bug into production.

Gilded Rose Requirements Specification 
======================================
It's lost!! :_(


New Requirement 
======================================

We have recently signed a supplier of conjured items. This requires an update to our system:

	- "Conjured" items degrade in Quality twice as fast as normal items


#  Useful commands

- Run this command to install stryker (only the first time):

> `$ dotnet tool restore`
 
- Run this command to execute coverage:

> `$ dotnet msbuild -target:Coverlet`
 
- Run this command to execute mutation testing:

> `$ dotnet stryker --open-report`
