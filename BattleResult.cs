using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPG_8_Team
{
    internal class BattleResult
    {
        public static void BattleResultMenu(bool isWin, List<Monster> monsters)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("\n");

                if (isWin)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Victory");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n");
                    Console.WriteLine($"던전에서 몬스터 {monsters.Count()}마리를 잡았습니다.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("You Lose");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine("\n");
                Console.WriteLine($"Lv.{GameManager.Instance().player.Level} {GameManager.Instance().player.Name}");
                Console.WriteLine($"HP {GameManager.Instance().player.TotalMaxHP} -> {GameManager.Instance().player.CurrentHP}\n");

                Console.WriteLine("0. 다음\n");
                Console.Write(">> ");

                bool isChoiceNum = int.TryParse(Console.ReadLine(), out int choiceNum);

                //선택한 것이 숫자가 아니라면 실행
                if (!isChoiceNum)
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(500);
                    continue;
                }
                //1~5의 값이 아니라면 실행
                if (choiceNum != 0)
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(500);
                    continue;
                }
                else if (choiceNum == 0)
                {
                    BattleResultInit(monsters);
                    //GameManager.instance.GameStart();
                    return;
                }

            }
        }
        public static void BattleResultInit(List<Monster> monsters)
        {
            int totalGold = monsters.Where(m => !m.isAlive).Sum(m => m.gold);
            GameManager.Instance().player.Gold += totalGold;

            Console.WriteLine($"\n획득한 골드: {totalGold} G");
            Console.WriteLine($"현재 보유 골드: {GameManager.Instance().player.Gold} G");

            Console.WriteLine("\n0. 다음\n>> ");
            while (true)
            {
                bool isChoiceNum = int.TryParse(Console.ReadLine(), out int choiceNum);
                if (isChoiceNum && choiceNum == 0) break;

                Console.WriteLine("잘못된 입력입니다");
            }
        }

    }
}
