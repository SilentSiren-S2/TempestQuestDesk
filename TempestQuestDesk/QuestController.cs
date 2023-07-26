using TempestQuestDesk.Quests;
using TempestDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;

namespace TempestQuestDesk
{
    public enum QuestType : int
    {
        BaseQuest = 0
    }

    internal class QuestController
    {
        public DataTable Quests { get; set; }
        private string conString;
        private string tableName;
        private Connector connector = new Connector();
        private List<IQuest> questList = new List<IQuest> { };

        public QuestController() { }

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

    internal static class MainController
    {
        public static DataTable Quests { get; set; }
        private static string conString;
        private static string tableName;
        public static string ConString(string value) => conString = value;
        public static string TableName(string value) => tableName = value;
        private static Connector connector = new Connector();
        public static List<IQuest> questList = new List<IQuest> { };

        public static void AddQuest(IQuest quest)
        {
            questList.Add(quest);
        }

        public static void LoadQuests(DataTable dataTable)
        {
            questList.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                IQuest quest = new BaseQuest(row);
                AddQuest(quest);
            }
        }

        public static void CreateQuest(string name, string description, string reward)
        {
            BaseQuest quest = new BaseQuest(name, description, reward);
            questList.Add(quest);
            PushToDB(quest);
        }

        private static void PushToDB(IQuest quest)
        {
            DataRow row = quest.ToRow(Quests);
            row.ItemArray[0] = DBNull.Value;
            connector.ConnectionString = conString;
            connector.CreateInsertCommand(tableName, row);
            connector.InsertExecute();
        }

        internal static void OpenQuest(IQuest selectedQuest)
        {
            QuestWindow questWindow = new QuestWindow(selectedQuest);
            questWindow.Show();
            (Application.Current as App).QuestWindows.Add(questWindow);
        }
    }
}
