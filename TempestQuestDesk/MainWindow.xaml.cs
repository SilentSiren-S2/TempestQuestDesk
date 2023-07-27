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
        QuestType curType;
        public MainWindow()
        {
            InitializeComponent();
            QuestType[] questTypes = (QuestType[])Enum.GetValues(typeof(QuestType));

            foreach (QuestType questType in questTypes)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Content = questType.ToString();
                radioButton.Click += rb_Click;
                radioButton.Tag = questType;
                spTypes.Children.Add(radioButton);
            }

            MainController.ConString("Server=DESKTOP-D6NNJMI;Database=TempestData;Trusted_Connection=True;");
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            FormCreateQuest quest = new FormCreateQuest(curType);
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
                //switch()
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

        private void rb_Click(object sender, RoutedEventArgs e)
        {
            RadioButton clickedRadioButton = (RadioButton)sender;
            switch (clickedRadioButton.Tag)
            {
                case (QuestType.BaseQuest):
                    curType = QuestType.BaseQuest;
                    BaseQuestPage bqPage = new BaseQuestPage();
                    bqPage.Load();
                    fMain.Content = bqPage;
                    break;
                case (QuestType.TrackQuest): 
                    curType = QuestType.TrackQuest;
                    TrackQuestPage tqPage = new TrackQuestPage();
                    tqPage.Load();
                    fMain.Content = tqPage;
                    break;
                default:
                    break;
            }
        }
    }
}
