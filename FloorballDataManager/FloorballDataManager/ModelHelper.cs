using FloorballDataManager;
using FloorballServer.Models.Floorball;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Model;

namespace WpfApplication1
{
    public class ModelHelper
    {

        public static ObservableCollection<ComboboxModel> FillLeaguesComboBox()
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<LeagueModel> list = RESTHelper.GetAllLeague();
            foreach (var l in list)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = l.Name + " - " + l.Year.ToString("yyyy");
                model.Id = l.Id;
                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillStadiumsComboBox()
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<StadiumModel> list = RESTHelper.GetAllStadium();
            foreach (var l in list)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = l.Name;
                model.Id = l.Id;
                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillTeamsComboBox(int leagueId)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<TeamModel> list = RESTHelper.GetTeamsByLeague(leagueId);
            foreach (var l in list)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = l.Name;
                model.Id = l.Id;
                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<int> FillRoundsComboBox(int leagueId)
        {
            ObservableCollection<int> comboboxModel = new ObservableCollection<int>();
            int round = RESTHelper.GetRoundsByLeague(leagueId);
            for (int i = 1; i <= round; i++)
            {
                comboboxModel.Add(i);
            }

            return comboboxModel;
        }

        public static ObservableCollection<Model.EventModel> FillEventsList(int matchId)
        {
            ObservableCollection<Model.EventModel> eventModels = new ObservableCollection<Model.EventModel>();

            List<FloorballServer.Models.Floorball.EventModel> events = RESTHelper.GetEventsByMatch(matchId);

            foreach (var e in events)
            {

                if (e.Type != "A")
                {
                    Model.EventModel model = new Model.EventModel();
                    EventMessageModel eventMessage = RESTHelper.GetEventMessageById(e.EventMessageId);

                    if (e.Type == "G")
                    {
                        model.EventText = "Gól: " + RESTHelper.GetPlayerById(e.PlayerId).Name;
                        model.EventText += " Assziszt: " + RESTHelper.GetPlayerById(events.Find(ev => ev.Time == e.Time && ev.Type == "A").PlayerId).Name;

                        model.EventText += eventMessage.Code != 605 ? " - " + eventMessage.Message + " gól" : "";

                    }
                    else
                    {
                        if (e.Type == "P2")
                        {
                            model.EventText = "2 perc: " + RESTHelper.GetPlayerById(e.PlayerId).Name + " kiállították.";
                            model.EventText += " Megnevezés: " + eventMessage.Message;
                        }
                        else
                        {
                            if (e.Type == "P5")
                            {
                                model.EventText = "5 perc: " + RESTHelper.GetPlayerById(e.PlayerId).Name + " kiállították.";
                                model.EventText += " Megnevezés: " + eventMessage.Message;
                            }
                            else
                            {
                                if (e.Type == "P10")
                                {
                                    model.EventText = "10 perc: " + RESTHelper.GetPlayerById(e.PlayerId).Name + " kiállították.";
                                    model.EventText += " Megnevezés: " + eventMessage.Message;
                                }
                                else
                                {
                                    if (e.Type == "PV")
                                    {
                                        model.EventText = "Végleges: " + RESTHelper.GetPlayerById(e.PlayerId).Name + " kiállították.";
                                        model.EventText += " Megnevezés: " + eventMessage.Message;
                                    }
                                    else
                                    {
                                        if (e.Type == "I")
                                        {
                                            //model.EventText = "Időkérérés: " +  + " kiállították.";
                                            //model.EventText += " Megnevezés: " + e.EventMessage.Message;
                                        }
                                        else
                                        {
                                            if (e.Type == "B")
                                            {
                                                model.EventText = "Büntető rontás: " + RESTHelper.GetPlayerById(e.PlayerId).Name + " büntetőt rontott.";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    model.Time = e.Time.Split(':')[1];
                    model.Id = e.Id;

                    eventModels.Add(model);
                }
                
            }

            return eventModels;
        }

        public static ObservableCollection<ComboboxModel> FillPlayerCombobox(MatchModel m)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<PlayerModel> players = RESTHelper.GetPlayersByMatch(m.Id).ToList();
            foreach (var player in players)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = player.Name;
                model.Id = player.Number;

                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillEventMessageCombobox(string eventType)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            char category = ' ';

            if (eventType == "P2")
            {
                category = '2';
            } else
            {
                if (eventType == "P5")
                {
                    category = '5';
                }
                else
                {
                    if (eventType == "P10")
                    {
                        category = '1';
                    }
                    else
                    {
                        if (eventType == "V")
                        {
                            category = '3';
                        }
                        else
                        {
                            if (eventType == "G")
                            {
                                category = '6';
                            }
                            else
                            {
                                if (eventType == "B" || eventType == "I")
                                {
                                    category = '4';
                                }
                            }
                        }
                    }
                }
            }

            List<EventMessageModel> eventMessages = RESTHelper.GetEventMessagesByCategory(category);
            foreach (var e in eventMessages)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = e.Message + " (" + e.Code + ")";
                model.Id = e.Id;

                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillHomeAndAwayTeamCombobox(MatchModel m)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<TeamModel> teams = new List<TeamModel>();
            teams.Add(RESTHelper.GetTeamById(m.HomeTeamId));
            teams.Add(RESTHelper.GetTeamById(m.AwayTeamId));
            foreach (var team in teams)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = team.Name;
                model.Id = team.Id;

                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillComboboxFromStringList(List<string> list)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();

            foreach (var l in list)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = l;
                model.Id = -1;

                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillComboboxTeamMembersPlayedInMatch(TeamModel t, MatchModel m)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<PlayerModel> players = RESTHelper.GetPlayersByMatch(m.Id);

            foreach (var p in RESTHelper.GetPlayersByTeam(t.Id))
            {
                if (players.Select(p1 => p1.RegNum).Contains(p.RegNum))
                {
                    ComboboxModel model = new ComboboxModel();
                    model.Name = p.Name + " (" + p.Number + ")";
                    model.Id = p.RegNum;

                    comboboxModel.Add(model);
                }
                
            }

            return comboboxModel;
        }

        //public static EventModel CreateEventModel(int eventId1, int eventId2)
        //{

        //    Event e1 = DatabaseManager.GetEvent(eventId1);

        //    EventModel model = new EventModel();
        //    model.Id = e1.Id;
        //    model.Time = e1.Time.ToString(@"mm\:ss");

        //    if (e1.Type == "G")
        //    {

        //        Event e2 = DatabaseManager.GetEvent(eventId2);

            
        //        model.EventText = "Gól: " + e1.Player.Name;
        //        model.EventText += " Assziszt: " + e2.Player.Name;
        //        model.EventText += e1.EventMessage.Code != 605 ? " - " + e1.EventMessage.Message + " gól" : "";

        //    }


        //    return model;
        //}

    }
}
