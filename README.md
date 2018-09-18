# SimpleBank
A simple banking ecosystem to demonstrate unit testing and test driven development. SimpleBank consists of types Bank, Account, Money and Person.

##### Step 1: Write Unit Tests
- Branch 1: master
- There are just empty unit tests. Every test indicates a requirement.
- **Tasks:** Write unit tests to fullfil the given requirements.
- 
##### Step 1: Implement Features
- Branch 1: withtests
- The branch contains working unit tests.
- Most BankTests are failing with NotImplementedExceptions.
- **Tasks:** Implement all Bank methods in order to fix all unit tests.

##### Step 2: Bug fixing using TDD
- Branch 2: transferbug
- SimpleBank now contains succeeding tests, all green!
- **Tasks:** Bank customers have reported a bug. Whenever money is transferred from a valid source account to a non-existent target account the Bank throws an exception. However, the money is removed from the source account! 1) Write a unit test to reproduce the bug, 2) find and fix the bug.