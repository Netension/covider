Feature: Application management

@AM-Application
Scenario: [INT-AM001][Success]: Create new application
	Given I do not have any application
	When I call /api/application/am001 POST action
	Then am001 should be created
	
@AM-Application
Scenario: [INT-AM002][Failure]: Create existing application
	Given I have am002 application
	When I call /api/application/am002 POST action
	Then Response should be 400
	
@AM-Application
Scenario: [INT-AM003][Success]: Delete application
	Given I have am003 application
	When I call /api/application/am003 DELETE action
	Then am003 should be deleted	

@AM-Application
Scenario: [INT-AM004][Failure]: Delete not existing application
	Given I do not have any application
	When I call /api/application/am004 DELETE action
	Then Response should be 400

@AM-Application
Scenario: [INT-AM005][Success]: Get all applications
	Given I have the following applications
		| Name    |
		| am005-1 |
		| am005-2 |
	When I call /api/application GET action
	Then Should be response with the following applications
		| Name    |
		| am005-1 |
		| am005-2 |