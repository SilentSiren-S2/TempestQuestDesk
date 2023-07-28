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

namespace QuestUCLib
{
    /// <summary>
    /// Interaction logic for UCTrackQuest.xaml
    /// </summary>
    public partial class UCTrackQuest : UserControl
    {
        public UCTrackQuest()
        {
            InitializeComponent();
        }

        public UCTrackQuest(string name, string description, string reward, int goal, int currentProgress) : this()
        {
            tbName.Text = name;
            tbDescription.Text = description;
            tbReward.Visibility = Visibility.Hidden;
            pbProgress.Maximum = goal;
            pbProgress.Value = currentProgress;
            lGoal.Visibility = Visibility.Hidden;
            tbGoal.Visibility = Visibility.Hidden;
            tbProgress.Text = $"{currentProgress}/{goal}";
        }

        public void GetFields(out string name, out string description, out string reward, out int goal)
        {
            name = tbName.Text;
            description = tbDescription.Text;
            reward = tbReward.Text;
            goal = Int32.Parse(tbGoal.Text);
        }
    }
}
