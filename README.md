# Game-Programming-Fundamentals-Finals

This repository contains two games implemented as part of the **Game Programming Fundamentals** final assignment.

## 📌 Contents
The project includes two console-based games written in C#:

1. **The Real Nim Game**
2. **Tic Tac Toe**

Each section below explains how the game works and what mechanics are implemented.

---

## 🎮 Game 1 – The Real Nim Game

### Description
A classic mathematical strategy game where the player and the AI take turns removing matches (or tokens) from several piles.  
**The player who removes the last remaining match loses.**

### How It Works
- The game starts with several piles, each containing a certain number of matches.  
- On each turn, the player selects:
  - **Which pile** to remove matches from
  - **How many matches** to remove (minimum 1 and no more than the pile contains)
- After the player's move, the AI takes its turn.  
- When all piles are empty, the player who made the last move loses.  
- Some gameplay rules (number of piles, maximum removal per turn, etc.) are configurable.

### Implemented Mechanics
- Pile representation using arrays/lists of integers  
- Console UI to display pile states  
- Input validation for pile index and removal amount  
- Turn-based structure (Player → AI → Player …)  
- End-game detection when all piles are empty  
- AI logic to perform a legal move  
- Optional configurable game settings

---

## ❌⭕ Game 2 – Tic Tac Toe

### Description
A console version of the classic 3×3 Tic Tac Toe game.  
Two players (or player vs AI) alternate placing **X** and **O** on the board.  
The goal is to align three symbols horizontally, vertically, or diagonally.

### How It Works
- The board is represented as a 3×3 two-dimensional array.  
- Players alternate turns: **X** always starts.  
- Each turn, a player chooses an empty cell (row and column).  
- After each move, the board is redrawn in the console.  
- The game ends immediately when a player wins or when the board is full (draw).  
- If single-player mode is active, the AI takes its turn automatically.

### Implemented Mechanics
- 3×3 board represented in a 2D array  
- Input validation for cell selection  
- ASCII-rendered console board  
- Turn handling between players (X ↔ O)  
- Win detection:
  - Rows  
  - Columns  
  - Main and secondary diagonals  
- Draw detection (full board without winner)  
- Optional AI move selection  
- Game termination and state control

---

## 📝 Additional Notes
- Both games demonstrate core programming concepts: arrays, functions, loops, conditionals, simple AI logic, and input handling.  
- Nim includes customizable rules for extra replayability.  
- Tic Tac Toe can easily be extended to include stronger AI or larger boards. 

---
