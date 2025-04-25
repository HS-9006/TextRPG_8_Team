using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    internal class InventorySystem
    {
        public static void ShowInventory()
        {
            var player = GameManager.Instance.player;
            Console.Clear();
            Console.WriteLine("\n===== 인벤토리 =====");

            //player의 inventory에 있는 장비 개수 확인 없으면 비어있다고 출력
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
            }
            else
            {
                //인벤토리에 있다면 하나하나 출력함
                for (int i = 0; i < GameManager.Instance.player.Inventory.Count; i++)
                {
                    var item = GameManager.Instance.player.Inventory[i];
                    Console.WriteLine($"{i + 1}) {item.Name} (공격력 +{item.AttackBonus}, 방어력 +{item.DefenseBonus}, 최대체력 +{item.MaxHP})");
                }
            }

            Console.WriteLine("\n장착 중인 아이템:");
            //장착한 장비 리스트 확인, 없으면 없다고 출력
            if (GameManager.Instance.player.EquippedItems.Count == 0)
            {
                Console.WriteLine("장착된 아이템이 없습니다.");
            }
            else
            {
                //장착한 각각의 아이템마다 정보 출력
                foreach (var item in GameManager.Instance.player.EquippedItems)
                {
                    Console.WriteLine($"- [E] {item.Name} (공격력 +{item.AttackBonus}, 방어력 +{item.DefenseBonus}, 최대체력 +{item.MaxHP})");
                }
            }

            Console.WriteLine("====================\n");
            //장착, 해체는 다른 함수로 부르기
            ManageInventory();
        }

        static void ManageInventory()
        {
            Console.WriteLine("1) 아이템 장착");
            Console.WriteLine("2) 아이템 해제");
            Console.WriteLine("0) 돌아가기");
            Console.Write("선택: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    EquipItem();
                    break;
                case "2":
                    UnequipItem();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }

        static void EquipItem()
        {
            //장착할 아이템 인벤토리 리스트에서 출력, 없으면 없다고 출력
            if (GameManager.Instance.player.Inventory.Count == 0)
            {
                Console.WriteLine("장착할 아이템이 없습니다.");
                GameManager.Instance.TotalThreadSleep();
                return;
            }
            else
            {
                //인벤토리에 있다면 하나하나 출력함
                Console.Clear();
                for (int i = 0; i < GameManager.Instance.player.Inventory.Count; i++)
                {
                    var item = GameManager.Instance.player.Inventory[i];
                    Console.WriteLine($"{i + 1}) {item.Name} (공격력 +{item.AttackBonus}, 방어력 +{item.DefenseBonus}, 최대체력 +{item.MaxHP})\n");
                }
            }
            //인벤토리에 장비가 있으면 숫자와 함께 출력
            Console.Write("장착할 아이템 번호를 입력하세요: ");
            if (int.TryParse(Console.ReadLine(), out int choiceNum))
            {
                //유저가 입력한 게 0이거나 인벤토리 리스트보다 크다면 잘못됐다고 출력하고 되돌림
                if (choiceNum < 1 || choiceNum > GameManager.Instance.player.Inventory.Count)
                {
                    Console.WriteLine("잘못된 번호입니다.");
                    GameManager.Instance.TotalThreadSleep();
                    return;
                }
                //유저가 입력한 값 - 1 을 해야 리스트 순서에 맞출 수 있음
                var item = GameManager.Instance.player.Inventory[choiceNum - 1];
                Item? equipped = null;
                foreach (Item i in GameManager.Instance.player.EquippedItems)
                {
                    if (i.type == item.type)
                    {
                        equipped = i;
                    }
                }
                if (equipped != null)
                {
                    GameManager.Instance.player.EquippedItems.Remove(equipped);
                    GameManager.Instance.player.Inventory.Add(equipped);
                    GameManager.Instance.player.Inventory.Remove(item);
                    GameManager.Instance.player.EquippedItems.Add(item);
                    Console.WriteLine($"\n{item.Name}을(를) 장착했습니다.");
                    GameManager.Instance.TotalThreadSleep();
                }
                else
                {
                    //장착한 장비 리스트에 추가
                    GameManager.Instance.player.EquippedItems.Add(item);
                    //인벤토리에서 개수 -1
                    GameManager.Instance.player.Inventory.RemoveAt(choiceNum - 1);
                    Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
                    GameManager.Instance.TotalThreadSleep();
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
            }
        }

        static void UnequipItem()
        {
            if (GameManager.Instance.player.EquippedItems.Count == 0)
            {
                Console.WriteLine("해제할 아이템이 없습니다.");
                GameManager.Instance.TotalThreadSleep();
                return;
            }

            Console.Write("해제할 아이템 번호를 입력하세요: ");
            for (int i = 0; i < GameManager.Instance.player.EquippedItems.Count; i++)
            {
                var item = GameManager.Instance.player.EquippedItems[i];
                Console.WriteLine($"{i + 1}) {item.Name} (공격력 +{item.AttackBonus}, 방어력 +{item.DefenseBonus}, 최대체력 +{item.MaxHP})");
            }

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index < 1 || index > GameManager.Instance.player.EquippedItems.Count)
                {
                    Console.WriteLine("잘못된 번호입니다.");
                    GameManager.Instance.TotalThreadSleep();
                    return;
                }

                var item = GameManager.Instance.player.EquippedItems[index - 1];
                GameManager.Instance.player.Inventory.Add(item);
                GameManager.Instance.player.EquippedItems.RemoveAt(index - 1);
                Console.WriteLine($"{item.Name}을(를) 해제했습니다.");
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
