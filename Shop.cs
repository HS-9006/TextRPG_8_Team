﻿using System;
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
            new Item("철검", ItemType.Weapon, 10, 0, 0, 1000),
            new Item("갑옷", ItemType.Armor, 0, 10, 10, 1200),
            new Item("지팡이", ItemType.Weapon, 15, -2, 0, 1300),
            new Item("도적의 방패", ItemType.Armor, 2, 10, 20, 1600),
            new Item("전사의 방패", ItemType.Armor, 10, 5, 20, 1900),
            new Item("마법사의 지팡이", ItemType.Weapon, 20, 0, 10, 2100),
            new Item("뒤틀린 황천의 바주카포", ItemType.Weapon, 300, 0, 0, 3000),
            new Item("무한의 대검", ItemType.Weapon, 99999, 99999, 99999, 5000)
        };

        public static void OpenShop()
        {
            while (true)
            {
                Console.Clear();
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
                        GameManager.Instance.TotalThreadSleep();
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        GameManager.Instance.TotalThreadSleep();
                        break;
                }
            }
        }
        static void BuyItem()
        {
            Console.Clear();
            Console.WriteLine("\n===== 상점 아이템 목록 =====");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {shopItems[i]}");
            }

            Console.WriteLine($"보유 Gold: {GameManager.Instance.player.Gold}");
            Console.Write("구매할 아이템 번호 입력 (0: 취소): ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index == 0)
                    return;

                if (index < 1 || index > shopItems.Count)
                {
                    Console.WriteLine("잘못된 번호입니다.");
                    GameManager.Instance.TotalThreadSleep();
                    return;
                }

                Item selected = shopItems[index - 1];
                if (GameManager.Instance.player.Gold >= selected.Price)
                {
                    GameManager.Instance.player.Gold -= selected.Price;
                    GameManager.Instance.player.Inventory.Add(selected);
                    Console.WriteLine($"{selected.Name}을(를) 구매했습니다.");
                    GameManager.Instance.TotalThreadSleep();
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    GameManager.Instance.TotalThreadSleep();
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                GameManager.Instance.TotalThreadSleep();
            }
        }
        static void SellItem()
        {
            Console.Clear();
            if (GameManager.Instance.player.Inventory.Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                GameManager.Instance.TotalThreadSleep();
                return;
            }

            Console.WriteLine("\n===== 판매 가능한 아이템 =====");
            for (int i = 0; i < GameManager.Instance.player.Inventory.Count; i++)
            {
                Item item = GameManager.Instance.player.Inventory[i];
                Console.WriteLine($"{i + 1}) {item} (판매가: {item.Price / 2}G)");
            }

            Console.Write("판매할 아이템 번호 입력 (0: 취소): ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index == 0)
                    return;

                if (index < 1 || index > GameManager.Instance.player.Inventory.Count)
                {
                    Console.WriteLine("잘못된 번호입니다.");
                    GameManager.Instance.TotalThreadSleep();
                    return;
                }

                Item item = GameManager.Instance.player.Inventory[index - 1];
                GameManager.Instance.player.Gold += item.Price / 2;
                GameManager.Instance.player.Inventory.RemoveAt(index - 1);
                Console.WriteLine($"{item.Name}을(를) 판매했습니다. {item.Price / 2}G 획득!");
                GameManager.Instance.TotalThreadSleep();
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                GameManager.Instance.TotalThreadSleep();
            }
        }

    }
}
