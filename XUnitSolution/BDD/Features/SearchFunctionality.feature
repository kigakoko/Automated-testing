Feature: Search Functionality

  As a user,
  I want to perform a search on the website
  So that I can find relevant results based on my search term.

  Scenario: Verify search functionality
    Given I am on the search page "https://en.ehu.lt/"
    When I perform a search with the term "study programs"
    Then the current URL should contain "?s=study%20programs"
    And the search results should indicate that results were found
