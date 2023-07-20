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

namespace TempestQuestDesk
{
    /// <summary>
    /// Interaction logic for FormCreateQuest.xaml
    /// </summary>
    public partial class FormCreateQuest : Window
    {
        public FormCreateQuest()
        {
            InitializeComponent();
        }

        private void createQuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QuestController controller = new QuestController("Server=DESKTOP-D6NNJMI;Database=TempestData;Trusted_Connection=True;", "BaseQuest");
                controller.Quests = new System.Data.DataTable("BaseQuest");
                //DataColumn columnId = new DataColumn("ID");
                DataColumn columnName = new DataColumn("Name");
                DataColumn columnDescription = new DataColumn("Description");
                DataColumn columnReward = new DataColumn("Reward");
                //controller.Quests.Columns.Add(columnId);
                controller.Quests.Columns.Add(columnName);
                controller.Quests.Columns.Add(columnDescription);
                controller.Quests.Columns.Add(columnReward);
                controller.CreateQuest(tbName.Text, tbDescription.Text, tbBounty.Text);
                DialogResult = true;
            }
            catch
            {

            }
        }
    }
}
