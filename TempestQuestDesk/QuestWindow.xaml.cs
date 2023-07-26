using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QuestUCLib;
using TempestQuestDesk.Quests;

namespace TempestQuestDesk
{
    /// <summary>
    /// Interaction logic for QuestWindow.xaml
    /// </summary>
    public partial class QuestWindow : Window
    {
        //private int questID;
        //private QuestType questType;
        private IQuest holdingQuest;
        private UserControl ucQuest;
        public QuestWindow()
        {
            InitializeComponent();
        }

        internal QuestWindow(IQuest quest) : this()
        {
            holdingQuest = quest;
            switch (quest.QuestType)
            {
                case QuestType.BaseQuest:
                    BaseQuest baseQuest = (BaseQuest)quest;
                    this.Title = baseQuest.Name;
                    ucQuest = new UCBaseQuest(baseQuest.Name, baseQuest.Description, baseQuest.Reward);
                    pMain.Children.Add(ucQuest);
                    break;
            }
        }

        public void OpenQuest(QuestType questType)
        {

        }

        private void bUpdateQuest_Click(object sender, RoutedEventArgs e)
        {
            switch(holdingQuest.QuestType)
            {
                case QuestType.BaseQuest:
                    (ucQuest as UCBaseQuest).GetFields(out var name, out var description, out var reward);
                    BaseQuest baseQuest = (BaseQuest)holdingQuest;
                    baseQuest.Name = name; 
                    baseQuest.Description = description; 
                    baseQuest.Reward = reward;
                    holdingQuest = baseQuest;
                    break;
            }
            MainController.UpdateQuest(holdingQuest);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainController.openedQuestList.Remove(holdingQuest);
        }
    }
}
