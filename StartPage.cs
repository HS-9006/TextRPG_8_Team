using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    internal class StartPage
    {
        //시작 화면
        public void StartGame()
        {
            Init();

            bool isGameEnd = false;

            SettingName();

            while (true)
            {
                if (isGameEnd) break;

                Console.Clear();
                Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

                for (int i = 0; i < System.Enum.GetValues(typeof(StartChoice)).Length; i++)
                {
                    StartChoice num = (StartChoice)i + 1;
                    Console.WriteLine((int)num+") "+ num);
                }

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                bool isChoiceNum = int.TryParse(Console.ReadLine(), out int choiceNum);

                //선택한 것이 숫자가 아니라면 실행
                if (!isChoiceNum)
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(500);
                    continue;
                }
                //1 ~ Eunm 갯수 사이의 값이 아니라면 실행
                if (choiceNum > System.Enum.GetValues(typeof(StartChoice)).Length || choiceNum < 1)
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(500);
                    continue;
                }

                StartChoice enumChoice = (StartChoice)choiceNum;

                switch (enumChoice)
                {
                    case StartChoice.Status:
                        GameManager.Instance.player.PlayerStat();
                        break;
                    case StartChoice.Battle:
                        StartBattle startBattle = new StartBattle();
                        startBattle.start();
                        break;
                    case StartChoice.Inventory:
                        InventorySystem.ShowInventory();
                        break;
                    case StartChoice.Shop:
                        Shop.OpenShop();
                        break;
                    case StartChoice.Inn:
                        Inn.VisitInn();
                        break;
                    case StartChoice.Guild:
                        GameManager.Instance.guild.GulidQuest();
                        break;
                    case StartChoice.GameEnd:
                        isGameEnd = true;
                        break;
                }
            }

            Console.WriteLine("게임이 종료되었습니다");
        }

        enum StartChoice
        {
            Status = 1,
            Battle,
            Inventory,
            Shop,
            Inn,
            Guild,
            GameEnd
        }

        public void SettingName()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이름을 입력해주세요.\n");
            Console.Write(">>");

            while (true)
            {
                string? inputName = Console.ReadLine();

                if (inputName == null)
                {
                    Console.WriteLine("잘못입력하셨습니다 다시 입력하세요");
                    continue;
                }

                GameManager.Instance.player.Name = inputName;

                Console.WriteLine($"환영합니다. {GameManager.Instance.player.Name}님! \n\n잠시후 넘어갑니다!");
                GameManager.Instance.TotalThreadSleep();
                break;
            }
        }

        public void Init()
        {
            QuestManager.Instance.QuestList.Add(new KillMonster()); 
            QuestManager.Instance.QuestList.Add(new Equipped()); 
            QuestManager.Instance.QuestList.Add(new AttackPower100());
        }
    }
}
