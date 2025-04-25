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

        //퀘스트를 클리어한 퀘스트리스트에 욺겨주는 함수
        public void MoveClearList(Quest _quest)
        {
            _quest.isMoveClear = true;
            QuestClaerList.Add(_quest);
            QuestList.Remove(_quest);
        }

        //퀘스트 이름을 적어주면 그 이름의 퀘스트를 찾아서 리턴해주는 함수
        public T SearchQuest<T>(string _questName) where T : Quest
        {
            return QuestManager.Instance.QuestList.FirstOrDefault(q => q.questName == _questName) as T;
        }
    }
}
