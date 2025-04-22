namespace TextRPG_8_Team
{
    internal class Program
    {
        static StartPage StartPage = new StartPage();
        static Player Player = new Player();
        static void Main(string[] args)
        {
            StartPage.StartGame(Player);
        }
    }
}