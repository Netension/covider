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