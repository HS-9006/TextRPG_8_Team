using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TextRPG_8_Team
{
    internal class StartBattle
    {
        private Random rand = new Random();

        public void start()
        {
            List<Monster> monsters = RandomMonsters();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!\n");
                foreach (var monster in monsters) Console.WriteLine($"Lv.{monster.level} {monster.name} {monster.health}");
                Console.WriteLine("\n[내 정보]");
                Console.WriteLine($"Lv.{GameManager.Instance.player.Level} Chad{GameManager.Instance.player.Job}");
                Console.WriteLine($"HP {GameManager.Instance.player.CurrentHP}/{GameManager.Instance.player.MaxHP}\n");
                Console.WriteLine("1) 공격");
                Console.WriteLine("0) 돌아가기\n");
                Console.WriteLine("원하는 행동을 입력하세요.");
                Console.WriteLine(">>");

                bool isChoiceNum = int.TryParse(Console.ReadLine(), out int choiceNum);
                if (!isChoiceNum)
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(500);
                    continue;
                }
                if (choiceNum == 1)
                {
                    BattleManager battleManager = new BattleManager();
                    battleManager.Battle(monsters);
                    break;
                }
                else if (choiceNum == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(500);
                }
            }
        }

        private List<Monster> RandomMonsters()
        {
            List<Monster> currentMonster = new List<Monster>
            {
                new Ssalsoong(),
                new Reshoongjwak(),
                new Gongjungwi()
            };
            List<Monster> result = new List<Monster>();

            int count = rand.Next(1, 5);

            for (int i = 0; i < count; i++)
            {
                int idx = rand.Next(currentMonster.Count);
                Monster selected = currentMonster[idx];

                Monster copy = null;

                if (selected is Ssalsoong)
                {
                    copy = new Ssalsoong();
                }
                else if (selected is Reshoongjwak)
                {
                    copy = new Reshoongjwak();
                }
                else if (selected is Gongjungwi)
                {
                    copy = new Gongjungwi();
                }

                result.Add(copy);
            }
            return result;
        }
    }
}
