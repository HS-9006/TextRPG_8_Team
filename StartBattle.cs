using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    internal class StartBattle
    {
        private Random rand = new Random();

        public void start(Player player)
        {
            Console.WriteLine("전투시작");

            // 1. 몬스터 생성
            List<Monster> monster = RandomMonsters();

            // 2. 내 정보 출력
            player.PlayerStat(player);

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
                Console.WriteLine("공격할 몬스터의 번호를 선택해주세요.");
                string input = Console.ReadLine();

                if(int.TryParse(input, out int index) && index > 0 && index <= monster.Count)
                {
                    Monster currentMonster = monster[index - 1];

                    if (!currentMonster.isAlive)
                    {
                        Console.WriteLine("이미죽은 몬스터입니다.");
                    }
                    else
                    {
                        Console.WriteLine($"플레이어가 {currentMonster.name}을 공격했습니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다.");
                }
            }
        }

        private List<Monster> RandomMonsters()
        {
            List<Monster> monsterList = new List<Monster>();
            string[] names = { "미니언", "대포미니언", "공허충" };
            int[] levels = { 2, 5, 3 };
            int[] hps = { 15, 25, 10 };
            int[] attacks = { 5, 10, 3 };
            int[] defs = { 3, 5, 7 };
            int[] speed = { 2, 2, 2 };
            int[] exp = { 3, 2, 1 };
            int[] gold = { 1, 2, 3 };

            int count = rand.Next(1, 5);
            for (int i = 0; i < count; i++)
            {
                int idx = rand.Next(names.Length);

                Monster m = new Monster();
                m.name = names[idx];
                m.level = levels[idx];
                m.health = hps[idx];
                m.attack = attacks[idx];
                m.def = defs[idx];
                m.speed = speed[idx];
                m.exp = exp[idx];
                m.gold = gold[idx];

                monsterList.Add(m);
                //monsterList.Add(new Monster(names[idx], levels[idx], hps[idx], attacks[idx], defs[idx], speed[idx], exp[idx], gold[idx] ));
            }

            return monsterList;
        }
    }
}
