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
        DataTable table = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
            MainController.ConString("Server=DESKTOP-D6NNJMI;Database=TempestData;Trusted_Connection=True;");
            try
            {
                connector.ConnectionString = "Server=DESKTOP-D6NNJMI;Database=TempestData;Trusted_Connection=True;";
                connector.CreateSelectCommand("BaseQuest");
                table = connector.SelectExecute();
                MainController.TableName("BaseQuest");
                MainController.LoadQuests(table); 
                lbQuests.Items.Clear();
                lbQuests.ItemsSource = MainController.questList;
            }
            catch
            {
            }
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            FormCreateQuest quest = new FormCreateQuest();
            quest.Owner = this;
            quest.ShowDialog();

            (Application.Current as App).QuestWindows.Add(quest);
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbQuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IQuest selectedQuest = (IQuest)lbQuests.SelectedItem;
        }
    }
}
