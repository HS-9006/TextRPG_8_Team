using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    internal class QuestManager
    {
        //싱글톤
        private static QuestManager instance;
        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance;
            }
        }
        private QuestManager() { }

        public Guild Guild = new Guild();

        public List<Quest> QuestList = new List<Quest>();
        
        public void AddQuest(Quest _quest)
        {
            QuestList.Add(_quest);
        }
    }
}
