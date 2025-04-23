using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    internal class GameManager
    {
        //싱글톤
        public static GameManager instance;

        public static GameManager Instance()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }

        public Player player = new Player();
        public StartPage startPage = new StartPage();
        public Quest quest = new Quest();

        public void GameStart()
        {
            startPage.StartGame();
        }
    }
}
