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
                case QuestType.TrackQuest:
                    TrackQuest trackQuest = (TrackQuest)quest;
                    this.Title = trackQuest.Name;
                    ucQuest = new UCTrackQuest(trackQuest.Name, trackQuest.Description, trackQuest.Reward, trackQuest.Goal, trackQuest.CurrentProgress);
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
                    (ucQuest as UCBaseQuest).GetFields(out var bqname, out var bqdescription, out var bqreward);
                    BaseQuest baseQuest = (BaseQuest)holdingQuest;
                    baseQuest.Name = bqname; 
                    baseQuest.Description = bqdescription; 
                    baseQuest.Reward = bqreward;
                    holdingQuest = baseQuest;
                    break;
                case QuestType.TrackQuest:
                    (ucQuest as UCTrackQuest).GetFields(out var tqname, out var tqdescription, out var tqreward, out int goal, out int currentProgress);
                    TrackQuest trackQuest = (TrackQuest)holdingQuest;
                    trackQuest.Name = tqname;
                    trackQuest.Description = tqdescription;
                    trackQuest.Reward = tqreward;
                    trackQuest.Goal = goal;
                    trackQuest.CurrentProgress = currentProgress;
                    holdingQuest = trackQuest;
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
