using TempestQuestDesk.Quests;
using TempestDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Collections;
using System.Windows.Documents;

namespace TempestQuestDesk
{
    public enum QuestType : int
    {
        BaseQuest = 0,
        TrackQuest = 1,
        TempQuest = 2,
        Epic = 4,
        SubQuest = 5
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
            DataRow row = quest.ToRow();
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
        public static List<IQuest> openedQuestList = new List<IQuest> { };

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

        public static void LoadQuests(QuestType questType)
        {
            connector.ConnectionString = "Server=DESKTOP-D6NNJMI;Database=TempestData;Trusted_Connection=True;";
            connector.CreateSelectCommand(questType.ToString());
            var table = connector.SelectExecute();
            LoadQuests(table);
        }

        public static void CreateBaseQuest(string name, string description, string reward)
        {
            BaseQuest quest = new BaseQuest(name, description, reward);
            PushToDB(quest);
            questList.Add(quest);
        }

        internal static void CreateTrackQuest(TrackQuest trackQuest)
        {
            PushToDB(trackQuest);
            questList.Add(trackQuest);
        }

        internal static void CreateTrackQuest(string tqname, string tqdescription, string tqreward, int tqgoal)
        {
            throw new NotImplementedException();
        }

        private static void PushToDB(IQuest quest)
        {
            DataRow row = quest.ToRow();
            row.Table.Columns.Remove("ID");
            connector.ConnectionString = conString;
            connector.CreateInsertCommand(quest.QuestType.ToString(), row);
            connector.InsertExecute();
        }

        internal static void OpenQuest(IQuest selectedQuest)
        {
            foreach (IQuest quest in openedQuestList)
            {
                if (quest.Id == selectedQuest.Id && quest.QuestType == selectedQuest.QuestType)
                {
                    return;
                }
            }
            QuestWindow questWindow = new QuestWindow(selectedQuest);
            questWindow.Show();
            (Application.Current as App).QuestWindows.Add(questWindow);
            openedQuestList.Add(selectedQuest);
        }

        internal static void UpdateQuest(IQuest holdingQuest)
        {
            try
            {
                DataRow row = holdingQuest.ToRow();
                connector.ConnectionString = conString;
                connector.CreateUpdateCommand(holdingQuest.QuestType.ToString(), row);
                connector.UpdateExecute();
            }
            catch { }
        }
    }
}
