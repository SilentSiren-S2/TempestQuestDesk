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
    /// Interaction logic for TrackQuestPage.xaml
    /// </summary>
    public partial class TrackQuestPage : Page
    {
        public TrackQuestPage()
        {
            InitializeComponent();
        }

        private void lbTrackQuests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IQuest selectedQuest = (IQuest)lbTrackQuests.SelectedItem;
            if (selectedQuest != null)
            {
                MainController.OpenQuest(selectedQuest);
            }
        }

        internal void Load()
        {
            MainController.LoadQuests(QuestType.TrackQuest);
            lbTrackQuests.ItemsSource = null;
            lbTrackQuests.Items.Clear();
            lbTrackQuests.ItemsSource = MainController.questList;
        }
    }
}
