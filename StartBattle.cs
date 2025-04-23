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
