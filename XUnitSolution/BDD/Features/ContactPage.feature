Feature: Contact Page Information Verification

  As a user,
  I want to validate the contact information on the Contact Page
  So that I can ensure the presence of a valid email, phone number, and Facebook link.

  Scenario: Verify Contact Page contains correct information
    Given I am on the contact page "https://en.ehu.lt/contacts/"
    Then the page should contain a valid email matching the pattern "[\w\.\-]+@[\w\-]+(\.[\w]{2,3})+"
    And the page should contain a valid phone number matching the pattern "\+\d{1,3}\s\d\s\d{3}\s\d{4}"
    And the page should contain a reference to "Facebook"
