using System;
using System.Linq;

namespace TextRPG_8_Team;

public class BattleManager
{
    public void Battle(List<Monster> monsters)
    {
        int beforeHp = GameManager.Instance.player.CurrentHP;
        while (GameManager.Instance.player.CurrentHP > 0 && monsters.Any(m => m.health > 0))
        {
            PlayerTurn(monsters);
            MonsterTurn(monsters);
        }
        if (monsters.All(m => m.health <= 0))
        {
            BattleResult.BattleResultMenu(true, monsters);
        }
        else if (GameManager.Instance.player.CurrentHP <= 0)
        {
            BattleResult.BattleResultMenu(false, monsters);
        }
    }
    public void PlayerTurn(List<Monster> monsters)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i];
                Console.ForegroundColor = monster.health <= 0 ? ConsoleColor.DarkGray : ConsoleColor.White;
                string hp = monster.health <= 0 ? "Dead" : $"HP {monster.health}";
                Console.WriteLine($"{i + 1} Lv.{monster.level} {monster.name} {hp}");
            }
            Console.ResetColor();
            Console.WriteLine("\n[내 정보]");
            Console.WriteLine($"Lv.{GameManager.Instance.player.Level} Chad{GameManager.Instance.player.Job}");
            Console.WriteLine($"HP {GameManager.Instance.player.CurrentHP}/{GameManager.Instance.player.MaxHP}");
            Console.WriteLine("\n0. 취소");
            Console.WriteLine("\n대상을 선택해주세요");
            Console.WriteLine(">>");
            bool isChoiceNum = int.TryParse(Console.ReadLine(), out int choiceNum);
            if (!isChoiceNum)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1000);
                continue;
            }

            if (choiceNum == 0)
            {
                break;
            }

            if (choiceNum >= 1 && choiceNum <= monsters.Count)
            {
                Monster target = monsters[choiceNum - 1];
                if (target.health > 0)
                {
                    AttackResult(target);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    continue;
                }
            }
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1000);
        }
    }
    public void AttackResult(Monster monster)
    {
        int beforeHp = monster.health;
        IDamageCalculator calculator = new PlayerBasicAttack();
        IAttack atk = new PlayerAttack(calculator);
        var (damage, isCritical, isMiss) = atk.Attack(monster);

        Console.Clear();
        Console.WriteLine("Battle!!\n");
        Console.WriteLine($"{GameManager.Instance.player.Name}의 공격!");
        if (isMiss)
        {
            Console.WriteLine($"Lv.{monster.name}이(가) 공격을 회피했습니다.\n");
        }
        else
        {
            Console.WriteLine($"Lv.{monster.level} {monster.name}을(를) 공격했습니다. [데미지 : {damage}]  {(isCritical ? "- 치명타 공격!!" : "")}\n");
        }
        Console.WriteLine($"Lv.{monster.level} {monster.name}");
        Console.WriteLine($"HP {beforeHp} -> {(monster.health <= 0 ? "Dead" : monster.health)}\n");
        Console.WriteLine("0. 다음\n");
        Console.WriteLine(">>");
        while (true)
        {
            bool isChoiceNum = int.TryParse(Console.ReadLine(), out int choiceNum);
            if (isChoiceNum && choiceNum == 0) break;

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
    public void MonsterTurn(List<Monster> monsters)
    {
        foreach (var monster in monsters)
        {
            if (monster.health <= 0) continue;
            int beforeHp = GameManager.Instance.player.CurrentHP;
            IDamageCalculator calculator = new MonsterBasicAttack();
            IAttack atk = new MonsterAttack(calculator);
            var (damage, _, isMiss) = atk.Attack(monster);

            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"Lv.{monster.level} {monster.name}의 공격!");
            if (isMiss)
            {
                Console.WriteLine($"{GameManager.Instance.player.Name}이(가) Lv.{monster.level} {monster.name}의 공격을 회피했습니다.\n");
            }
            else
            {
                Console.WriteLine($"{GameManager.Instance.player.Name}을(를) 공격했습니다. [데미지 : {damage}]\n");
            }
            Console.WriteLine($"Lv.{GameManager.Instance.player.Level} {GameManager.Instance.player.Name}");
            Console.WriteLine($"HP. {beforeHp} -> {GameManager.Instance.player.CurrentHP}\n");
            Console.WriteLine("0. 다음\n");
            Console.WriteLine("대상을 선택해주세요.");
            Console.WriteLine(">>");
            while (true)
            {
                bool isChoiceNum = int.TryParse(Console.ReadLine(), out int choiceNum);
                if (isChoiceNum && choiceNum == 0) break;

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}

public interface IAttack
{
    (int damage, bool isCritical, bool isMiss) Attack(Monster monster);
}

public class PlayerAttack : IAttack
{
    private readonly IDamageCalculator calculator;
    public PlayerAttack(IDamageCalculator calculator)
    {
        this.calculator = calculator;
    }
    public (int damage, bool isCritical, bool isMiss) Attack(Monster monster)
    {
        var (damage, isCritical, isMiss) = calculator.Calculate(GameManager.Instance.player.TotalAttack, monster.def);
        monster.health -= damage;

        return (damage, isCritical, isMiss);
    }
}

public class MonsterAttack : IAttack
{
    private readonly IDamageCalculator calculator;

    public MonsterAttack(IDamageCalculator calculator)
    {
        this.calculator = calculator;
    }

    public (int damage, bool isCritical, bool isMiss) Attack(Monster monster)
    {
        var (damage, isCritical, isMiss) = calculator.Calculate(monster.attack, GameManager.Instance.player.TotalDefense);
        GameManager.Instance.player.CurrentHP -= damage;
        if (GameManager.Instance.player.CurrentHP < 0) GameManager.Instance.player.CurrentHP = 0;

        return (damage, false, isMiss);
    }
}

public interface IDamageCalculator
{
    (int damage, bool isCritical, bool isMiss) Calculate(int atk, int def);
}
public class PlayerBasicAttack : IDamageCalculator
{
    private Random random = new Random();
    private double criticalChance = 0.15;
    private double criticalHit = 1.6;
    private double evasionChance = 0.5;

    public (int damage, bool isCritical, bool isMiss) Calculate(int atk, int def)
    {
        bool isMiss = random.NextDouble() < evasionChance;
        if (isMiss) return (0, false, true);

        int error = (int)Math.Ceiling(atk * 0.1);
        int min = atk - error;
        int max = atk + error;
        if (min < 0) min = 0;

        int damage = random.Next(min, max + 1);
        bool isCritical = random.NextDouble() < criticalChance;
        if (isCritical) damage = (int)Math.Ceiling(damage * criticalHit);

        int finalDamage = damage - def;
        if (finalDamage < 0) finalDamage = 0;

        return (finalDamage, isCritical, false);
    }
}
public class MonsterBasicAttack : IDamageCalculator
{
    private Random random = new Random();
    private double evasionChance = 0.5;

    public (int damage, bool isCritical, bool isMiss) Calculate(int atk, int def)
    {
        bool isMiss = random.NextDouble() < evasionChance;
        if (isMiss) return (0, false, true);

        int error = (int)Math.Ceiling(atk * 0.1);
        int min = atk - error;
        int max = atk + error;
        if (min < 0) min = 0;

        int damage = random.Next(min, max + 1);
        int finalDamage = damage - def;
        if (finalDamage < 0) finalDamage = 0;

        return (finalDamage, false, false);
    }
}