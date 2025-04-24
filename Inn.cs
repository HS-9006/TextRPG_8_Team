using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    public static class Inn
    {
        // 여관 이용 비용
        const int InnHealCost = 30;

        public static void VisitInn()
        {
            Console.WriteLine("\n===== 여관 =====");
            Console.WriteLine($"체력을 회복하시겠습니까? ({InnHealCost} Gold 소모)");
            Console.WriteLine($"현재 체력: {GameManager.Instance.player.CurrentHP} / {GameManager.Instance.player.TotalMaxHP}");
            Console.WriteLine($"보유 Gold: {GameManager.Instance.player.Gold}");
            Console.Write("1) 회복한다   0) 나간다 → ");

            string input = Console.ReadLine();

            if (input == "1")
            {
                if (GameManager.Instance.player.CurrentHP == GameManager.Instance.player.TotalMaxHP)
                {
                    Console.WriteLine("이미 체력이 가득합니다.");
                    Thread.Sleep(500);
                }
                else if (GameManager.Instance.player.Gold < InnHealCost)
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    Thread.Sleep(500);
                }
                else
                {
                    GameManager.Instance.player.Gold -= InnHealCost;
                    GameManager.Instance.player.CurrentHP = GameManager.Instance.player.TotalMaxHP;
                    Console.WriteLine("체력을 모두 회복했습니다!");
                    Thread.Sleep(500);
                }
            }
            else if (input == "0")
            {
                Console.WriteLine("여관을 나갑니다.");
                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(500);
            }

            Console.WriteLine();
        }

    }
}
