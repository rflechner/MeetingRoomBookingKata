Feature: RoomReservation
	Here we will specify business rules concerning meeting room reservations requests.
	Rooms and slots numbers are 0 based.

Scenario: Booking an available room
	Given 5 rooms
	And time slots have 1 hour of duration
	When no slots are reserved
	Then user can book slot number 12 of day '2018-01-02' for room 2

	Given 5 rooms
	And time slots have 1 hour of duration
	When following slots are reserved
	| RoomNumber | Day        | SlotNumber |
	| 1          | 2018-01-02 | 10         |
	| 1          | 2018-01-02 | 11         |
	| 1          | 2018-01-02 | 12         |
	| 2          | 2018-01-02 | 11         |
	| 2          | 2018-01-02 | 13         |
	Then user can book slot number 12 of day '2018-01-02' for room 2
	Then user can not book slot number 11 of day '2018-01-02' for room 2


