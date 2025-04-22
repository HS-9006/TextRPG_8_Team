using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    public static class Shop
    {
        //상점에서 팔 거 리스트
        //밸런스 공격력, 방어력 1 당 100, 최대체력 10당 200
        static List<Item> shopItems = new List<Item>
        {
                new Item("철검", 10, 0, 0, 1000),
                new Item("갑옷", 0, 10, 10, 1200),
                new Item("지팡이", 15, -2, 0, 1300),
                new Item("도적의 방패", 2, 10, 20 , 1600),
                new Item("전사의 방패", 10, 5, 20 , 1900),
                new Item("마법사의 지팡이", 20, 0, 10 , 2100)
        };

        public static void OpenShop()
        {
            while (true)
            {
                Console.WriteLine("\n===== 상점 =====");
                Console.WriteLine("1) 아이템 구매");
                Console.WriteLine("2) 아이템 판매");
                Console.WriteLine("0) 나가기");
                Console.Write("선택: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        BuyItem();
                        break;
                    case "2":
                        SellItem();
                        break;
                    case "0":
                        Console.WriteLine("상점을 나갑니다.\n");
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
        static void BuyItem()
        {
            Console.WriteLine("\n===== 상점 아이템 목록 =====");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {shopItems[i]}");
            }

            Console.WriteLine($"보유 Gold: {Player.Instance().Gold}");
            Console.Write("구매할 아이템 번호 입력 (0: 취소): ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index == 0)
                    return;

                if (index < 1 || index > shopItems.Count)
                {
                    Console.WriteLine("잘못된 번호입니다.");
                    return;
                }

                Item selected = shopItems[index - 1];
                if (Player.instance.Gold >= selected.Price)
                {
                    Player.Instance().Gold -= selected.Price;
                    Player.Instance().Inventory.Add(selected);
                    Console.WriteLine($"{selected.Name}을(를) 구매했습니다.");
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
            }
        }
        static void SellItem()
        {
            if (Player.Instance().Inventory.Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                return;
            }

            Console.WriteLine("\n===== 판매 가능한 아이템 =====");
            for (int i = 0; i < Player.Instance().Inventory.Count; i++)
            {
                Item item = Player.Instance().Inventory[i];
                Console.WriteLine($"{i + 1}) {item} (판매가: {item.Price / 2}G)");
            }

            Console.Write("판매할 아이템 번호 입력 (0: 취소): ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index == 0)
                    return;

                if (index < 1 || index > Player.Instance().Inventory.Count)
                {
                    Console.WriteLine("잘못된 번호입니다.");
                    return;
                }

                Item item = Player.Instance().Inventory[index - 1];
                Player.Instance().Gold += item.Price / 2;
                Player.Instance().Inventory.RemoveAt(index - 1);
                Console.WriteLine($"{item.Name}을(를) 판매했습니다. {item.Price / 2}G 획득!");
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
            }
        }

    }
}
