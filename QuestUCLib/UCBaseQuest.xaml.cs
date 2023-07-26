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
    /// Interaction logic for UCBaseQuest.xaml
    /// </summary>
    public partial class UCBaseQuest : UserControl
    {
        public UCBaseQuest()
        {
            InitializeComponent();
        }

        public UCBaseQuest(string name, string description, string reward) : this() 
        {
            tbName.Text = name;
            tbDescription.Text = description;
            tbReward.Text = reward;
        }
    }
}
