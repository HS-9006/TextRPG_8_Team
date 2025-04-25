using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TextRPG_8_Team;

public static class SaveSystem
{
	private static readonly string SaveFolder = Path.Combine(Directory.GetCurrentDirectory(), "save");
	private static readonly string SaveFilePath = Path.Combine(SaveFolder, "player_save.json");

	public static void Save(Player player)
	{
		if (!Directory.Exists(SaveFolder))
			Directory.CreateDirectory(SaveFolder);
		
		string json = JsonSerializer.Serialize(player, new JsonSerializerOptions {  WriteIndented = true });
		File.WriteAllText(SaveFilePath, json);
		Console.WriteLine("저장 완료됨.");
	}

	public static void Load()
	{
		if (!File.Exists(SaveFilePath))
		{
			Console.WriteLine("저장 파일이 없습니다.");
			return;
		}

		string json = File.ReadAllText(SaveFilePath);
		Player loaded = JsonSerializer.Deserialize<Player>(json);

		var player = GameManager.Instance.player;
		player.Name = loaded.Name;
		player.Job = loaded.Job;
		player.Level = loaded.Level;
		player.BaseAttack = loaded.BaseAttack;
		player.BaseDefense = loaded.BaseDefense;
		player.MaxHP = loaded.MaxHP;
		player.CurrentHP = loaded.CurrentHP;
		player.Gold = loaded.Gold;
		player.Inventory = loaded.Inventory;
		player.EquippedItems = loaded.EquippedItems;

		Console.WriteLine("불러오기 완료!");
	}
	public static void DeleteSave()
	{
		if(File.Exists(SaveFilePath))
		{
			File.Delete(SaveFilePath);
			Console.WriteLine("저장 파일 삭제됨.");
		}
		else
		{
			Console.WriteLine("저장 파일이 존재하지 않습니다.");
		}
	}

    public static void ShowSaveMenu()
    {
        Console.Clear();
        Console.WriteLine("=== 저장/ 불러오기 메뉴 ===");
        Console.WriteLine("1) 저장");
        Console.WriteLine("2) 불러오기");
        Console.WriteLine("3) 저장 파일 삭제");
        Console.WriteLine("0) 돌아가기");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                SaveSystem.Save(GameManager.Instance.player);
                break;
            case "2":
                SaveSystem.Load();
                break;
            case "3":
                SaveSystem.DeleteSave();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }

        GameManager.Instance.TotalThreadSleep();
    }
}

