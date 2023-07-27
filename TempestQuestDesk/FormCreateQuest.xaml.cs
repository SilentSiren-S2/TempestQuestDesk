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
                    ucQuest = new UCBaseQuest();
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
                        (ucQuest as UCBaseQuest).GetFields(out var name, out var description, out var reward);
                        BaseQuest baseQuest = new BaseQuest(name, description, reward); 
                        MainController.CreateBaseQuest(name, description, reward);
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
