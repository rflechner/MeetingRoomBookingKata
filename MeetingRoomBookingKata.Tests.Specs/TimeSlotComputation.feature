Feature: TimeSlotComputation
	The goal of this feature is to compute time slots available in a day

Scenario: Computing all slots in a day
	Given A period of one day
	And Slots has 1 hour of duration
	When I compute all available slots
	Then the result should be 24 slots
