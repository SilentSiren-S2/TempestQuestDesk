using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TempestQuestDesk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<FormCreateQuest> createQuestWindows = new List<FormCreateQuest>();
        private List<QuestWindow> questWindows = new List<QuestWindow>();
        public List<FormCreateQuest> CreateQuestWindows
        {
            get { return createQuestWindows; }
            set { createQuestWindows = value; }
        }
        public List<QuestWindow> QuestWindows
        {
            get { return questWindows; }
            set { questWindows = value; }
        }
    }
}
