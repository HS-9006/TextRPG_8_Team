using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    public enum JobType
    {
        Warrior,    // 전사
        Thief,      // 도적
        Mage        // 마법사
    }

    public class Player
    {
        public string Name;
        public JobType Job = JobType.Warrior;
        public int Level = 1;

        // 기본 능력치
        public int BaseAttack = 15;
        public int BaseDefense = 5;
        public int MaxHP = 100;
        public int CurrentHP = 100;
        public int Gold = 1500000;

        //아이템 장착으로 변한 능력치
        public int BonusAttack => EquippedItems.Sum(item => item.AttackBonus);
        public int BonusDefense => EquippedItems.Sum(item => item.DefenseBonus);
        public int BonusMaxHP => EquippedItems.Sum(item => item.MaxHP);

        //최종 능력치
        public int TotalAttack => BaseAttack + BonusAttack;
        public int TotalDefense => BaseDefense + BonusDefense;
        public int TotalMaxHP => MaxHP + BonusMaxHP;

        public List<Item> Inventory = new List<Item>();
        public List<Item> EquippedItems = new List<Item>();

        //직업에 따라 능력치 조절해주기 전사가 기본 스텟이니까 도적과 마법사만 조절
        //밸런스 공격력 1당 최대체력 -5, 방여력 -0.5(내림)
        public void ApplyJobStats()
        {
            switch (Job)
            {
                case JobType.Thief:
                    BaseAttack += 2;
                    BaseDefense -= 1;
                    MaxHP -= 10;
                    break;
                case JobType.Mage:
                    BaseAttack += 5;
                    BaseDefense -= 2;
                    MaxHP -= 25;
                    break;
            }

            CurrentHP = MaxHP;
        }

        public void PlayerStat()
        {
            Console.Clear();
            Console.WriteLine("\n===== 캐릭터 정보 =====");
            Console.WriteLine($"이름: {GameManager.Instance.player.Name}");
            Console.WriteLine($"직업: {GameManager.Instance.player.Job}");
            Console.WriteLine($"레벨: {GameManager.Instance.player.Level}");

            Console.WriteLine($"공격력: {GameManager.Instance.player.BaseAttack} (+{GameManager.Instance.player.BonusAttack}) => {GameManager.Instance.player.TotalAttack}");
            Console.WriteLine($"방어력: {GameManager.Instance.player.BaseDefense} (+{GameManager.Instance.player.BonusDefense}) => {GameManager.Instance.player.TotalDefense}");

            Console.WriteLine($"체력: {GameManager.Instance.player.CurrentHP} / {GameManager.Instance.player.TotalMaxHP}");
            Console.WriteLine($"Gold: {GameManager.Instance.player.Gold}");

            Console.WriteLine("\n[장착 중인 아이템]");
            if (GameManager.Instance.player.EquippedItems.Count == 0)
            {
                Console.WriteLine("없음");
            }
            else
            {
                foreach (var item in GameManager.Instance.player.EquippedItems)
                {
                    Console.WriteLine($"- {item}");
                }
            }
            Console.WriteLine("========================\n");
            Console.Write("메인 메뉴로 돌아가려면 0을 입력 : ");

            //숫자가 아니거나 0이 아니면 실행
            while (!int.TryParse(Console.ReadLine(), out int index) || !(index == 0))
            {
                Console.WriteLine("잘못된 입력입니다.");
                continue;
            }
        }
    }
}
