Feature: Navigation to About EHU Page

  As a user,
  I want to navigate to the About EHU page
  So that I can verify the page title and header.

  Scenario: Verify About EHU Page Navigation
    Given I am on the homepage "https://en.ehu.lt/"
    When I click the "About" link
    Then the page title should be "About"
    And the page header should contain "About"