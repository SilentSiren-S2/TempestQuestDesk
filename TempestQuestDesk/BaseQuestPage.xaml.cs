using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TempestQuestDesk.Quests;

namespace TempestQuestDesk
{
    /// <summary>
    /// Interaction logic for BaseQuestPage.xaml
    /// </summary>
    public partial class BaseQuestPage : Page
    {
        public BaseQuestPage()
        {
            InitializeComponent();
        }

        private void lbBaseQuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IQuest selectedQuest = (IQuest)lbBaseQuests.SelectedItem;
            if (selectedQuest != null)
            {
                MainController.OpenQuest(selectedQuest);
            }
        }

        private void lbBaseQuests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IQuest selectedQuest = (IQuest)lbBaseQuests.SelectedItem;
            if (selectedQuest != null)
            {
                MainController.OpenQuest(selectedQuest);
            }
        }

        internal void Load()
        {
            MainController.LoadQuests(QuestType.BaseQuest);
            lbBaseQuests.ItemsSource = null;
            lbBaseQuests.Items.Clear();
            lbBaseQuests.ItemsSource = MainController.questList;
        }
    }
}
