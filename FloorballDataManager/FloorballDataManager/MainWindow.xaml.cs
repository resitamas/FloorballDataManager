using FloorballDataManager;
using FloorballServer.Models.Floorball;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static Dictionary<int,bool> Ids { get; set; }
        public static bool allowAddingId = true;
        public int selectedRadioButtonName;
        public int selectedId;

        public ObservableCollection<ListItemModel> RightList { get; set; }

        public ObservableCollection<ListItemModel> LeftList { get; set; }

        AddLeagueWindow addLeagueWindow;
        AddStadiumWindow addStadiumWindow;
        AddTeamWindow addTeamWindow;
        AddWindows.AddRefereeWindow addReferreWindow;
        AddWindows.AddPlayerWindow addPlayerWindow;
        AddWindows.AddMatchWindow addMatchWindow;
        EditMatchWindow editMatchWindow;

        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindow()
        {
            InitializeComponent();

            Ids = new Dictionary<int, bool>();

            buttonSave.IsEnabled = false;
            buttonBack.IsEnabled = false;

            comboBoxLeague.DataContext = this;
            leftList.DataContext = this;
            rightList.DataContext = this;

            comboBoxLeague.ItemsSource = ModelHelper.FillLeaguesComboBox();
            comboBoxLeague.SelectedIndex = 0;

            comboBoxTable.ItemsSource = new List<String>(new String[] {"Add player to team","Add player to match","Add referee to match"});
            comboBoxTable.SelectedIndex = 0;

            //ICollectionView view = CollectionViewSource.GetDefaultView(RightList);
            //new TextSearchFilter(view, filter);

            
        }


        private void comboBoxLeague_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxTable.SelectedValue == null)
                return;

            loadListBoxes(comboBoxLeague.SelectedValue ,comboBoxTable.SelectedIndex);            

        }

        private void comboBoxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxLeague.SelectedValue == null)
                return;

            loadListBoxes(comboBoxLeague.SelectedValue, comboBoxTable.SelectedIndex);
            ICollectionView view = CollectionViewSource.GetDefaultView(RightList);
            new TextSearchFilter(view, filter);
        }

        private ObservableCollection<ListItemModel> LoadListBoxWithPlayers()
        {
            ObservableCollection<ListItemModel> players = new ObservableCollection<ListItemModel>();

            try
            {
                List<PlayerModel> playersList = RESTHelper.GetAllPlayer();
                foreach (var player in playersList)
                {
                    var model = new ListItemModel();
                    model.Name = player.Name;
                    model.Id = player.RegNum;

                    players.Add(model);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            
            return players;

        }

        private ObservableCollection<ListItemModel> LoadListBoxWithTeams(int leagueId)
        {
            ObservableCollection<ListItemModel> teams = new ObservableCollection<ListItemModel>();

            try
            {
                List<TeamModel> teamList = RESTHelper.GetTeamsByLeague(leagueId);
                foreach (var team in teamList)
                {
                    var model = new ListItemModel();
                    model.Name = team.Name;
                    model.Id = team.Id;

                    teams.Add(model);
                }
            }
            catch (Exception)
            {

            }
            

            return teams;
        }

        private ObservableCollection<ListItemModel> LoadListBoxWithMatches(int leagueId)
        {
            ObservableCollection<ListItemModel> matches = new ObservableCollection<ListItemModel>();

            try
            {
                List<MatchModel> matchesList = RESTHelper.GetMatchesByLeague(leagueId);
                foreach (var match in matchesList)
                {
                    var model = new ListItemModel();
                    TeamModel homeTeam = RESTHelper.GetTeamById(match.HomeTeamId);
                    TeamModel awayTeam = RESTHelper.GetTeamById(match.AwayTeamId);
                    model.Name = homeTeam.Name + " - " + awayTeam.Name;
                    model.Id = match.Id;

                    matches.Add(model);
                }
            }
            catch (Exception)
            {

            }
            

            return matches;
        }

        private ObservableCollection<ListItemModel> LoadListBoxWithReferees()
        {
            ObservableCollection<ListItemModel> referees = new ObservableCollection<ListItemModel>();

            try
            {
                List<RefereeModel> refereesList = RESTHelper.GetAllReferee();

                foreach (var referee in refereesList)
                {
                    var model = new ListItemModel();
                    model.Name = referee.Name;
                    model.Id = referee.Id;

                    referees.Add(model);
                }
            }
            catch (Exception)
            {

            }
            

            return referees;
        }

        private void loadListBoxes(object year, object table)
        {

            try
            {
                if (year == null || table == null)
                    return;


                if (Convert.ToInt32(table) == 1)
                {
                    LeftList = LoadListBoxWithMatches(Convert.ToInt32(comboBoxLeague.SelectedValue));
                    OnPropertyChanged("LeftList");

                    RightList = LoadListBoxWithPlayers();
                    OnPropertyChanged("RightList");

                    //modelRight = LoadListBoxWithPlayers();
                    //modelLeft = LoadListBoxWithMatches(Convert.ToInt32(comboBoxLeague.SelectedValue));

                    //leftList.ItemsSource = modelLeft;
                    //rightList.ItemsSource = modelRight;


                }
                else
                {
                    if (Convert.ToInt32(table) == 0)
                    {

                        LeftList = LoadListBoxWithTeams(Convert.ToInt32(comboBoxLeague.SelectedValue));
                        OnPropertyChanged("LeftList");

                        RightList = LoadListBoxWithPlayers();
                        OnPropertyChanged("RightList");

                        //modelRight = LoadListBoxWithPlayers();
                        //modelLeft = LoadListBoxWithTeams(Convert.ToInt32(comboBoxLeague.SelectedValue));

                        //rightList.ItemsSource = modelRight;
                        //leftList.ItemsSource = modelLeft;
                    }
                    else
                    {
                        if (Convert.ToInt32(table) == 2)
                        {

                            LeftList = LoadListBoxWithReferees();
                            OnPropertyChanged("LeftList");

                            RightList = LoadListBoxWithMatches(Convert.ToInt32(comboBoxLeague.SelectedValue));
                            OnPropertyChanged("RightList");

                        }
                    }

                }
            }
            catch (Exception)
            {

            }
            


        }

        private void UpdateModelRight(int id)
        {
            try
            {
                if (comboBoxTable.SelectedIndex == 1)
                {
                    var match = RESTHelper.GetMatchById(id);
                    var homeTeamPlayers = RESTHelper.GetPlayersByTeam(match.HomeTeamId);
                    var awayTeamPlayers = RESTHelper.GetPlayersByTeam(match.AwayTeamId);

                    while (RightList.Count != 0)
                    {
                        RightList.RemoveAt(0);
                    }

                    List<PlayerModel> playersList = homeTeamPlayers;
                    playersList = playersList.Concat(awayTeamPlayers).ToList();
                    List<PlayerModel> players = RESTHelper.GetPlayersByMatch(match.Id);
                    foreach (var player in playersList)
                    {
                        var model = new ListItemModel(players.Select(p => p.RegNum).ToList().Contains(player.RegNum) ? true : false);
                        model.Name = player.Name;
                        model.Id = player.RegNum;

                        RightList.Add(model);
                    }

                }
                else
                {
                    if (comboBoxTable.SelectedIndex == 0)
                    {
                        allowAddingId = false;
                        List<PlayerModel> playersInTeam = RESTHelper.GetPlayersByTeam(id);

                        foreach (var p in RightList)
                        {
                            if (playersInTeam.Select(p1 => p1.RegNum).Contains(p.Id))
                            {
                                p.IsChecked = true;
                            }
                            else
                            {
                                p.IsChecked = false;
                            }
                        }
                        allowAddingId = true;
                    }
                    else
                    {
                        if (comboBoxTable.SelectedIndex == 2)
                        {
                            allowAddingId = false;
                            List<MatchModel> matches = RESTHelper.GetMatchesByReferee(selectedId);

                            foreach (var p in RightList)
                            {
                                if (matches.Select(m => m.Id).Contains(p.Id))
                                {
                                    p.IsChecked = true;
                                }
                                else
                                {
                                    p.IsChecked = false;
                                }
                            }
                            allowAddingId = true;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            
        }   

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            FrameworkElement element = sender as FrameworkElement;
            int id = Convert.ToInt32(element.Tag);
            selectedId = id;

            UpdateModelRight(id);

        }

        private void MaintainRadioButtons()
        {
            foreach (WrapPanel panel in Helper.FindVisualChildren<WrapPanel>(leftList))
            {
                var box = panel.Children.OfType<RadioButton>().First();
                if (box.IsChecked.Value)
                {
                    box.IsEnabled = true;
                }
                else
                {
                    if (Ids.Count == 0)
                    {
                        box.IsEnabled = true;
                    }
                    else
                    {
                        box.IsEnabled = false;
                    }

                }

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MaintainRadioButtons();

            if (Ids.Count == 0)
            {
                buttonSave.IsEnabled = false;
                buttonBack.IsEnabled = false;
            }
            else
            {
                buttonSave.IsEnabled = true;
                buttonBack.IsEnabled = true;
            }


        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxTable.SelectedIndex == 1)
                {
                    foreach (var id in Ids)
                    {
                        if (id.Value)
                        {
                            RESTHelper.AddPlayerToMatch(id.Key, selectedId);
                        }
                        else
                        {
                            RESTHelper.RemovePlayerFromMatch(id.Key, selectedId);
                        }
                    }
                }
                else
                {
                    if (comboBoxTable.SelectedIndex == 0)
                    {
                        foreach (var id in Ids)
                        {
                            if (id.Value)
                            {
                                RESTHelper.AddPlayerToTeam(id.Key, selectedId);
                            }
                            else
                            {
                                RESTHelper.RemovePlayerFromTeam(id.Key, selectedId);
                            }
                        }
                    }
                }
                Ids = new Dictionary<int, bool>();
                buttonSave.IsEnabled = false;
                buttonBack.IsEnabled = false;
                MaintainRadioButtons();
            }
            catch (Exception)
            {

            }
            
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Ids = new Dictionary<int, bool>();

            UpdateModelRight(selectedId);

            buttonSave.IsEnabled = false;
            buttonBack.IsEnabled = false;
        }

        private void AddLeagueClick(object sender, RoutedEventArgs e)
        {
            addLeagueWindow = new AddLeagueWindow();

            addLeagueWindow.Closed += AddLeagueWindow_Closed;

            addLeagueWindow.ShowDialog();

        }

        private void AddLeagueWindow_Closed(object sender, EventArgs e)
        {

            comboBoxLeague.ItemsSource = ModelHelper.FillLeaguesComboBox();
            comboBoxLeague.SelectedIndex = 0;

        }

        private void AddStadiumClick(object sender, RoutedEventArgs e)
        {
            addStadiumWindow = new AddStadiumWindow();

            addStadiumWindow.ShowDialog();
        }

        private void AddTeamClick(object sender, RoutedEventArgs e)
        {
            addTeamWindow = new AddTeamWindow();

            addTeamWindow.ShowDialog();
        }

        private void AddRefereeClick(object sender, RoutedEventArgs e)
        {
            addReferreWindow = new AddWindows.AddRefereeWindow();

            addReferreWindow.ShowDialog();
        }

        private void AddPlayerClick(object sender, RoutedEventArgs e)
        {
            addPlayerWindow = new AddWindows.AddPlayerWindow();

            addPlayerWindow.ShowDialog();
        }

        private void AddMatchClick(object sender, RoutedEventArgs e)
        {
            addMatchWindow = new AddWindows.AddMatchWindow();

            addMatchWindow.ShowDialog();
        }

        private void EditMatchClick(object sender, RoutedEventArgs e)
        {

            editMatchWindow = new EditMatchWindow(RESTHelper.GetMatchById(1));

            editMatchWindow.ShowDialog();

        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
