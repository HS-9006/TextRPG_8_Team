using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_team
{
    internal class StartBattle
    {
        private Random rand = new Random();

        public void start(Player player)
        {
            Console.WriteLine("전투시작");

            // 1. 몬스터 생성
            List<몬스터> 몬스터 = RandomMonsters();

            // 2. 내 정보 출력
            player.PlayerStat();

            // 3. 몬스터 출력
            foreach (var 몬스터 in 몬스터)
            {
                몬스터.ShowInfo();
            }

            // 4. 행동 선택
            Console.WriteLine("1. 공격");
            Console.WriteLine("원하는 행동을 입력하세요.");
            string action = Console.ReadLine();

            if (action == "1")
            {
                Console.WriteLine("플레이어가 공격을 헀습니다.");
            }
        }

        private List<몬스터> RandomMonsters()
        {
            List<몬스터> 몬스터목록 = new List<몬스터>();
            string[] names = { "미니언", "대포미니언", "공허충" };
            int[] levels = { 2, 5, 3 };
            int[] hps = { 15, 25, 10 };

            int count = rand.Next(1, 5);
            for (int i = 0; i < count; i++)
            {
                int idx = rand.Next(names.Length);
                몬스터목록.Add(new 몬스터(names[idx], levels[idx], hps[idx]));
            }

            return 몬스터목록;
        }
    }
}
