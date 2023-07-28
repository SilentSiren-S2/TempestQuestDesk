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
        private int CurrentProgress;
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
            tbGoal.Visibility = Visibility.Hidden;
            CurrentProgress = currentProgress;
            tbProgress.Text = $"{CurrentProgress}/{goal}";
        }

        public void GetFields(out string name, out string description, out string reward, out int goal, out int currentProgress)
        {
            name = tbName.Text;
            description = tbDescription.Text;
            reward = tbReward.Text;
            goal = (int)pbProgress.Maximum;
            currentProgress = CurrentProgress;
        }

        private void bUp_Click(object sender, RoutedEventArgs e)
        {
            CurrentProgress += Int32.Parse(tbCounter.Text);
            pbProgress.Value = CurrentProgress;
        }

        private void bDown_Click(object sender, RoutedEventArgs e)
        {
            CurrentProgress -= Int32.Parse(tbCounter.Text);
            pbProgress.Value = CurrentProgress;
        }
    }
}
