Dispatcher
==========
Most of the credit goes to https://gist.github.com/jageall - initially this will be more or less a copy of the infrastructure he's described to me recently. When I start making changes of my own, I will make that clear.

#What's the point?
The purpose of this repo is to demonstrate a basic implementation of the Actor Model and to act as a starting point for any future developments that I might choose to implement using this pattern. What you see here is *by no means*:
* Original
* In any way comprehensive
* Well-written
* (Assumed) Correct
* Used optimally in any of the supplied examples

#Examples
**Printers** models an office with a number of employees and a number of printers. Employees periodically submit print jobs (by publishing a **PrintJob** message) and this work is distributed equally between each available printer. Printers publish **PagePrinted** messages when each page is printed, and **OutOfPaper** messages when they are - ta-da - out of paper. The Porter subscribes to OutOfPaper and publishes RefillPaper in response. In this manner, jobs are continuously serviced even as printers periodically go out of service and are brought back online.
