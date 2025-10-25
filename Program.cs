// See https://aka.ms/new-console-template for more information

/*-----------*/
/*
 * TODO:
 * * GAME DEVELOPMENT ROADMAP
 * **DONE 1: Win condition checking for AI and player
 * **DONE 2: Player input validation - prevent overwriting existing moves
 * **DONE 3: Multiple difficulty levels implementation
 * **DONE 4: Player vs Player mode functionality
 * **DONE 5: Game replay functionality until player exits
 * **DONE 6: Nim Game implementation
 * **DONE 7: Color-coded text system for better UX
 * **DONE 8: Comprehensive code documentation
 * ** 9: Code refactoring and optimization
 */
/*-----------*/


// COLOR MANAGEMENT SYSTEM
// Handles all console color output for better user experience
void PrintSuccess(string text) {
    Console.ForegroundColor = ConsoleColor.Green;
    //Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine(text);
    Console.ResetColor();
}

void PrintError(string text) {
    Console.ForegroundColor = ConsoleColor.Red;
    //Console.BackgroundColor = ConsoleColor.DarkGray;
    Console.WriteLine(text);
    Console.ResetColor();
}

void PrintWarning(string text) {
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(text);
    Console.ResetColor();
}

void PrintHighlight(string text) {
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine(text);
    Console.ResetColor();
}

void PrintInfo(string text) {
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(text);
    Console.ResetColor();
}

PrintHighlight("Welcome to the House of Games!");
// MAIN GAME LOOP CONTROLLER
// Manages game selection and program flow
string play = "|";
while (play == "1" || play == "2" || play == "|")
{
    Console.WriteLine("\n" + new string('═', 60));
    PrintWarning("GAME SELECTION MENU");
    PrintInfo("Choose your adventure:");
    PrintWarning("1 - Tic-Tac-Toe (Classic Strategy)");
    PrintWarning("2 - Nim Game (Mathematical Challenge)");
    PrintError ("Any other key to exit the Game Hub");
    PrintInfo("Enter your choice (1/2):");
    
    play = Console.ReadLine();
    if (play == "1")// TIC-TAC-TOE GAME IMPLEMENTATION
    {
        PrintHighlight("Starting Tic-Tac-Toe Game!");
        PrintInfo("Rules: Get 3 in a row to win! You are 'X', AI is 'O'");
        
        // Game state initialization
        int choice = 0;
        bool validStart = false;
        bool validDiff = false;
        int difficulty = 0;
        char[,] board = new char[3, 3];
        bool gameOver = false;
        int turn = 0;

        // DIFFICULTY SELECTION LOOP
        // Handles player difficulty preference input
        while (!validDiff)
        {
            PrintWarning("\nSELECT DIFFICULTY LEVEL:");
            PrintInfo("1 - Easy (Random AI moves)");
            PrintInfo("2 - Medium (Defensive AI)");
            PrintInfo("3 - Hard (Strategic AI)");
            PrintInfo("4 - Player vs Player (Human vs Human)");
            PrintWarning("Enter your choice (1-4):");
            
            if (int.TryParse(Console.ReadLine(), out difficulty) && difficulty >= 1 && difficulty <= 4)
            {
                validDiff = true;
            }
            else
            {
                PrintError("Invalid selection! Please enter 1, 2, 3, or 4.");
            }
        }

        // STARTING PLAYER SELECTION LOOP
        // Determines who makes the first move
        while (!validStart && difficulty >= 1 && difficulty <= 3)
        {
            PrintWarning("\nSELECT STARTING PLAYER:");
            PrintInfo("1 - You go first (Player)");
            PrintInfo("2 - AI goes first (Computer)");
            PrintInfo("3 - Random selection (Luck of the draw)");
            PrintWarning("Enter your choice (1-3):");

            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 3)
            {
                validStart = true;
                // RANDOM START DECISION
                if (choice == 3)
                {
                    Random random = new Random();
                    choice = random.Next(1, 3);
                }
                PrintSuccess($"Game will start with {(choice == 1 ? "YOU" : "AI")}!");
            }
            else
            {
                PrintError("Invalid input! Please enter 1, 2, or 3.");
            }
        }
        
        // MAIN TIC-TAC-TOE GAME LOOP
        // Controls the flow of each game turn until completion
        while (!gameOver)
        {
            if (turn == 0) // Initial game setup
            {
                PrintSuccess("\nLet the game begin!");
                PrintBoard(turn, difficulty, board);
                turn++;
            }
            // TURN MANAGEMENT SYSTEM
            // Determines whose turn it is and processes the move
            WhoseTurnIsIt(turn, choice, board, difficulty);
            PrintBoard(turn, difficulty, board);
            
            // WIN CONDITION CHECK
            // Verifies if game has reached conclusion state
            gameOver = CheckGameOver(turn, board, (difficulty != 4 ? "checkGameOver" : "checkGameOverVS"));
            turn++;
        }
        /// <summary>
        /// Renders the current game board state to console
        /// Displays 3x3 grid with current X and O positions
        /// </summary>
        void PrintBoard(int turn, int difficulty, char[,] board)
        {
            for (int row = 0; row < 3; row++)
            {
                Console.Write($"{row + 1}  "); // Row indicator
                for (int col = 0; col < 3; col++)
                {
                    // Display cell content with proper spacing
                    Console.Write($" {(board[row, col] == '\0' ? ' ' : board[row, col])} ");
                    if (col < 2) Console.Write("│"); // Vertical separators
                }
                Console.WriteLine();
                
                // Horizontal separators between rows
                if (row < 2)
                {
                    Console.WriteLine("   ───┼───┼───");
                }
            }
            
            // Player identification system
            if (turn != 0 && difficulty != 4)
            {
                PrintInfo("You are X.");
            }

            if (difficulty == 4)
            {
                PrintInfo("Player 1: X | Player 2: O.");
            }
        }

        // <summary>
        /// Comprehensive win condition checker
        /// Analyzes all possible winning patterns on the board
        /// </summary>
        bool CheckGameOver(int turn, char[,] board, string checkingTask)
        {
            bool check = false;
            char winner = ' ';
            
            // HORIZONTAL WIN CHECK
            // Checks each row for three identical symbols
            for (int row = 0; row < 3; row++)
            {
                if ((board[row, 0] == 'X' || board[row, 0] == 'O') && board[row, 0] == board[row, 1] &&
                    board[row, 1] == board[row, 2])
                {
                    check = true;
                    winner = board[row, 0];
                    goto EndCheckingGameOver;
                }
            }

            // VERTICAL WIN CHECK
            // Checks each column for three identical symbols
            for (int col = 0; col < 3; col++)
            {
                if ((board[0, col] == 'X' || board[0, col] == 'O') && board[0, col] == board[1, col] &&
                    board[1, col] == board[2, col])
                {
                    check = true;
                    winner = board[0, col];
                    goto EndCheckingGameOver;
                }
            }

            // DIAGONAL WIN CHECKS
            // Main diagonal (top-left to bottom-right)
            if ((board[0, 0] == 'X' || board[0, 0] == 'O') && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                check = true;
                winner = board[0, 0];
                goto EndCheckingGameOver;
            }

            // Anti-diagonal (top-right to bottom-left)
            if ((board[0, 2] == 'X' || board[0, 2] == 'O') && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                check = true;
                winner = board[0, 2];
                //goto EndCheckingGameOver;
            }

            // WINNER ANNOUNCEMENT SYSTEM
            EndCheckingGameOver:
            if (check && checkingTask == "checkGameOver")
            {
                PrintSuccess("The Winner is: " + (winner == 'X' ? "YOU" : "AI"));
            }

            if (check && checkingTask == "checkGameOverVS")
            {
                PrintSuccess("The Winner is: " + (winner == 'X' ? "Player 1" : "Player 2"));
            }

            // DRAW CONDITION CHECK
            if (turn == 9 && !check && (checkingTask == "checkGameOver" || checkingTask == "checkGameOverVS"))
            {
                PrintSuccess("It's a draw! No winners this round.");
                check = true;
            }

            return check;
        }

        /// <summary>
        /// Turn coordinator - determines current active player
        /// Alternates between human and AI based on game mode
        /// </summary>
        void WhoseTurnIsIt(int turn, int choice, char[,] board, int difficulty)
        {
            if (difficulty != 4)
            {
                if ((turn % 2 != 0 && choice == 1) || (turn % 2 == 0 && choice == 2))
                {
                    PrintInfo("Player's turn:");
                    AskForCoordinatesHuman(board, 'X');
                }
                else
                {
                    PrintInfo("AI's turn:");
                    AskForCoordinatesAI(board, difficulty, turn);
                }
            }
            else
            {
                if (turn % 2 != 0)
                {
                    PrintInfo("Player 1's turn:");
                    AskForCoordinatesHuman(board, 'X');
                }
                else
                {
                    PrintInfo("Player 2's turn:");
                    AskForCoordinatesHuman(board, 'O');
                }
            }
        }

        /// <summary>
        /// Human player input handler
        /// Processes and validates human player moves
        /// </summary>
        void AskForCoordinatesHuman(char[,] board, char letter)
        {
            PrintWarning("Please, enter your coordinates...");
            bool coordinates = false;
            int column = 0;
            int row = 0;

            // COORDINATE VALIDATION LOOP
            // Ensures valid and available board positions
            while (!coordinates)
            {
                column = AskCoordinates("column");
                row = AskCoordinates("row");
                coordinates = CheckCoordinates(column, row, board);
            }

            board[row - 1, column - 1] = letter == 'X' ? 'X' : 'O';
        }

        /// <summary>
        /// Coordinate input validator
        /// Handles individual coordinate input with validation
        /// </summary>
        int AskCoordinates(string name)
        {
            int columnRowValue = 0;
            bool validColumnRow = false;

            while (!validColumnRow)
            {
                PrintWarning($"Enter {name} number (1-3):");
                if (int.TryParse(Console.ReadLine(), out columnRowValue) && columnRowValue >= 1 && columnRowValue <= 3)
                {
                    validColumnRow = true;
                }
                else
                {
                    PrintError("Invalid input! Please enter 1, 2, or 3.");
                }
            }

            return columnRowValue;
        }

        /// <summary>
        /// Position availability checker
        /// Verifies selected cell is not already occupied
        /// </summary>
        bool CheckCoordinates(int column, int row, char[,] board)
        {
            bool checkCoor = false;

            if (!(board[row - 1, column - 1] == 'X' || board[row - 1, column - 1] == 'O'))
            {
                checkCoor = true;
            }

            if (!checkCoor)
            {
                Console.WriteLine("");
                PrintError("That position is already taken! Choose different coordinates.");
            }

            return checkCoor;
        }

        /// <summary>
        /// AI move dispatcher
        /// Routes to appropriate AI strategy based on difficulty
        /// </summary>
        void AskForCoordinatesAI(char[,] board, int difficulty, int turn)
        {
            switch (difficulty)
            {
                case 1:
                {
                    GetEasyAIMove(board);
                    break;
                }
                case 2:
                {
                    GetMediumAIMove(board, turn);
                    break;
                }
                case 3:
                {
                    GetHardAIMove(board, turn);
                    break;
                }
                default:
                {
                    PrintError("Something went wrong.");
                    break;
                }
            }
        }

        /// <summary>
        /// Easy AI implementation - Random move selection
        /// Selects from all available empty cells randomly
        /// </summary>
        void GetEasyAIMove(char[,] board)
        {
            string coordStr = IteratingBoard(board, "emptyCells", int.Parse(IteratingBoard(board, "counting", 0)));

            int row = int.Parse(coordStr.Substring(0, 1));
            int column = int.Parse(coordStr.Substring(2, 1));
            board[row, column] = 'O';
        }

        /// <summary>
        /// Board analysis utility
        /// Performs various board analysis tasks (counting empty cells, etc.)
        /// </summary>
        string IteratingBoard(char[,] board, string task, int k)
        {
            string valueStr = "";
            string[] emptyCells = new string[k];
            int l = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(board[i, j] == 'X' || board[i, j] == 'O'))
                    {
                        if (task == "counting")
                        {
                            k++;
                        }

                        if (task == "emptyCells")
                        {
                            emptyCells[l] = i + "," + j;
                            l++;
                        }
                    }
                }
            }

            if (task == "counting")
            {
                valueStr = "" + k;
            }
            else
            {
                Random random = new Random();
                valueStr = emptyCells[random.Next(emptyCells.Length)];
            }

            return valueStr;
        }

        void GetMediumAIMove(char[,] board, int turn)
        {
            //check if AI can win with the next move
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(board[i, j] == 'X' || board[i, j] == 'O'))
                    {
                        board[i, j] = 'O';
                        if (CheckGameOver(turn, board, "checkIAsChoice"))
                        {
                            return;
                        }

                        board[i, j] = ' ';
                    }
                }
            }

            //check if the player can win with the next move 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(board[i, j] == 'X' || board[i, j] == 'O'))
                    {
                        board[i, j] = 'X';
                        if (CheckGameOver(turn, board, "checkIAsChoice"))
                        {
                            board[i, j] = 'O';
                            return;
                        }

                        board[i, j] = ' ';
                    }
                }
            }

            //choose a random movement
            GetEasyAIMove(board);
        }

        void GetHardAIMove(char[,] board, int turn)
        {
            //check if AI can win with the next move
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(board[i, j] == 'X' || board[i, j] == 'O'))
                    {
                        board[i, j] = 'O';
                        if (CheckGameOver(turn, board, "checkIAsChoice"))
                        {
                            return;
                        }

                        board[i, j] = ' ';
                    }
                }
            }

            //check if the player can win with the next move 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(board[i, j] == 'X' || board[i, j] == 'O'))
                    {
                        board[i, j] = 'X';
                        if (CheckGameOver(turn, board, "checkIAsChoice"))
                        {
                            board[i, j] = 'O';
                            return;
                        }

                        board[i, j] = ' ';
                    }
                }
            }

            //logical movement
            bool check = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((board[i, j] == 'O'))
                    {
                        for (int row = i; row < 3; row++)
                        {
                            if ((board[row, 0] != 'X') && board[row, 0] != 'X' && board[row, 1] != 'X' &&
                                board[row, 2] != 'X')
                            {
                                int l = board[row, 0] != 'O' ? 0 : (board[row, 1] != 'O' ? 1 : 2);
                                board[row, l] = 'O';
                                return;
                            }
                        }

                        for (int col = 0; col < 3; col++)
                        {
                            if ((board[0, col] != 'X') && board[0, col] != 'X' && board[1, col] != 'X' &&
                                board[2, col] != 'X')
                            {
                                int l = board[0, col] != 'O' ? 0 : (board[1, col] != 'O' ? 1 : 2);
                                board[l, col] = 'O';
                                return;
                            }
                        }

                        if ((i == 0 && j == 0) || (i == 3 && j == 0) || (i == 3 && j == 0) || (i == 3 && j == 3) ||
                            (i == 2 && j == 2))
                        {
                            int l = 0;
                            if (board[0, 0] != 'X' && board[1, 1] != 'X' && board[2, 2] != 'X')
                            {
                                l = board[0, 0] != 'O' ? 1 : (board[1, 1] != 'O' ? 2 : 3);
                            }

                            if (board[0, 2] != 'X' && board[1, 1] != 'X' && board[2, 0] != 'X')
                            {
                                l = board[0, 2] != 'O' ? 4 : (board[1, 1] != 'O' ? 2 : 5);
                            }

                            if (l != 0)
                            {
                                switch (l)
                                {
                                    case 1:
                                        board[0, 0] = 'O';
                                        break;
                                    case 2:
                                    {
                                        board[1, 1] = 'O';
                                        break;
                                    }
                                    case 3:
                                    {
                                        board[2, 2] = 'O';
                                        break;
                                    }
                                    case 4:
                                    {
                                        board[0, 2] = 'O';
                                        break;
                                    }
                                    case 5:
                                    {
                                        board[2, 0] = 'O';
                                        break;
                                    }
                                    default:
                                    {
                                        PrintError("Something went wrong.");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //choose a random movement
            if (turn == 1)
            {
                board[1, 1] = 'O';
            }
            else
            {
                GetEasyAIMove(board);
            }
        }
    }
    else if (play == "2")// NIM GAME IMPLEMENTATION
    {
        PrintHighlight("Starting Nim Game - The Mathematical Strategy Challenge!");
        PrintInfo("Rules: Remove matches from rows. Last player to take a match loses!");

        // Nim game state initialization
        bool validDiff = false;
        bool validStart = false;
        bool validRows = false;
        int difficulty = 0;
        bool gameOver = false;
        int turn = 0;
        int choice = 0;
        int rows = 0;

        // NIM DIFFICULTY SELECTION LOOP
        while (!validDiff)
        {
            PrintWarning("\nSELECT NIM DIFFICULTY LEVEL:");
            PrintInfo("1 - Easy AI (Random moves)");
            PrintInfo("2 - Medium AI (Mixed strategy)");
            PrintInfo("3 - Hard AI (Mathematical strategy)");
            PrintInfo("4 - Player vs Player (Human vs Human)");
            PrintWarning("Enter your choice (1-4):");
            if (int.TryParse(Console.ReadLine(), out difficulty) && difficulty >= 1 && difficulty <= 4)
            {
                validDiff = true;
            }
            else
            {
                PrintError("Invalid selection! Please enter 1, 2, 3 or 4.");
            }

            // STARTING PLAYER SELECTION FOR NIM
            while (!validStart && difficulty >= 1 && difficulty <= 3 && validDiff)
            {
                PrintWarning("\nSELECT STARTING PLAYER:");
                PrintInfo("1 - You go first");
                PrintInfo("2 - AI goes first"); 
                PrintInfo("3 - Random selection");
                PrintWarning("Enter your choice (1-3):");
                
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 3)
                {
                    validStart = true;
                    if (choice == 3)
                    {
                        Random random = new Random();
                        choice = random.Next(1, 3);
                    }

                    PrintSuccess($"Game will start with {(choice == 1 ? "YOU" : "AI")}!");
                }
                else
                {
                    PrintError("Invalid input! Please enter 1, 2, or 3.");
                }
            }
            
            // ROW COUNT SELECTION LOOP
            // Determines game board size
            while (!validRows && validStart && validStart)
            {
                PrintWarning("\nSELECT GAME SIZE:");
                PrintInfo("Choose number of rows (3-10):");
                if (int.TryParse(Console.ReadLine(), out rows) && rows >= 3 && rows <= 10)
                {
                    validRows = true;
                }
                else
                {
                    PrintError("Invalid input! Please enter a number between 3 and 10.");
                }
            }
        }

        // MATCHES ARRAY INITIALIZATION
        // Creates the game board with increasing match counts
        int[] matchesArray = new int[rows];
        for (int i = 0; i < rows; i++)
        {
            matchesArray[i] = (i + 3);// Row 1: 3 matches, Row 2: 4 matches, etc
        }

        // MAIN NIM GAME LOOP
        while (!gameOver)
        {
            if (turn == 0)// Initial game state
            {
                PrintSuccess("Let's play!");
                PrintBoard(matchesArray, rows);
                turn++;
            }
            
            // NIM TURN MANAGEMENT
            WhoseTurnIsIt(turn, choice, matchesArray, difficulty);
            PrintBoard(matchesArray, rows);
            
            // NIM WIN CONDITION CHECK
            gameOver = CheckGameOver(matchesArray, turn, choice, difficulty);
            turn++;
        }

        /// <summary>
        /// Renders the current game board state to console
        /// Displays all the matches
        /// </summary>
        void PrintBoard(int[] matchesArray, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine();

                if (matchesArray[i] > 0)
                {
                    for (int j = 0; j < matchesArray[i]; j++)
                    {
                        Console.Write("O ");
                    }

                    Console.WriteLine();
                    for (int j = 0; j < matchesArray[i]; j++)
                    {
                        Console.Write("| ");
                    }
                }

                Console.Write($" (Row {i + 1}: {matchesArray[i]} matches)");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Turn coordinator - determines current active player
        /// Alternates between human and AI based on game mode
        /// </summary>
        void WhoseTurnIsIt(int turn, int choice, int[] matchesArray, int difficulty)
        {
            if (difficulty != 4)
            {
                Console.WriteLine();
                if ((turn % 2 != 0 && choice == 1) || (turn % 2 == 0 && choice == 2))
                {
                    PrintInfo("Player's turn:");
                    AskPlayer(matchesArray);
                }
                else
                {
                    PrintInfo("AI's turn:");
                    AskAI(matchesArray);
                }
            }
            else
            {
                if (turn % 2 != 0)
                {
                    PrintInfo("Player 1's turn:");
                    AskPlayer(matchesArray);
                }
                else
                {
                    PrintInfo("Player 2's turn:");
                    AskPlayer(matchesArray);
                }
            }
        }

        /// <summary>
        /// Position availability checker
        /// Verifies the selected row and the selected amount of matches
        /// </summary>
        void AskPlayer(int[] matchesArray)
        {
            int row = 0;
            int matches = 0;
            bool validRow = false;
            bool validMatches = false;

            while (!validRow)
            {
                PrintWarning("Which row do you want to remove matches from?");
                if (int.TryParse(Console.ReadLine(), out row) && row >= 1 && row <= matchesArray.Length)
                {
                    row--;
                    if (matchesArray[row] != 0)
                    {
                        validRow = true;
                    }
                    else
                    {
                        PrintError("There are no matches in that row!");
                    }
                }
                else
                {
                    PrintError($"Invalid input. Please enter a number between 1 and {matchesArray.Length}.");
                }
            }

            while (!validMatches)
            {
                PrintWarning("How many matches do you want to remove (max: 3)?");
                if (int.TryParse(Console.ReadLine(), out matches) && matches >= 1 && matches <= matchesArray[row] &&
                    matches <= 3)
                {
                    validMatches = true;
                }
                else
                {
                    PrintError($"Invalid input. Please enter another number.");
                }
            }

            removeMatches(matchesArray, row, matches);
        }

        /// <summary>
        /// AI move dispatcher
        /// Routes to appropriate AI strategy based on difficulty
        /// </summary>
        void AskAI(int[] matchesArray)
        {
            switch (difficulty)
            {
                case 1:
                {
                    GetEasyAIMove(matchesArray);
                    break;
                }
                case 2:
                {
                    GetMediumAIMove(matchesArray);
                    break;
                }
                case 3:
                {
                    GetHardAIMove(matchesArray);
                    break;
                }
                default:
                {
                    PrintError("Something went wrong.");
                    break;
                }
            }
        }

        void GetEasyAIMove(int[] matchesArray)
        {
            bool validRow = false;
            bool validMatches = false;
            int row = 0;
            int matches = 0;

            Random random = new Random();
            while (!validRow)
            {
                row = random.Next(1, matchesArray.Length + 1);
                row--;
                if (matchesArray[row] > 0)
                {
                    validRow = true;
                }
            }

            while (!validMatches)
            {
                matches = random.Next(1, matchesArray[row] + 1);
                validMatches = true;
            }

            removeMatches(matchesArray, row, matches);
        }

        void GetMediumAIMove(int[] matchesArray)
        {
            Random random = new Random();
            if (random.Next(1, 3) == 1)
            {
                GetEasyAIMove(matchesArray);
            }
            else
            {
                GetHardAIMove(matchesArray);
            }
        }

        void GetHardAIMove(int[] matchesArray)
        {
            int sum = 0;
            int rows = 0;
            for (int i = 0; i < matchesArray.Length; i++)
            {
                if (matchesArray[i] > 0)
                {
                    rows++;
                    sum += matchesArray[i];
                }
            }

            if (sum > 7)
            {
                bool foundRow = false;
                for (int i = 0; i < matchesArray.Length; i++)
                {
                    if (!foundRow && (matchesArray[i] == 1 || matchesArray[i] == 2 || matchesArray[i] == 3))
                    {
                        int matchesToRemove = matchesArray[i];
                        if (matchesToRemove <= 3)
                        {
                            removeMatches(matchesArray, i, matchesToRemove);
                            foundRow = true;
                        }
                    }
                }

                if (!foundRow)
                {
                    int maxRow = -1;
                    int maxMatches = 0;
                    for (int i = 0; i < matchesArray.Length; i++)
                    {
                        if (matchesArray[i] > maxMatches)
                        {
                            maxMatches = matchesArray[i];
                            maxRow = i;
                        }
                    }

                    if (maxRow != -1)
                    {
                        int matchesToRemove = Math.Min(3, matchesArray[maxRow]);
                        removeMatches(matchesArray, maxRow, matchesToRemove);
                    }
                    else
                    {
                        GetEasyAIMove(matchesArray);
                    }
                }
            }
            else
            {
                bool foundRow = false;
                if (sum > 4)
                {
                    for (int i = 0; i < matchesArray.Length; i++)
                    {
                        if (!foundRow && (sum - matchesArray[i] <= 4))
                        {
                            removeMatches(matchesArray, i, (sum - matchesArray[i]));
                            foundRow = true;
                        }
                    }

                    if (!foundRow)
                    {
                        for (int i = 0; i < matchesArray.Length; i++)
                        {
                            if (!foundRow && matchesArray[i] > 0)
                            {
                                int matchesToRemove = Math.Min(3, matchesArray[i]);
                                removeMatches(matchesArray, i, matchesToRemove);
                                foundRow = true;
                            }
                        }
                    }
                }
                else
                {
                    switch (rows)
                    {
                        case 4:
                        {
                            for (int i = 0; i < matchesArray.Length; i++)
                            {
                                if (!foundRow && (matchesArray[i] > 0))
                                {
                                    removeMatches(matchesArray, i, 1);
                                    foundRow = true;
                                }
                            }

                            break;
                        }

                        case 3:
                        {
                            int maxRow = -1;
                            int maxMatches = 0;
                            for (int i = 0; i < matchesArray.Length; i++)
                            {
                                if (matchesArray[i] > maxMatches)
                                {
                                    maxMatches = matchesArray[i];
                                    maxRow = i;
                                }
                            }

                            if (maxRow != -1)
                            {
                                removeMatches(matchesArray, maxRow, 1);
                            }
                            else
                            {
                                GetEasyAIMove(matchesArray);
                            }

                            break;
                        }

                        case 2:
                        {
                            switch (sum)
                            {
                                case 4:
                                {
                                    bool row3Matches = false;
                                    int row = 0;
                                    int matchesToRemove = 1;
                                    for (int i = 0; i < matchesArray.Length; i++)
                                    {
                                        if (matchesArray[i] > 2)
                                        {
                                            row = i;
                                            matchesToRemove = 2;
                                            row3Matches = true;
                                        }
                                    }

                                    if (!row3Matches)
                                    {
                                        for (int i = 0; i < matchesArray.Length; i++)
                                        {
                                            if (matchesArray[i] > 0)
                                            {
                                                row = i;
                                                return;
                                            }
                                        }
                                    }

                                    removeMatches(matchesArray, row, matchesToRemove);
                                    break;
                                }
                                case 3:
                                {
                                    for (int i = 0; i < matchesArray.Length; i++)
                                    {
                                        if (!foundRow && (matchesArray[i] > 1))
                                        {
                                            removeMatches(matchesArray, i, 1);
                                            foundRow = true;
                                        }
                                    }

                                    break;
                                }
                                case 2:
                                {
                                    for (int i = 0; i < matchesArray.Length; i++)
                                    {
                                        if (!foundRow && (matchesArray[i] > 0))
                                        {
                                            removeMatches(matchesArray, i, 1);
                                            foundRow = true;
                                        }
                                    }

                                    break;
                                }

                                default:
                                {
                                    PrintError("Something went wrong");
                                    break;
                                }
                            }

                            break;
                        }

                        case 1:
                        {
                            int maxMatches = sum - 1;
                            for (int i = 0; i < matchesArray.Length; i++)
                            {
                                if (!foundRow && (matchesArray[i] > 0))
                                {
                                    if (maxMatches > 0)
                                    {
                                        removeMatches(matchesArray, i, maxMatches);
                                    }
                                    else
                                    {
                                        removeMatches(matchesArray, i, 1);
                                    }

                                    foundRow = true;
                                }
                            }

                            break;
                        }

                        default:
                        {
                            PrintError("Something went wrong");
                            break;
                        }
                    }
                }
            }
        }
        
        void removeMatches(int[] matchesArray, int row, int matches)
        {
            matchesArray[row] -= matches;
        }

        // <summary>
        /// Comprehensive win condition checker
        /// </summary>
        bool CheckGameOver(int[] matchesArray, int turn, int choice, int difficulty)
        {
            int number0s = 0;
            bool check = false;
            
            for (int i = 0; i < matchesArray.Length; i++)
            {
                if (matchesArray[i] == 0)
                {
                    number0s++;
                }
            }

            check = (number0s == matchesArray.Length);

            if (check)
            {
                Console.WriteLine();
                if (difficulty != 4)
                {
                    if ((turn % 2 != 0 && choice == 1) || (turn % 2 == 0 && choice == 2))
                    {
                        PrintError("AI win!");
                    }
                    else
                    {
                        PrintHighlight("YOU win!");
                    }
                }
                else
                {
                    // Player vs Player
                    if (turn % 2 != 0)
                    {
                        PrintHighlight("Player 1 win!");
                    }
                    else
                    {
                        PrintHighlight("Player 2 win!");
                    }
                }

                Console.WriteLine();
            }

            return check;
        }
    }
    else
    {
        PrintHighlight("\nThank you for playing! Goodbye!");
    }
}