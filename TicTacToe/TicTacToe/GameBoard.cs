using System;

namespace TicTacToe
{
    internal class GameBoard
    {
        private string[] board = { ".", ".", ".",
                               ".", ".", ".",
                               ".", ".", "." };
        private string turn;
        public void WaitForEnter()
        {
            Console.WriteLine("확인했으면 Enter를 누르세요.");
            Console.ReadLine();
        }

        public void DispalyBoard()
        {
            int[] number = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine("=== 틱택토 게임 ===\n");
            Console.WriteLine("\n위치 번호: ");
            for (int i = 0; i < number.Length; i++)
            {
                Console.Write($"{number[i]}");
                if (i != 2 && i != 5 && i != 8)
                {
                    Console.Write(" | ");
                    continue;
                }
                if (i != 8)
                {
                    Console.WriteLine();
                    Console.WriteLine("---------");
                }
            }
            Console.WriteLine();
        }

        public void showBoard()
        {
            Console.WriteLine("\n현재 보드: ");
            for (int i = 0; i < board.Length; i++)
            {
                Console.Write($"{board[i]}");
                if (i != 2 && i != 5 && i != 8)
                {
                    Console.Write(" | ");
                    continue;
                }
                if (i != 8)
                {
                    Console.WriteLine();
                    Console.WriteLine("---------");
                }
            }
            Console.WriteLine();
        }

        public bool CheckWin(string turn)
        {
            for (int i = 0; i < 9; i += 3)
            {
                if (board[i] == turn && board[i + 1] == turn && board[i + 2] == turn)
                {
                    return true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (board[i] == turn && board[i + 3] == turn && board[i + 6] == turn)
                {
                    return true;
                }
            }
            if (board[0] == turn && board[4] == turn && board[8] == turn)
            {
                return true;
            }
            if (board[2] == turn && board[4] == turn && board[6] == turn)
            {
                return true;
            }
            return false;
        }
        public void ComChoice()
        {
            Random random = new Random();
            int playCount = 0;
            int number = 0;
            int difficulty = Level();
            DispalyBoard();
            showBoard();
            for (int i = 0; i < 5; i++)
            {
                int n;

                Console.WriteLine("\n");
                turn = "X";
                Console.WriteLine($"\n플레이어의 차례입니다.");
                Console.Write("위치를 선택하세요. (1~9): ");
                string choice = Console.ReadLine();
                if (!exception(choice, ref number))
                {
                    i--;
                    continue;
                }
                board[number - 1] = $"{turn}";
                playCount++;
                Console.Clear();
                DispalyBoard();
                showBoard();
                if (playCount == 9 && !CheckWin(turn))
                {
                    Console.WriteLine("무승부 입니다!");
                    WaitForEnter();
                    break;
                }
                if (CheckWin(turn))
                {
                    Console.WriteLine($"플레이어 승리!");
                    WaitForEnter();
                    break;
                }
                if (difficulty == 1)
                {
                    while (true)
                    {
                        turn = "O";
                        n = random.Next(0, 9);
                        if (board[n] == ".")
                        {
                            board[n] = turn;
                            playCount++;
                            Console.Clear();
                            DispalyBoard();
                            showBoard();
                            Console.WriteLine($"\n컴퓨터가 {n + 1}번 위치에 두었습니다.");
                            break;
                        }
                    }
                }
                else
                {
                    turn = "O";
                    n = FindWinMove("O");
                    if (n == -1)
                    {
                        n = FindDefenseMove();
                    }
                    if (n == -1 && board[4] == ".")
                    {
                        n = 4;
                    }
                    if (n == -1)
                    {
                        int[] corners = { 0, 2, 6, 8 };

                        foreach (int corner in corners)
                        {
                            if (board[corner] == ".")
                            {
                                n = corner;
                                break;
                            }
                        }
                    }
                    if (n == -1)
                    {
                        for (int j = 0; j < board.Length; j++)
                        {
                            if (board[j] == ".")
                            {
                                n = j;
                                break;
                            }
                        }
                    }
                    board[n] = turn;
                    playCount++;

                    Console.Clear();
                    DispalyBoard();
                    showBoard();
                    Console.WriteLine($"\n컴퓨터가 {n + 1}번 위치에 두었습니다.");
                }

                if (CheckWin(turn))
                {
                    Console.WriteLine($"플레이어 패배!");
                    WaitForEnter();
                    break;
                }
            }
        }
        public void PersonChoice()
        {
            DispalyBoard();
            showBoard();
            int playCount = 0;
            int number = 0;
            for (int i = 0; i < 9; i++)
            {

                Console.WriteLine("\n");
                if (i % 2 == 0)
                {
                    turn = "X";
                }
                else
                {
                    turn = "O";
                }
                Console.WriteLine($"\n플레이어 {turn}의 차례입니다.");
                Console.Write("위치를 선택하세요. (1~9): ");
                string choice = Console.ReadLine();
                if (!exception(choice, ref number))
                {
                    i--;
                    continue;
                }
                board[number - 1] = $"{turn}";
                playCount++;
                Console.Clear();
                DispalyBoard();
                showBoard();
                if (playCount == 9 && !CheckWin(turn))
                {
                    Console.WriteLine("무승부 입니다!");
                    WaitForEnter();
                    break;
                }
                if (CheckWin(turn))
                {
                    Console.WriteLine($"플레이어 {turn}승리!");
                    WaitForEnter();
                    break;
                }
            }
        }

        public bool ReTry()
        {
            for (; ; )
            {
                Console.Write("다시 플레이 하시겠습니까?  Y/N:");
                string yesOrNo = Console.ReadLine();
                if (char.TryParse(yesOrNo, out char yn))
                {
                    if (char.ToLower(yn) == 'y')
                    {
                        ResetBoard();
                        Console.Clear();
                        return true;
                    }
                    if (char.ToLower(yn) == 'n')
                    {
                        return false;
                    }
                }

                Console.WriteLine("Y 또는 N만 입력하십시오.");

            }
        }

        public void Board()
        {
            bool reTry = true;
            while (reTry)
            {

                Console.WriteLine("1. 두 사람 대전");
                Console.WriteLine("2. 컴퓨터 대전");
                Console.Write("선택하세요: ");
                for (; ; )
                {
                    string choice = Console.ReadLine();
                    if (int.TryParse(choice, out int result))
                    {
                        bool isTrue = false;
                        switch (result)
                        {
                            case 1:
                                Console.Clear();
                                PersonChoice();
                                reTry = ReTry();
                                isTrue = true;
                                break;
                            case 2:
                                Console.Clear();
                                ComChoice();
                                reTry = ReTry();
                                isTrue = true;
                                break;
                            default:
                                Console.WriteLine("잘못된 숫자 입니다. \n1,2번 중에서 선택하세요.");
                                isTrue = false;
                                break;
                        }
                        if (!isTrue)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력하세요.");
                    }
                }
            }
        }

        private bool exception(string choice, ref int number)
        {
            if (!int.TryParse(choice, out int n))
            {
                Console.WriteLine("숫자로 위치를 입력하세요.");

                return false;
            }
            if (n < 1 || n > 9)
            {
                Console.WriteLine("1부터 9까지의 번호를 입력하세요.");

                return false;
            }
            if (board[n - 1] != ".")
            {
                Console.WriteLine("이미 선택한 칸입니다. 빈칸을 선택하세요.");

                return false;
            }
            number = n;
            return true;
        }
        private void ResetBoard()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = ".";
            }
        }
        private int FindWinMove(string turn)
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == ".")
                {
                    board[i] = turn;

                    bool win = CheckWin(turn);

                    board[i] = ".";

                    if (win)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        private int FindDefenseMove()
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == ".")
                {
                    board[i] = "X";
                    bool win = CheckWin("X");
                    board[i] = ".";
                    if (win)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private int Level()
        {
            string level = null;
            Console.WriteLine("1. 쉬움");
            Console.WriteLine("2. 어려움");
            for (; ; )
            {
                Console.Write("난이도를 선택하세요: ");
                level = Console.ReadLine();
                if (!int.TryParse(level, out int difficulty))
                {
                    Console.WriteLine("숫자로 위치를 입력하세요.");
                    continue;
                }
                if (difficulty < 1 || difficulty > 2)
                {
                    Console.WriteLine("1 또는 2를 입력하세요.");
                    continue;
                }
                Console.Clear();
                return difficulty;
            }
        }
    }
}
