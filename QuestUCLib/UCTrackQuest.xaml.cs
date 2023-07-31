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
using System.Xml.Linq;

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
            tbProgress.IsReadOnly = true;
            tbProgress.Text = $"{CurrentProgress}/{goal}";
        }

        public void GetFields(out string name, out string description, out string reward, out int goal)
        {
            name = tbName.Text;
            description = tbDescription.Text;
            reward = tbReward.Text;
            goal = Int32.Parse(tbProgress.Text);
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
            tbProgress.Text = $"{pbProgress.Value}/{pbProgress.Maximum}";
        }

        private void bDown_Click(object sender, RoutedEventArgs e)
        {
            CurrentProgress -= Int32.Parse(tbCounter.Text);
            pbProgress.Value = CurrentProgress;
            tbProgress.Text = $"{pbProgress.Value} / {pbProgress.Maximum}";
        }

        private void tbProgress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
