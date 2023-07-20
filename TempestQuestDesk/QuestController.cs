using TempestQuestDesk.Quests;
using TempestDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TempestQuestDesk
{
    internal class QuestController
    {
        public DataTable Quests { get; set; }
        private string conString;
        private string tableName;
        private Connector connector = new Connector();
        private List<IQuest> questList = new List<IQuest> { };

        public QuestController(string conString, string tableName)
        {
            this.conString = conString;
            this.tableName = tableName;
            connector.ConnectionString = conString;
        }

        public void CreateQuest(string name, string description, string reward)
        {
            BaseQuest quest = new BaseQuest(name, description, reward);
            questList.Add(quest);
            PushToDB(quest);
        }
        private void PushToDB(IQuest quest)
        {
            DataRow row = quest.ToRow(Quests);
            row.ItemArray[0] = DBNull.Value;
            connector.CreateInsertCommand(tableName, row);
            connector.InsertExecute();
        }
    }
}
