# CPSC-5200-01 Software Architecture and Design Project

This is a multi-person / team project. We will divide the class up into eight teams of five students each. This project will require architecture, design, and coding skills (at the UI, server, network, and data tiers). As such, I will ask that you choose teams that can cover those components. I will not be providing any architecture or design (component) requirements on the project; that's up to you.

Components of the project will be turned in as group homework during the quarter. You will then be required to present your overall project design, show a demo of the project, and _allow your fellow students to interact with your project_. That is, other students in the class will play your game.

## The project

The project is a multi-user, distributed version of the classic [Space Invaders](https://en.wikipedia.org/wiki/Space_Invaders) game. Instead of a single user battling the invaders your game will allow for up to _four_ distributed players to play in turn. Each player sees a live version of the game as the action is taking place.

However, this is a notch up from the original game which had a single CPU and a single player (other players stood by, quarters in hand, to take their turn). You are building a software service that allows a large number of simultaneous and independent games to take place. Your game will remember high scores across the population of games and players over all time.

I will provide additional non-functional requirements during the quarter (don't worry that you don't know what that really means today, you will). The main requirements are to provide the game itself as documented above. There will be some additional requirements that I will provide during the quarter. This is because I want to make the project somewhat like the real-world where requirements slightly (grin) change during the project lifecycle.

The first requirement is that your game will need some mechanism to invite players and actually start a game. This is called [attract mode](https://en.wikipedia.org/wiki/Glossary_of_video_game_terms#attract_mode). You'll discover more requirements as you're going.

## What you have to turn-in

You will need to turn in, during the quarter, three homework assignments that describe the overall architecture, the high-level design, and the detailed design(s) of your project. Each team will submit one copy of the homework assignments. This means that your grade is based on your team's performance, as a whole.

Before class on 10-Mar (before the first presentation night) all submissions must be completed and made available to me. That is, I want to see the final presentation materials and some level of proof that your game is complete. To make this a little more fair I will give two additional / bonus points to the four teams that present on 10-Mar to offset the additional two days that the other teams _might_ get because they're presenting on 10-Mar.

## What you have to present

You will present to the class your architecture, high-level, and low-level designs; novel solutions to any problems you encountered; your team structure and roles; and a demonstration of your game with two simultaneous games and with two players taking turns in the same game.

At this point in the quarter I'm still operating under the assumption that you can form groups that can do the architecture, design, and coding for the entire project (this means a working Space Invaders game, not just the plumbing). However, we will discuss this in class during the first week and adjust this project accordingly.

## Scoring

Overall this project will be worth 30 points (this is nearly half of your quarter grade). Thirty-five of the points are in the form of the project itself. Fifteen of the points will be in the form of homework assignments. This is broken down as follows:

- 15 points for the homework components

  - 5 points for the architecture one-pager (**due 30-Jan**)
  - 5 points for the preliminary design documentation (**due 18-Feb**)
  - 5 points for the detailed design documentation (**due 05-Mar**)

- 15 remaining points for the project overall
  - 5 points for multi-player real-time display, update, and access
  - 5 points for meeting non-functional requirements (playability, availability, performance, etc.)
  - 5 points for presentation, demo, and "realness" (**10- and 12-Mar**)

Extra credit points are available in the following categories:

- 5 points for multi-base play (two players controlling two bases simultaneously in the game; this is a different game than the traditional Space Invaders and you'll need to think about gameplay and interaction models)
- 2 points for sound integration [sound files](http://www.classicgaming.cc/classics/space-invaders/sounds)
- 2 points for original graphics and animation [graphics files](http://www.classicgaming.cc/classics/space-invaders/graphics)
- 2 points for an administration mode / console to configure the game at a _system_ level

## How the project is graded

Each team will be provided with a score sheet for each other team and one for themselves. Each team will score the other teams on the above presentation components (the 30 overall points and any extra credit points). Each team will also score their performance on the presentation component.

I will compile these as inputs to my overall assessment to gauge how the class, as a whole, feels about the presentations. _I reserve the right to bring in outside players to evaluate your games as well._ After all presentations are completed on the 12th I will compile and score all submissions and return them by the final exam.
