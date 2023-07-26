﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;

namespace TempestQuestDesk.Quests
{
    class BaseQuest : IQuest
    {
        //public QuestType GetQuestType() { return QuestType.BaseQuest; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reward { get; set; }
        public bool IsActive { get; set; }
        DataRow AsRow { get; set; }
        private DataTable table;

        public QuestType QuestType => QuestType.BaseQuest;

        public BaseQuest(string name, string description, string reward)
        {
            this.Name = name;
            this.Description = description;
            this.Reward = reward;
        }

        public BaseQuest(DataRow row) 
        {
            this.Id = (int)row["ID"];
            this.Name = row["Name"].ToString();
            this.Description = row["Description"].ToString();
            this.Reward = row["Reward"].ToString();
        }

        public DataRow ToRow()
        {
            table = new DataTable("BaseQuest");
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name");
            table.Columns.Add("Description");
            table.Columns.Add("Reward");
            table.Columns.Add("AsRow", typeof(bool));
            AsRow = table.NewRow();
            AsRow.ItemArray = new object[] {Id, Name, Description, Reward, IsActive};
            return AsRow;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
