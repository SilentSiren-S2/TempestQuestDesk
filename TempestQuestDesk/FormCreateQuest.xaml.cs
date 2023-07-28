using QuestUCLib;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;
using TempestQuestDesk.Quests;

namespace TempestQuestDesk
{
    /// <summary>
    /// Interaction logic for FormCreateQuest.xaml
    /// </summary>
    public partial class FormCreateQuest : Window
    {
        private UserControl ucQuest;
        private QuestType questType;
        public FormCreateQuest()
        {
            InitializeComponent();
        }

        internal FormCreateQuest(QuestType questType) : this()
        {
            this.questType = questType;
            switch (questType)
            {
                case QuestType.BaseQuest:
                default:
                    ucQuest = new UCBaseQuest();
                    pMain.Children.Add(ucQuest);
                    break;
                case QuestType.TrackQuest:
                    ucQuest = new UCTrackQuest();
                    pMain.Children.Add(ucQuest);
                    break;
            }
        }

        private void createQuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (questType)
                {
                    case QuestType.BaseQuest:
                        (ucQuest as UCBaseQuest).GetFields(out var bqname, out var bqdescription, out var bqreward);
                        BaseQuest baseQuest = new BaseQuest(bqname, bqdescription, bqreward); 
                        MainController.CreateBaseQuest(bqname, bqdescription, bqreward);
                        break;
                    case QuestType.TrackQuest:
                        (ucQuest as UCTrackQuest).GetFields(out var tqname, out var tqdescription, out var tqreward, out var tqgoal);
                        TrackQuest trackQuest = new TrackQuest(tqname, tqdescription, tqreward, tqgoal, 0);
                        MainController.CreateTrackQuest(trackQuest);
                        break;
                }
                
                DialogResult = true;
            }
            catch
            {

            }
        }
    }
}
