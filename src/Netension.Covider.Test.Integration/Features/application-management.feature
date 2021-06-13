Feature: Application management

@AM-Application
Scenario: [AM001][Success]: Create new application
	Given I do not have any application
	When I call /api/application/AM001 POST action
	Then AM001 should be created
	
@AM-Application
Scenario: [AM002][Failure]: Create existing application
	Given I have AM002 application
	When I call /api/application/AM002 POST action
	Then Response should be 400