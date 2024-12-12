Feature: Language Change Functionality

  As a user,
  I want to change the website language to Lithuanian
  So that I can view the content in the Lithuanian language.

  Scenario: Verify language change to Lithuanian
    Given I am on the language change homepage "https://en.ehu.lt/"
    When I click the language switcher
    And I select the Lithuanian language
    Then the current URL should contain "https://lt.ehu.lt/"
    And the header text should contain "Kodėl EHU?"
