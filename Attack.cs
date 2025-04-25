namespace TextRPG_8_Team;

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