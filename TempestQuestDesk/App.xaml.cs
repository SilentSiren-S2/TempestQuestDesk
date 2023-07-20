﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TempestQuestDesk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<FormCreateQuest> questWindows = new List<FormCreateQuest>();

        public List<FormCreateQuest> QuestWindows
        {
            get { return questWindows; }
            set { questWindows = value; }
        }
    }
}
