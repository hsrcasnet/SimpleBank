# SimpleBank
A simple banking ecosystem to demonstrate unit testing and test driven development. SimpleBank consists of types Bank, Account, Money and Person.

##### Step 1: Write Unit Tests
- Branch 1: master
- There are just empty unit tests. Every test indicates a requirement.
- **Tasks:** Write unit tests to fullfil the given requirements.

##### Step 1: Implement Features
- Branch 1: withtests
- The branch contains the code for all unit tests. Most BankTests are failing with NotImplementedExceptions.
- **Tasks:** Implement all Bank methods in order to fix all unit tests.

##### Step 2: Bug fixing using TDD
- Branch 2: transferbug
- SimpleBank now contains succeeding tests, all green!
- **Tasks:** Bank customers have reported a bug. Whenever money is transferred from a valid source account to a non-existent target account the Bank throws an exception. However, the money is removed from the source account! 1) Write a unit test to reproduce the bug, 2) find and fix the bug.


##### Additional Exercises
- The bank gets a new regulatory requirement: Each transaction (Transfer, Withdraw and Deposit) has to charge a transaction fee on top of the transfer value. Following fees apply from January 1 of next year:
	- Customer origin fee: Switzerland 1%, Germany 0.8%
	- Accounts value fee: 0% if <= 1'000'000, 1% if > 1'000'000
- Extend Money with currency. Transfers from one currency to another have to be recalculated based on the current interbank exchange rate. Consume a forex service to retrieve exchange rates.
- Implement operators for Money so that we can compare (==, !=, >, <) between Money objects.
