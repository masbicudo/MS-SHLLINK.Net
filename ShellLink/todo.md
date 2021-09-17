# To Do

- Continue separating data from behavior
	- Place data objects inside `DataObjects` namespace
	- Actuator is a class that can create, load, write, validate, fix and do other operations over data objects

	[ ] Rename loaders as actuators
	[ ] Move ItemId data objects to `DataObjects` namespace
	[ ] Organize PropertyStore, are they actuators?
	[ ] Organize actuators, separating them from enums and structs
	[ ] Loaders should be renamed as actuators

- Validators and fixers
	- Used to validade data objects
	- Used to fix incorrect data objects
	- Each fix can have options to tell how the fix should be done
