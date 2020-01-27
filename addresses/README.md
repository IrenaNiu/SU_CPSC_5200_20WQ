# CPSC-5200-01 Software Architecture and Design Project

## The problem definition

A very common problem in enterprise software development is related to handling postal addresses. Nearly every country has a particular, and peculiar, way to structure a postal address. [This](http://www.bitboost.com/ref/international-address-formats.html#Formats) is a high-level overview of the problem complete with details for a number of countries. We will attempt to address (no pun intended) this problem by delivering the following:

- A web-based user interface (a form) that can capture a country-specific address format entered by an end user
  - The form must dynamically adjust to capture the address
  - Validation of the address formats and related data must occur in a reasonable time for the user
  - The end user should be able to select data from appropriate user interface elements and not just type everything in free-form
  - Where possible, default values and constrained lists should be presented to the user
- A way for a user to search for a given address based on the country-specific format
- A way for a user to search across countries to find "matching" addresses and display them in the application
- An API callable via HTTP (curl or postman) because we might want to sell access to this application
  - Documentation for the API, preferably available alongside the API itself (think swagger / OpenAPI)

Your application should be able to deal with all of the countries listed in the above link. An address search should return a result to the end user in < 75ms and should support at least 1000 concurrent requests (read, write, and search). Your "database" should be seeded with 1,000,000 addresses of various formats spread across the countries on a per-capita basis (or approximately based on number of residents in each country). You are responsible for writing the code that seeds your database, and you can decide if you exercise your API to do so or if you want to "go around" your API and write the data in some other way.

## What your team needs to deliver

1. A high-level architecture explaining and justifying your architecture style / pattern choice to the component level (18-Feb)
2. A preliminary design document explaining how your team will create the solution using your style / pattern choice (25-Feb)
3. A detailed design document covering design patterns, algorithms, technologies, and trade-offs (05-Mar)
4. A presentation of the above to the class and a demonstration of your working application exercising UI and API (10- and 12-Mar)

## What you have to turn-in

You will need to turn in, during the quarter, three homework assignments that describe the overall architecture, the high-level design, and the detailed design(s) of your project. Each team will submit one copy of the homework assignments. This means that your grade is based on your team's performance, as a whole.

Before class on 10-Mar (before the first presentation night) all submissions must be completed and made available to me. That is, I want to see the final presentation materials and some level of proof that your game is complete.

## What you have to present

You will present to the class your architecture, high-level, and low-level designs; novel solutions to any problems you encountered; your team structure and roles; and a demonstration of your API and UI.

## Scoring

Overall this project will be worth 30 points (this is nearly half of your quarter grade). Thirty-five of the points are in the form of the project itself. Fifteen of the points will be in the form of homework assignments. This is broken down as follows:

- 15 points for the homework components

  - 5 points for the architecture one-pager
  - 5 points for the preliminary design documentation
  - 5 points for the detailed design documentation

- 15 remaining points for the project overall
  - 5 points for meeting non-functional requirements (usability, availability, performance, etc.)
  - 10 points for presentation and demo

## How the project is graded

Each team will be provided with a score sheet for each other team and one for themselves. Each team will score the other teams on the above presentation components (the 30 overall points and any extra credit points). Each team will also score their performance on the presentation component.

I will compile these as inputs to my overall assessment to gauge how the class, as a whole, feels about the presentations. _I reserve the right to bring in outside resources to evaluate your application as well._ After all presentations are completed on the 12th I will compile and score all submissions and return them by the final exam.
