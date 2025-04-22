using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    internal class Quest
    {
        public static Quest quest;

        //싱글톤
        public static Quest Instance()
        {
            if (quest == null)
            {
                quest = new Quest();
            }
            return quest;
        }

        public void GulidQuest()
        {
            while (true)
            {
                Console.WriteLine("안녕하세요 모험가님!\n의뢰가 있는데 어떠세요!\n");

                Console.WriteLine("1. 몬스터 처치\n2. 장비를 장착해보자\n3. 공격력 100만들기\n");

                Console.WriteLine("원하시는 행동을 선택해주세요.\n>>");

                bool isNum = int.TryParse(Console.ReadLine(), out int choiceNum);

                if (!isNum || (choiceNum > 3 || choiceNum < 1))
                {
                    Console.WriteLine("잘못입력하셨습니다.");
                    Thread.Sleep(500);
                    continue;
                }

                qeustChoice enumChoice = (qeustChoice)choiceNum;

                switch (enumChoice)
                {
                    case qeustChoice.KillMonster:
                        break;
                    case qeustChoice.Equipped:
                        break;
                    case qeustChoice.AttackPower100:
                        break;
                    default:
                        break;
                }
            }
        }

        enum qeustChoice
        {
            KillMonster = 1,
            Equipped,
            AttackPower100
        }
    }
    class KillMonster
    {
        public void KillMonsterStart()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("이봐! 마을 근처에 몬스터들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!\n");

                Console.WriteLine("- 몬스터 5마리 처치 (0/5)\n");

                Console.WriteLine("- 보상 - \n\t쓸만한 방패 x 1\n\t5G");

                Console.WriteLine("1. 수락\n2. 거절");
                Console.WriteLine("원하는 행동을 입력하세요.\n>>");

                bool isNum = int.TryParse(Console.ReadLine(), out int choiceNum);

                if (!isNum || (choiceNum > 2 || choiceNum < 1))
                {
                    Console.WriteLine("잘못입력하셨습니다.");
                    Thread.Sleep(500);
                    continue;
                }
                else if (choiceNum == 1)
                {
                    Player.Instance().Inventory.Add(new Item());
                }
            }
        }
    }
}
