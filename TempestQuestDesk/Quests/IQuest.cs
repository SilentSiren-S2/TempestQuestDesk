using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempestQuestDesk.Quests
{
    internal interface IQuest
    {
        DataRow ToRow();
        string ToString();
        QuestType QuestType { get; }
        int Id { get; }
    }
}
