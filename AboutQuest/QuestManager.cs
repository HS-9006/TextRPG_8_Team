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

        public Guild Guild = GameManager.Instance.guild;

        public List<Quest> QuestList = new List<Quest>();
        public List<Quest> QuestClaerList = new List<Quest>();

        public void MoveClearList(Quest _quest)
        {
            _quest.isMoveClear = true;
            QuestClaerList.Add(_quest);
            QuestList.Remove(_quest);
        }
        public T SearchQuest<T>(string _questName) where T : Quest
        {
            return QuestManager.Instance.QuestList.FirstOrDefault(q => q.questName == _questName) as T;
        }
    }
}
