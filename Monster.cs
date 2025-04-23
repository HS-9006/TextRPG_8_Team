using System;
namespace TextRPG_8Team
{
<<<<<<< Updated upstream
    public class Monster
    {
        public string name;
        public int level;
        public int health { get; set; }
        public int attack { get; set; }
        public int def { get; set; }
        public int speed { get; set; }
        public int exp { get; set; }
        public int gold { get; set; }
=======
    public class DungeonManager
    {
        private Random rand = new Random();
        private BattleManager battleManager = new BattleManager();

        public void EnterDungeon()
        {
            Console.Clear();
            Console.WriteLine("던전에 입장합니다...\n");

            int stage = 1;
            while (true)
            {
                Console.WriteLine($"== {stage} 층 ==");

                // 몬스터 생성
                List<Monster> monsters = GenerateDungeonMonsters(stage);
                battleManager.Battle(Player.Instance(), monsters);

                // 사망 시 종료
                if (Player.Instance().CurrentHP <= 0)
                {
                    Console.WriteLine("플레이어가 사망했습니다. 던전에서 탈출합니다.");
                    break;
                }

                // 클리어 후 다음 단계
                Console.WriteLine("던전 클리어! 다음 층으로 갈까요?");
                Console.WriteLine("1. 다음 층  2. 나가기");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    stage++;
                    continue;
                }
                else
                {
                    Console.WriteLine("던전에서 안전하게 탈출했습니다!");
                    break;
                }
            }
        }

        private List<Monster> GenerateDungeonMonsters(int stage)
        {
            Type[] monsterTypes = { typeof(Ssalsoong), typeof(Reshoongjwak), typeof(Gongjungwi) };
            List<Monster> monsterList = new List<Monster>();
            int count = rand.Next(2, 5); // 2~4마리

            for (int i = 0; i < count; i++)
            {
                int idx = rand.Next(monsterTypes.Length);
                Monster m = (Monster)Activator.CreateInstance(monsterTypes[idx]);
                m.level += stage / 2;
                m.health += stage * 5;
                m.attack += stage * 2;
                m.def += stage;
                monsterList.Add(m);
            }

            return monsterList;
        }
    }

    public class Monster
    {
        public string name;
        public int level;
        public int health { get; set; }
        public int attack { get; set; }
        public int def { get; set; }
        public int speed { get; set; }
        public int exp { get; set; }
        public int gold { get; set; }
>>>>>>> Stashed changes
        public bool isAlive => health > 0;


    }
}

    public class Ssalsoong : Monster
    {
        public Ssalsoong()
        {
            name = "쌀숭이";
            level = 1;
            health = 30;
            attack = 10;
            def = 5;
            speed = 15;
            exp = 10;
            gold = 5;
        }
    }

    public class Reshoongjwak : Monster
    {
        public Reshoongjwak()
        {
            name = "리슝좍";
            level = 2;
            health = 35;
            attack = 15;
            def = 7;
            speed = 20;
            exp = 15;
            gold = 10;
        }
    }

    public class Gongjungwi : Monster
{
    public Gongjungwi()
    {
        name = "공정위";
        level = 5;
        health = 50;
        attack = 30;
        def = 14;
        speed = 25;
        exp = 40;
        gold = 100;

    }
}
}