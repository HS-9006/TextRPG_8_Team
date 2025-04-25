namespace TextRPG_8_Team;

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