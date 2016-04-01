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
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            RestResponse response = client.ExecuteRequest("/api/floorball/players", Method.GET) as RestResponse;

            return deserial.Deserialize<List<PlayerModel>>(response);
        }

        public static List<TeamModel> GetTeamsByLeague(int leagueId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { {"id",leagueId.ToString()} };
            RestResponse response = client.ExecuteRequest("/api/floorball/leagues/{id}/teams", Method.GET,urlParams) as RestResponse;

            return deserial.Deserialize<List<TeamModel>>(response);
        }

        public static TeamModel GetTeamById(int teamId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", teamId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/teams/{id}", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<TeamModel>(response);
        }

        public static List<MatchModel> GetMatchesByLeague(int leagueId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/leagues/{id}/matches", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<List<MatchModel>>(response);
        }

        public static MatchModel GetMatchById(int matchId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/matches/{id}", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<MatchModel>(response);
        }

        public static List<RefereeModel> GetAllReferee()
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            RestResponse response = client.ExecuteRequest("/api/floorball/referees", Method.GET) as RestResponse;

            return deserial.Deserialize<List<RefereeModel>>(response);
        }

        public static RefereeModel GetRefereeById(int refereeId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", refereeId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/referee/{id}", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<RefereeModel>(response);
        }

        public static List<PlayerModel> GetPlayersByTeam(int teamId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", teamId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/teams/{id}/players", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<List<PlayerModel>>(response);
        }

        public static List<MatchModel> GetMatchesByReferee(int refereeId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", refereeId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/referee/{id}/matches", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<List<MatchModel>>(response);
        }

        public static void AddPlayerToTeam(int playerId, int teamId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "teamId",teamId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/teams/{teamId}/players/{playerId}}", Method.PUT, urlParams) as RestResponse;

        }

        public static void AddPlayerToMatch(int playerId, int matchId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "matchId", matchId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/matches/{matchId}/players/{playerId}}", Method.PUT, urlParams) as RestResponse;

        }

        public static void RemovePlayerFromMatch(int playerId, int matchId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "matchId", matchId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/matches/{matchId}/players/{playerId}}", Method.DELETE, urlParams) as RestResponse;

        }

        public static void RemovePlayerFromTeam(int playerId, int teamId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "teamId", teamId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/teams/{teamId}/players/{playerId}}", Method.DELETE, urlParams) as RestResponse;

        }

        public static List<LeagueModel> GetAllLeague()
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            RestResponse response = client.ExecuteRequest("/api/floorball/leagues", Method.GET) as RestResponse;

            return deserial.Deserialize<List<LeagueModel>>(response);
        }

        public static List<StadiumModel> GetAllStadium()
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            RestResponse response = client.ExecuteRequest("/api/floorball/stadiums", Method.GET) as RestResponse;

            return deserial.Deserialize<List<StadiumModel>>(response);
        }

        public static int GetRoundsByLeague(int leagueId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() }};
            RestResponse response = client.ExecuteRequest("/api/floorball/leagues/{id}/rounds", Method.GET,urlParams) as RestResponse;

            return deserial.Deserialize<int>(response);
        }

        public static EventModel GetEventById(int eventId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", eventId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/events/{id}", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<EventModel>(response);
        }

        public static List<EventModel> GetEventsByMatch(int matchId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/matches/{id}/events", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<List<EventModel>>(response);
        }

        public static List<EventMessageModel> GetEventMessagesByCategory(char category)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> queryParams = new Dictionary<string, string>() { { "categoryNumber", category.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/eventmessages", Method.GET,queryParams) as RestResponse;

            return deserial.Deserialize<List<EventMessageModel>>(response);
        }

        public static int AddLeague(string name, DateTime year, string type, string class1, int rounds)
        {
            LeagueModel model = new LeagueModel();
            model.ClassName = class1;
            model.Name = name;
            model.Rounds = rounds;
            model.type = type;
            model.Year = year;

            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            RestResponse response = client.ExecuteRequest("/api/floorball/leagues", Method.POST, null, null,model) as RestResponse;

            return deserial.Deserialize<int>(response);
        }

        public static int AddPlayer(string name, int regNum, int number, DateTime year)
        {
            PlayerModel model = new PlayerModel();
            model.Name = name;
            model.Number = (short) number;
            model.RegNum = regNum;
            model.BirthDate = year;

            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            RestResponse response = client.ExecuteRequest("/api/floorball/matches", Method.POST, null, null, model) as RestResponse;

            return deserial.Deserialize<int>(response);
        }

        public static int AddReferee(string name)
        {
            RefereeModel model = new RefereeModel();
            model.Name = name;

            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            RestResponse response = client.ExecuteRequest("/api/floorball/referees", Method.POST, null, null, model) as RestResponse;

            return deserial.Deserialize<int>(response);
        }

        public static int AddStadium(string name, string address)
        {
            StadiumModel model = new StadiumModel();
            model.Name = name;
            model.Address = address;

            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            RestResponse response = client.ExecuteRequest("/api/floorball/stadiums", Method.POST, null, null, model) as RestResponse;

            return deserial.Deserialize<int>(response);
        }

        public static int AddTeam(string name, DateTime year, string coach, int stadiumId, int leagueId)
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

        public static LeagueModel GetLeagueById(int leagueId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/leagues/{id}", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<LeagueModel>(response);
        }

        public static PlayerModel GetPlayerById(int playerId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", playerId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/players/{id}", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<PlayerModel>(response);
        }

        public static EventMessageModel GetEventMessageById(int eventMessageId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", eventMessageId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/eventmessages/{id}", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<EventMessageModel>(response);
        }

        public static int AddEvent(int matchId, string type, TimeSpan time, int playerId, int eventMessageId, int teamId)
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
            RestResponse response = client.ExecuteRequest("/api/floorball/events", Method.POST,null,null,eventModel,headers) as RestResponse;

            return deserial.Deserialize<int>(response);
        }

        public static void RemoveEvent(int eventId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", eventId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/events/{id}", Method.DELETE, urlParams) as RestResponse;

        }

        public static List<PlayerModel> GetPlayersByMatch(int matchId)
        {
            FloorballRESTClient client = new FloorballRESTClient(Settings.Default.ServerURL);
            Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } };
            RestResponse response = client.ExecuteRequest("/api/floorball/matches/{id}/players", Method.GET, urlParams) as RestResponse;

            return deserial.Deserialize<List<PlayerModel>>(response);
        }

    }
}
