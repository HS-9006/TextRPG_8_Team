using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    //아직 덜 만듬
    internal class Guild
    {
        public Guild()
        {
            
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
                foreach (var quest in QuestManager.Instance.QuestList)
                {
                    Console.WriteLine(quest.questName);
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
}
