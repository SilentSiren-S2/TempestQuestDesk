using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TempestDB;
using TempestQuestDesk.Quests;

namespace TempestQuestDesk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Connector connector = new Connector();
        internal QuestController controller3 = new QuestController();
        DataTable table = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
            MainController.ConString("Server=DESKTOP-D6NNJMI;Database=TempestData;Trusted_Connection=True;");
            bRefresh_Click(this, null);
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            FormCreateQuest quest = new FormCreateQuest(QuestType.BaseQuest);
            quest.Owner = this;
            quest.ShowDialog();

            (Application.Current as App).CreateQuestWindows.Add(quest);
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbQuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IQuest selectedQuest = (IQuest)lbQuests.SelectedItem;
            if (selectedQuest != null)
            {
                MainController.OpenQuest(selectedQuest);
            }
        }

        private void lbQuests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IQuest selectedQuest = (IQuest)lbQuests.SelectedItem;
            if (selectedQuest != null)
            {
                MainController.OpenQuest(selectedQuest);
            }
        }

        private void bRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connector.ConnectionString = "Server=DESKTOP-D6NNJMI;Database=TempestData;Trusted_Connection=True;";
                var t = QuestType.BaseQuest.ToString();
                connector.CreateSelectCommand(t);
                table = connector.SelectExecute();
                MainController.LoadQuests(table);
                lbQuests.ItemsSource = null;
                lbQuests.Items.Clear();
                lbQuests.ItemsSource = MainController.questList;
            }
            catch
            {
            }
        }
    }
}
