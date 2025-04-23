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
            Console.WriteLine("전투시작");

            // 1. 몬스터 생성
            List<Monster> monster = RandomMonsters();

            // 2. 내 정보 출력
            GameManager.Instance().player.PlayerStat();

            // 3. 몬스터 출력
            for(int i = 0; i < monster.Count; i++)
            {
                Monster m = monster[i];
                Console.WriteLine($"{i + 1}. Lv {m.level}{m.name}(HP: {m.health})");
            }

            // 4. 행동 선택
            Console.WriteLine("1. 공격");
            Console.WriteLine("원하는 행동을 입력하세요.");
            string action = Console.ReadLine();

            if (action == "1")
            {
                BattleManager battleManager = new BattleManager();
                battleManager.Battle(monster);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
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

            for(int i = 0;i < count;i++)
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
