using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pong
{
    class Program
    {
        #region Positions X , Y
        private const int StartingPlayerPosX = MaxWidth / 2;
        private const int StartingPlayerPosY = BorderHeight - 2;

        private const int StartingBallPosX = MaxWidth / 2;
        private const int StartingBallPosY = MaxHeight / 2;

        private const int PlayerSizeDrawLeft = StartingPlayerPosX - 5;
        private const int PlayerSizeDrawRight = StartingPlayerPosX + 5;

        private const int scorePosX = BorderSize - 2;
        private const int scorePosY = BorderSize - 2;
        #endregion

        #region Parameters
        private const int PlayerSpeed = 50;
        //private const int PlayerSegments = 11;
        #endregion

        #region String Characters
        private const string PlayerSymbol = "═";
        private const string BallSymbol = "*";
        private const string BorderHeightSymbol = "║";
        private const string BorderWidthSymbol = "═";
        private const string TopLeftCornerSymbol = "╔";
        private const string TopRightCornerSymbol = "╗";
        private const string BottomLeftCornerSymbol = "╚";
        private const string BottomRightCornerSymbol = "╝";
        #endregion

        #region Arena Borders 
        private const int BorderSize = 4;
        private const int MaxWidth = 119;
        private const int MaxHeight = 40;
        private const int BorderWidth = MaxWidth - BorderSize;
        private const int BorderHeight = MaxHeight - BorderSize;
        #endregion

        static void Main(string[] args)
        {
            InitGame();
            GameLogic();
        }

        public static void InitGame()
        {
            Console.SetWindowSize(MaxWidth, MaxHeight);
            Console.CursorVisible = false;
            DrawBordersOnInit();
            DrawBallOnInit();
            DrawScoreOnInit();
            DrawPlayerOnInit();
        }

        public static void PlayableGame()
        {
            
        }

        public static void DrawBordersOnInit()
        {
            for (int i = BorderSize; i <= BorderHeight; i++)
            {
                Console.SetCursorPosition(BorderSize, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(BorderHeightSymbol);

                Console.SetCursorPosition(BorderWidth, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(BorderHeightSymbol);
            }

            for (int j = BorderSize; j <= BorderWidth; j++)
            {
                Console.SetCursorPosition(j, BorderSize);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(BorderWidthSymbol);

                Console.SetCursorPosition(j, BorderHeight);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(BorderWidthSymbol);
            }

            Console.SetCursorPosition(BorderSize, BorderSize);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(TopLeftCornerSymbol);

            Console.SetCursorPosition(BorderWidth, BorderSize);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(TopRightCornerSymbol);

            Console.SetCursorPosition(BorderSize, BorderHeight);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(BottomLeftCornerSymbol);

            Console.SetCursorPosition(BorderWidth, BorderHeight);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(BottomRightCornerSymbol);
        }

        public static void DrawScoreOnInit()
        {
            int playerOneScore = 0;
            int playerTwoScore = 0;

            Console.SetCursorPosition(scorePosX, scorePosY - 1);
            Console.Write("Player 1 : " + playerOneScore);
            Console.SetCursorPosition(scorePosX, scorePosY);
            Console.Write("Player 2 : " + playerTwoScore);
        }

        public static void DrawPlayerOnInit()
        {
            for (int i = PlayerSizeDrawLeft; i < PlayerSizeDrawRight; i++)
            {
                Console.SetCursorPosition(i, StartingPlayerPosY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(PlayerSymbol);
            }
        }

        public static void DrawBallOnInit()
        {
            Console.SetCursorPosition(StartingBallPosX, StartingBallPosY);
            Console.Write(BallSymbol);
        }

        public static void DrawPosX(int x, int y, string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(symbol);
        }

        public static void DrawBall(int x, int y, string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        //Complete this after ball movement
        public static void UpdateScore(int scoreMax, int playerOneScore, int playerTwoScore)
        {

        }

        public static void GameLogic()
        {
            int stepX = 0;
            int numberX = 1;
            int numberY = 1;
            int currentPosX = StartingPlayerPosX;
            int currentPosY = StartingPlayerPosY;
            int currentBallPosX = StartingBallPosX;
            int currentBallPosY = StartingBallPosY;

            ConsoleKeyInfo key;

            //Loop to play game...
            while (true)
            {

                //Input Handling...
                if (Console.KeyAvailable)
                {
                    //Has to be true to stop printing
                    key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow when (currentPosX != BorderSize + 6):
                            stepX = -1;
                            break;
                        case ConsoleKey.RightArrow when (currentPosX != BorderWidth - 6):
                            stepX = 1;
                            break;
                        default:
                            break;
                    }
                }

                DrawBall(currentBallPosX, currentBallPosY, " ");

                #region Ball Movement
                currentBallPosX += numberX;
                currentBallPosY += numberY;
                var paddleBody = currentBallPosX >= currentPosX - 5 && currentBallPosX <= currentPosX + 5;

                if (currentBallPosX >= BorderWidth - 1 || currentBallPosX <= BorderSize + 1)
                {
                    numberX = numberX * (-1);
                }
                if (currentBallPosY <= BorderSize + 1 || paddleBody && currentBallPosY == currentPosY - 1)
                {
                    numberY = numberY * (-1);
                }

                if(currentBallPosY == BorderHeight - 1)
                {
                    numberX = 0;
                    numberY = 0;
                    Console.Clear();
                    Console.SetCursorPosition(MaxWidth / 2 - 5, MaxHeight / 2);
                    Console.Write("You lose");
                    break;
                }

                #endregion

                DrawBall(currentBallPosX, currentBallPosY, BallSymbol);
                                
                #region Paddle Movement Check
                if (stepX > 0)
                {
                    DrawPosX(currentPosX - 5, currentPosY, " ");
                    DrawPosX(currentPosX + 5, currentPosY, PlayerSymbol);
                    currentPosX += 1;
                }

                if (stepX < 0)
                {
                    DrawPosX(currentPosX + 5, currentPosY, " ");
                    DrawPosX(currentPosX - 5, currentPosY, PlayerSymbol);
                    currentPosX -= 1;
                }
                #endregion

                stepX = 0;
                //var t = (int)(PlayerSpeed/1.8);
                Thread.Sleep(PlayerSpeed * 5);
            }
        }
    }
}

