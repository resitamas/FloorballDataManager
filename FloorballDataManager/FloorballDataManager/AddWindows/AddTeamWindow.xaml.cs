﻿using FloorballDataManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for AddTeamWindow.xaml
    /// </summary>
    public partial class AddTeamWindow : Window
    {
        public string Year { get; set; }
        public string TeamName { get; set; }
        public string Coach { get; set; }
        public string Sex { get; set; }


        public AddTeamWindow()
        {
            InitializeComponent();

            nameText.DataContext = this;
            years.DataContext = this;
            coach.DataContext = this;
            stadium.DataContext = this;
            league.DataContext = this;
            sex.DataContext = this;

            league.ItemsSource = ModelHelper.FillLeaguesComboBox();
            stadium.ItemsSource = ModelHelper.FillStadiumsComboBox();
            years.ItemsSource = new string[] { "2015", "2014", "2013" };
            sex.ItemsSource = new string[] { "férfi", "nő"};
        }

        private void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Year != null && Sex != null && TeamName != null && TeamName != "" && Coach != null && Coach != "" && stadium.SelectedValue != null && league.SelectedValue != null ;
        }

        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                RESTHelper.AddTeam(TeamName, DateTime.ParseExact(Year, "yyyy", CultureInfo.InvariantCulture), Coach, Sex, Convert.ToInt32(stadium.SelectedValue), Convert.ToInt32(league.SelectedValue));

                MessageBox.Show("A csapat sikeresen létrejött!", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception)
            {

            }
            
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
