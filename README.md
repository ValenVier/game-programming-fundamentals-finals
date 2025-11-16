[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/anLdVMdI)
﻿# Game On!

### Goal

Create either Nim or Tic Tac Toe as your project, make use of what we've learnt so far to complete the task.

## Criteria

### G (Passing Grade)

- Completed the real Nim game
- Using Functions
 
### VG (Excellent Grade)

- Complete either:
  - The real Nim game using an array
  - Tic Tac Toe using a 2d-array

-------

## Game 1 - The Real Nim Game

Implement the Nim game making use of three piles of variable matches.
You may randomize them if you like but, I recommend using this setup:

````
O O O
| | | (Row 1)

O O O O
| | | | (Row 2)

O O O O O
| | | | | (Row 3)
````

The player can decide each turn, which Row he wants to draw from and how many matches he'd like to draw from it (at least 1, not more than three, but also not more than there are still left in the Row).

Next, it's the AI's turn.

Whoever draws the last match, leaving no matches behind, loses.

Things to consider:
- What game structure do you want to use for the Matches?
- Does it work well, if the number of Stacks changes?
- Can you make it so it's easy to change the game to have 10 stacks?
- What if the number of matches that the player can maximally draw changes?
- Can your game easily be changed?
- Does your AI still work well?
- What functions can you generalize to simplify your game code?

## Game 2 - Tic Tac Toe

Goal:

````
Output:Welcome to Tic-Tac-Toe!
Output: | | 
Output:-----
Output: | |
Output:-----
Output: | |
Output:In what column do you want to place your X (1-3)?
Input:2
Output:In what row do you want to place your X (1-3)?
Input:2
Output: | | 
Output:-----
Output: |X|
Output:-----
Output: | |
Output:In what column do you want to place your O (1-3)?
...
Output: |O|X
Output:-----
Output:O|X|X
Output:-----
Output:O| |X
Output:Player X wins.
````

Instructions:

- Create a Console Project named `TicTacToe`
- 2 Players
   - Player 1: `X`
   - Player 2: `O`
- 3x3 Grid (use a two-dimensional array)
- Players take turns choosing an empty grid cell and putting their symbol into it
- Player that has three of his symbols either
   - Horizontally
   - Vertically
   - Diagonally
- Wins and the game ends instantly.
- Make an ASCII-Art Display of the grid
- Bonus: implement an AI-Player
