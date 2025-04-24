using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    internal class Guild
    {
        public Guild()
        {

        }
        public void GulidQuest()
        {
            while (true)
            {
                foreach(var quest in QuestManager.Instance.QuestList)
                {
                    quest.Logic();
                }

                Console.Clear();

                int count = 1;

                //퀘스트 목록
                Console.WriteLine("안녕하세요 모험가님!\n의뢰가 있는데 어떠세요!\n");
                foreach (var quest in QuestManager.Instance.QuestList)
                {
                    Console.WriteLine(count + " . " + quest.questName);
                    count++;
                }
                Console.Write("0.나가기\n원하시는 행동을 선택해주세요.\n>>");

                //예외 처리
                bool isNum = int.TryParse(Console.ReadLine(), out int choiceNum);
                if (!isNum || (choiceNum > QuestManager.Instance.QuestList.Count || choiceNum < 0))
                {
                    Console.WriteLine("잘못입력하셨습니다.");
                    Thread.Sleep(500);
                    continue;
                }

                //이전 화면
                if(choiceNum==0)
                {
                    break;
                }

                QuestManager.Instance.QuestList[choiceNum - 1].Print();
                QuestManager.Instance.QuestList[choiceNum - 1].ChoiceResult();

                //qeustChoice enumChoice = (qeustChoice)choiceNum;
                //switch (enumChoice)
                //{
                //    case qeustChoice.KillMonster:
                //        break;
                //    case qeustChoice.Equipped:
                //        break;
                //    case qeustChoice.AttackPower100:
                //        break;
                //    default:
                //        break;
                //}
            }
        }
        //enum qeustChoice
        //{
        //    KillMonster = 1,
        //    Equipped,
        //    AttackPower100
        //}
    }
}
