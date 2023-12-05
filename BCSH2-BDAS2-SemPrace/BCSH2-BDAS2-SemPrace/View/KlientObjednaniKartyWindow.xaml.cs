﻿using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.ViewModel;
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
using System.Windows.Shapes;

namespace BCSH2_BDAS2_SemPrace.View
{
    /// <summary>
    /// Interaction logic for KlientObjednaniKartyWindow.xaml
    /// </summary>
    public partial class KlientObjednaniKartyWindow : Window
    {
        public KlientObjednaniKartyWindow(Ucet ucet, Klient klient)
        {
            InitializeComponent();
            DataContext = new KlientObjednaniKartyViewModel(ucet, klient);
        }
    }
}
