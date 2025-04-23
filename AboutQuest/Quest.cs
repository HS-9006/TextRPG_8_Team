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
    }
    internal abstract class Quest : IQuest
    {
        protected bool isActivate = false;
        protected bool IsCompleted = false;

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
        int MonsterCount = 0;

        //생성자
        public KillMonster() : base
            ("KillMonster",
            @"
Quest!!

마을을 위협하는 미니언 처치

이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?
마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!
모험가인 자네가 좀 처치해주게!

- 미니언 5마리 처치 (0/5)

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

이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?
마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!
모험가인 자네가 좀 처치해주게!

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

        //몬스터 잡으면 수 늘려주기
        public override void Logic()
        { 
            if(this.isActivate == true && MonsterCount<5)
            {
                this.MonsterCount++;
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

당신 지금 그 상태로 싸우려는거야?
당장 장비를 입어!!!!

- 아무 장비나 착용 Clear

- 보상 -
    5G

1. 보상 획득
2. 거절
원하시는 행동을 입력해주세요.
>>",
            new List<Item> {},
            5)
        {

        }

        public override void Logic()
        {

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

        }
    }
}