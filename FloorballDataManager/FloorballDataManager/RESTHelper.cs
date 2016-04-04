using FloorballServer.Models.Floorball;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1;


namespace FloorballDataManager
{
    public class RESTHelper
    {
        private static JsonDeserializer deserial = new JsonDeserializer();

        public static List<PlayerModel> GetAllPlayer()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/players", Method.GET) as RestResponse;

                return deserial.Deserialize<List<PlayerModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static List<TeamModel> GetTeamsByLeague(int leagueId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/leagues/{id}/teams", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<List<TeamModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static TeamModel GetTeamById(int teamId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", teamId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/teams/{id}", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<TeamModel>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static List<MatchModel> GetMatchesByLeague(int leagueId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/leagues/{id}/matches", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<List<MatchModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static MatchModel GetMatchById(int matchId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/matches/{id}", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<MatchModel>(response);

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public static List<RefereeModel> GetAllReferee()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/referees", Method.GET) as RestResponse;

                return deserial.Deserialize<List<RefereeModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static RefereeModel GetRefereeById(int refereeId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", refereeId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/referee/{id}", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<RefereeModel>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static List<PlayerModel> GetPlayersByTeam(int teamId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", teamId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/teams/{id}/players", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<List<PlayerModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static List<MatchModel> GetMatchesByReferee(int refereeId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", refereeId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/referee/{id}/matches", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<List<MatchModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static void AddPlayerToTeam(int playerId, int teamId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "teamId", teamId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/teams/{teamId}/players/{playerId}}", Method.PUT, urlParams) as RestResponse;

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static void AddPlayerToMatch(int playerId, int matchId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "matchId", matchId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/matches/{matchId}/players/{playerId}}", Method.PUT, urlParams) as RestResponse;

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static void RemovePlayerFromMatch(int playerId, int matchId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "matchId", matchId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/matches/{matchId}/players/{playerId}}", Method.DELETE, urlParams) as RestResponse;

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static void RemovePlayerFromTeam(int playerId, int teamId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "teamId", teamId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/teams/{teamId}/players/{playerId}}", Method.DELETE, urlParams) as RestResponse;

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public static List<LeagueModel> GetAllLeague()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/leagues", Method.GET) as RestResponse;
                return deserial.Deserialize<List<LeagueModel>>(response);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static List<StadiumModel> GetAllStadium()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/stadiums", Method.GET) as RestResponse;

                return deserial.Deserialize<List<StadiumModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public static int GetRoundsByLeague(int leagueId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/leagues/{id}/rounds", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<int>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static EventModel GetEventById(int eventId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", eventId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/events/{id}", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<EventModel>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static List<EventModel> GetEventsByMatch(int matchId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/matches/{id}/events", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<List<EventModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static List<EventMessageModel> GetEventMessagesByCategory(char category)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> queryParams = new Dictionary<string, string>() { { "categoryNumber", category.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/eventmessages", Method.GET, queryParams) as RestResponse;

                return deserial.Deserialize<List<EventMessageModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public static int AddLeague(string name, DateTime year, string type, string class1, int rounds)
        {
            try
            {
                LeagueModel model = new LeagueModel();
                model.ClassName = class1;
                model.Name = name;
                model.Rounds = rounds;
                model.type = type;
                model.Year = year;

                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/leagues", Method.POST, null, null, model) as RestResponse;

                return deserial.Deserialize<int>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static int AddPlayer(string name, int regNum, int number, DateTime year)
        {
            try
            {
                PlayerModel model = new PlayerModel();
                model.Name = name;
                model.Number = (short)number;
                model.RegNum = regNum;
                model.BirthDate = year;

                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/players", Method.POST, null, null, model) as RestResponse;

                return deserial.Deserialize<int>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static int AddMatch(int leagueId, int round, int homeTeamId, int awayTeamId, string time, int stadiumId)
        {
            try
            {
                MatchModel model = new MatchModel();
                model.LeagueId = leagueId;
                model.Round = (short) round;
                model.HomeTeamId = homeTeamId;
                model.AwayTeamId = awayTeamId;
                model.Date = time;
                model.StadiumId = stadiumId;

                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/matches", Method.POST, null, null, model) as RestResponse;

                return deserial.Deserialize<int>(response);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int AddReferee(string name)
        {
            try
            {
                RefereeModel model = new RefereeModel();
                model.Name = name;

                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/referees", Method.POST, null, null, model) as RestResponse;

                return deserial.Deserialize<int>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static int AddStadium(string name, string address)
        {
            try
            {
                StadiumModel model = new StadiumModel();
                model.Name = name;
                model.Address = address;

                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/stadiums", Method.POST, null, null, model) as RestResponse;

                return deserial.Deserialize<int>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static int AddTeam(string name, DateTime year, string coach, int stadiumId, int leagueId)
        {
            try
            {
                TeamModel model = new TeamModel();
                model.Name = name;
                model.Year = year;
                model.Coach = coach;
                model.StadiumId = stadiumId;
                model.LeagueId = leagueId;

                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                RestResponse response = client.ExecuteRequest("/api/floorball/teams", Method.POST, null, null, model) as RestResponse;

                return deserial.Deserialize<int>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static LeagueModel GetLeagueById(int leagueId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/leagues/{id}", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<LeagueModel>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static PlayerModel GetPlayerById(int playerId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", playerId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/players/{id}", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<PlayerModel>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static EventMessageModel GetEventMessageById(int eventMessageId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", eventMessageId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/eventmessages/{id}", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<EventMessageModel>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static int AddEvent(int matchId, string type, TimeSpan time, int playerId, int eventMessageId, int teamId)
        {
            try
            {
                EventModel eventModel = new EventModel();
                eventModel.MatchId = matchId;
                eventModel.TeamId = teamId;
                eventModel.Type = type;
                eventModel.Time = time.ToString(@"h\h\:m\m\:s\s", System.Globalization.CultureInfo.InvariantCulture);
                eventModel.EventMessageId = eventMessageId;
                eventModel.PlayerId = playerId;

                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> headers = new Dictionary<string, string>() { { "Content-Type", "application/json" } };
                RestResponse response = client.ExecuteRequest("/api/floorball/events", Method.POST, null, null, eventModel, headers) as RestResponse;

                return deserial.Deserialize<int>(response);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static void RemoveEvent(int eventId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", eventId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/events/{id}", Method.DELETE, urlParams) as RestResponse;

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static List<PlayerModel> GetPlayersByMatch(int matchId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } };
                RestResponse response = client.ExecuteRequest("/api/floorball/matches/{id}/players", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<List<PlayerModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

    }
}
