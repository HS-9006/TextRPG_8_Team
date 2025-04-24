using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG_8_Team
{
    interface IQuest
    {
        public abstract void Logic();
        public abstract void Print();
        public abstract void ChoiceResult();
    }
    internal abstract class Quest : IQuest
    {
        public bool isActivate = false;
        protected bool isCompleted = false;
        public bool isMoveClear = false;


        public string questName;
        protected string descriptionStart;
        protected string descriptionEnd;

        protected List<Item> rewardItems;
        protected int rewardGold;

        //생성자( 퀘스트이름 , 퀘스트 Txt , 퀘스트 클리어 Txt , 보상 아이템 리스트 , 보상 골드 )
        public Quest(string name, string start, string end, List<Item> items, int gold)
        {
            this.questName = name;
            this.descriptionStart = start;
            this.descriptionEnd = end;
            this.rewardItems = items;
            this.rewardGold = gold;
        }

        public abstract void Logic();
        public void Print()
        {
            //활성화 O and 클리어 O and 클리어한 퀘스트 리스트로 이동 O
            if (isActivate == true && (isCompleted == true && isMoveClear == true))
            {
                Console.WriteLine("이미 클리어한 퀘스트\n - LOG - \n" + descriptionStart + "\n" + descriptionEnd);
                Console.WriteLine("나가시려면 아무키나 누르세요");
                Console.ReadLine();
            }
            //활성화 O and 클리어 O 
            else if (isActivate == true && isCompleted == true) Console.WriteLine(descriptionEnd);
            //활성화 X or 클리어 X 
            else if (isActivate == false || isCompleted == false) Console.WriteLine(descriptionStart);
        }
        public void ChoiceResult()
        {
            while (true)
            {
                //예외 처리
                bool isNum = int.TryParse(Console.ReadLine(), out int choiceNum);
                if (!isNum || (choiceNum > 2 || choiceNum < 1))
                {
                    Console.WriteLine("잘못입력하셨습니다.");
                    Thread.Sleep(500);
                    continue;
                }
                //이전 화면
                if (choiceNum == 2)
                {
                    break;
                }
                //퀘스트 수락 X / 퀘스트 클리어 X 
                else if (isActivate == false && isCompleted == false)
                {
                    isActivate = true;
                    Console.WriteLine("수락하셨습니다!\n잠시후에 넘어갑니다");
                    GameManager.Instance.TotalThreadSleep();
                    break;
                }
                //퀘스트 수락 O / 퀘스트 클리어 X 
                else if (isActivate == true && isCompleted == false)
                {
                    Console.WriteLine("이미 수락하셨습니다!\n잠시후에 넘어갑니다");
                    GameManager.Instance.TotalThreadSleep();
                    break;
                }
                //퀘스트 수락 O / 퀘스트 클리어 O => 보상 획득
                else if (isActivate == true && isCompleted == true)
                {
                    foreach (Item item in rewardItems)
                    { 
                        GameManager.Instance.player.Inventory.Add(item);
                        Console.WriteLine($"{item.Name}을(를) 획득하셨습니다!");
                    }
                    if (rewardGold > 0)
                    {
                        GameManager.Instance.player.Gold += rewardGold;
                        Console.WriteLine($"{rewardGold}g 를 획득하셨습니다!");
                    }
                    //클리어 퀘스트 리스트로 이동
                    QuestManager.Instance.MoveClearList(this);
                    Console.WriteLine("계속하시려면 아무키나 누르세요");
                    Console.ReadLine();
                    break;
                }
            }
        }
        //protected string explanationStart = "퀘스트 설명을 적을 공간입니다.\n이게 보인다면 뭔가 잘 못 된겁니다";
        //protected string explanationClear = "퀘스트 클리어 설명을 적을 공간입니다.\n이게 보인다면 뭔가 잘 못 된겁니다";
        //protected List<Item> itemReward = new List<Item>();
        //protected int goldReward = 0;

        //public void Requirements()
        //{
        //    Console.WriteLine(explanationStart);
        //}
        //class KillMonster:Quest
        //{
        //    //bool isKillMonster = false;
        //    //int killCount = 0;
        //    //public void KillMonsterStart()
        //    //{
        //    //    while (true)
        //    //    {
        //    //        if (isKillMonster && killCount >= 5)
        //    //        {
        //    //            Console.Clear();
        //    //            Console.WriteLine("아이고 정말 고맙네\n눈에 띄게 몬스터가 줄었어\n");

        //    //            Console.WriteLine("- 몬스터 5마리 처치 Claer\n");

        //    //            Console.WriteLine("- 보상 - \n\t쓸만한 방패 x 1\n\t5G");

        //    //            Console.WriteLine("1. 보상 받기\n2. 나가기");
        //    //            Console.WriteLine("원하는 행동을 입력하세요.\n>>");

        //    //            bool isNumKill = int.TryParse(Console.ReadLine(), out int choiceNumKill);

        //    //            if (!isNumKill || (choiceNumKill > 2 || choiceNumKill < 1))
        //    //            {
        //    //                Console.WriteLine("잘못입력하셨습니다.");
        //    //                Thread.Sleep(500);
        //    //                continue;
        //    //            }
        //    //            else if (choiceNumKill == 1)
        //    //            {
        //    //                GameManager.Instance().player.Inventory.Add(new Item("쓸만한 방패", 0, 10, 0, 10));
        //    //                isKillMonster = false;
        //    //            }
        //    //            else
        //    //            {
        //    //                break;
        //    //            }
        //    //        }
        //    //        Console.Clear();
        //    //        Console.WriteLine("이봐! 마을 근처에 몬스터들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!\n");

        //    //        Console.WriteLine("- 몬스터 5마리 처치 (0/5)\n");

        //    //        Console.WriteLine("- 보상 - \n\t쓸만한 방패 x 1\n\t5G");

        //    //        Console.WriteLine("1. 수락\n2. 거절");
        //    //        Console.WriteLine("원하는 행동을 입력하세요.\n>>");

        //    //        bool isNum = int.TryParse(Console.ReadLine(), out int choiceNum);

        //    //        if (!isNum || (choiceNum > 2 || choiceNum < 1))
        //    //        {
        //    //            Console.WriteLine("잘못입력하셨습니다.");
        //    //            Thread.Sleep(500);
        //    //            continue;
        //    //        }
        //    //        else if (choiceNum == 1)
        //    //        {
        //    //            if (isKillMonster == false)
        //    //            {
        //    //                isKillMonster = true;
        //    //            }
        //    //            else
        //    //            {
        //    //                Console.WriteLine("이미 수락하셨습니다,");
        //    //                Thread.Sleep(500);
        //    //            }
        //    //            break;
        //    //        }
        //    //        else
        //    //        {
        //    //            break;
        //    //        }
        //    //    }
        //    //}
        //    //public void killCountUp()
        //    //{
        //    //    if (killCount < 5 && isKillMonster) killCount++;
        //    //}
        //}

        //class Equipped : Quest { }
        //class AttackPower100 : Quest { }
    }

    class KillMonster : Quest
    {
        public int monsterCount = 0;

        //생성자
        public KillMonster() : base
            ("KillMonster",
            $@"
Quest!!

마을을 위협하는 미니언 처치

이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?
마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!
모험가인 자네가 좀 처치해주게!

- 미니언 5마리 처치

- 보상 -
    쓸만한 방패 x 1
    5G

1. 수락
2. 거절
원하시는 행동을 입력해주세요.
>>",
            @"
Quest!!

마을을 위협하는 미니언 처치

고맙네 여기 내가 젊을 때 쓰던 방패라네
쓸만하니 가지고 가게

- 미니언 5마리 처치 Clear

- 보상 -
    쓸만한 방패 x 1
    5G

1. 보상 획득
2. 거절
원하시는 행동을 입력해주세요.
>>",
            new List<Item> { new Item("쓸만한 방패", 0, 10, 0, 10) },
            5)
        {

        }


        public override void Logic()
        {

        }

        //몬스터 잡으면 1개씩 수 늘려주기
        public void OnlyMonsterCount()
        {
            if ((isActivate == true && monsterCount < 5) && isCompleted == false)
            {
                monsterCount++;

                if (monsterCount >= 5)
                {
                    isCompleted = true;
                }
            }
        }
        //외부에서 몬스터 리스트를 받아서 죽인 몬스터 수만큼 퀘스트 몬스터 수 올리기
        public void QuestMonsterKillCount(int count)
        {
            if ((isActivate == true)&&(isCompleted == false))
            {
                //Console.SetCursorPosition(너비,높이); 맨위가 0
                for (int i = 0; i < count; i++)
                {
                    OnlyMonsterCount();
                }
                if (monsterCount >= 5)
                {
                    monsterCount = 5;
                }
            }
        }
    }

    class Equipped : Quest
    {
        int MonsterCount = 0;

        //생성자
        public Equipped() : base
            ("Equipped",
            @"
Quest!!

장비를 장착해보세요!

당신 지금 그 상태로 싸우려는거야?
당장 장비를 입어!!!!

- 아무 장비나 착용

- 보상 -
    5G

1. 수락
2. 거절
원하시는 행동을 입력해주세요.
>>",
            @"
Quest!!

장비를 장착해보세요!

그래그래 이게 맞지

- 아무 장비나 착용 Clear

- 보상 -
    5G

1. 보상 획득
2. 거절
원하시는 행동을 입력해주세요.
>>",
            new List<Item> { },
            5)
        {

        }

        //플레이어가 장착한 장비가 있으면 클리어
        public override void Logic()
        {
            if (isActivate == true)
            {
                if(GameManager.Instance.player.EquippedItems.Count>0)
                {
                    isCompleted=true;
                }
            }
        }
    }

    class AttackPower100 : Quest
    {
        int MonsterCount = 0;

        //생성자
        public AttackPower100() : base
            ("AttackPower100",
            @"
Quest!!

공격력 100이상 올리세요!

너 허접이잖앜ㅋ

- 공격력 100이상 올리기

- 보상 -
   ???

1. 수락
2. 거절
원하시는 행동을 입력해주세요.
>>",
            @"
Quest!!

공격력 100이상 올리세요!

자격이 생겼구나

- 공격력 100이상 올리기 Clear

- 보상 -
   ???

1. 보상 획득
2. 거절
원하시는 행동을 입력해주세요.
>>",
            new List<Item> { new Item("격이 다른 검", 100, 0, 0, 0) },
            5)
        {

        }
        public override void Logic()
        {
            if (isActivate == true)
            {
                if (GameManager.Instance.player.TotalAttack > 100)
                {
                    isCompleted = true;
                }
            }
        }
    }
}