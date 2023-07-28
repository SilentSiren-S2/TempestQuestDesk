using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TempestQuestDesk.Quests
{
    internal class TrackQuest : BaseQuest, IQuest
    {
        public int CurrentProgress { get; set; }
        public int Goal { get; set; }
        DataRow AsRow { get; set; }
        public new QuestType QuestType => QuestType.TrackQuest;
        public TrackQuest(DataRow row) : base(row)
        {
            this.Goal = (int)row["Goal"];
            this.CurrentProgress = (int)row["CurrentProgress"];
        }

        public TrackQuest(string name, string description, string reward, int goal, int currentProgress) : this(name, description, reward, true)
        {
            Goal = goal;
            CurrentProgress = currentProgress;
        }

        public TrackQuest(string name, string description, string reward, bool isActive) : base(name, description, reward, isActive)
        {
        }

        public new DataRow ToRow()
        {
            table = new DataTable("TrackQuest");
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name");
            table.Columns.Add("Description");
            table.Columns.Add("Reward");
            table.Columns.Add("Active");
            table.Columns.Add("Goal");
            table.Columns.Add("CurrentProgress");
            AsRow = table.NewRow();
            AsRow.ItemArray = new object[] { Id, Name, Description, Reward, IsActive ? 1 : 0, Goal, CurrentProgress };
            return AsRow;
        }
    }
}
