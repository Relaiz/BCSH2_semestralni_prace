using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using System.Data;
using System.Windows;
using Oracle.ManagedDataAccess.Types;
using BCSH2_BDAS2_SemPrace.ViewModel;
using System.Windows.Controls;

namespace BCSH2_BDAS2_SemPrace.View
{
    /// <summary>
    /// Interaction logic for KlientWindow.xaml
    /// </summary>
    public partial class KlientWindow : Window
    {
        public Klient Klient { get; set; }
        public ListView AccountsList
        {
            get { return accountsList; }
        }
        public KlientWindow(Klient klient)
        {
            InitializeComponent();
            Klient = klient;
            // Create an instance of KlientViewModel and set it as the DataContext
            KlientViewModel viewModel = new KlientViewModel(klient);
            this.DataContext = viewModel;
            accountsList.ItemsSource = ((KlientViewModel)DataContext).ListOfKlientUcty;
            // Subscribe to the event
            
        }
    }
}
