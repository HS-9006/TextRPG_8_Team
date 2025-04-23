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
            player.PlayerStat();

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

            // 사용할 수 있는 몬스터 클래스들을 배열에 넣기
            Type[] monsterTypes = { typeof(Ssalsoong), typeof(Reshoongjwak), typeof(Gongjungwi) };

            int count = rand.Next(1, 5); // 최대 4마리
            for (int i = 0; i < count; i++)
            {
                int idx = rand.Next(monsterTypes.Length);
                Monster m = (Monster)Activator.CreateInstance(monsterTypes[idx]); // 인스턴스 생성
                monsterList.Add(m);
            }

            return monsterList;
        }
    }
}
