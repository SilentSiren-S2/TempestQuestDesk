﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TempestQuestDesk.Quests
{
    class BaseQuest : IQuest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reward { get; set; }
        DataRow AsRow { get; set; }

        public BaseQuest(string name, string description, string reward)
        {
            this.Name = name;
            this.Description = description;
            this.Reward = reward;
        }

        public DataRow ToRow(DataTable table)
        {
            AsRow = table.NewRow();
            AsRow.ItemArray = new object[] { Name, Description, Reward };
            return AsRow;
        }
    }
}