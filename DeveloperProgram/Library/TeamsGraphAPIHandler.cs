using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Components.Configurations;
using CSUI_Teams_Sync.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace CSUI_Teams_Sync.Library
{
    public class TeamsGraphAPIHandler
    {
        private readonly TeamsGraphAPIConfig _teamsGraphAPIConfig;
        public readonly NLog.Logger _logger;
        public TeamsGraphAPIHandler(IOptions<TeamsGraphAPIConfig> teamsGraphAPIConfig, LogManagerCustom logManagerCustom)
        {
            _teamsGraphAPIConfig = teamsGraphAPIConfig.Value;
            _logger = logManagerCustom.logger;
        }
        public static async Task<string> GetAccessToken(string tenantId)
        {
            string accessToken = "";
            try
            {
                var apiUrl = $"https://login.microsoftonline.com/";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest($"{tenantId}/oauth2/v2.0/token", Method.Post);

                var contentType = "application/x-www-form-urlencoded";
                request.AddHeader("Content-Type", contentType);
                request.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);
                request.AddParameter("client_id", "a1f4d546-0ff8-474c-b672-dbc626743eae", ParameterType.GetOrPost);
                request.AddParameter("scope", "https://graph.microsoft.com/.default", ParameterType.GetOrPost);
                request.AddParameter("client_secret", "qsr8Q~fN0ZUsnHQ2aMva_bpq0A6eTahcvArJKc38", ParameterType.GetOrPost);

                var response = await restClient.ExecutePostAsync<TeamsGraphToken>(request); // Use ExecuteAsync for asynchronous operation

                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    var token = JsonConvert.DeserializeObject<TeamsGraphToken>(response.Content);
                    accessToken = token?.access_token;
                }
                return accessToken;

            }
            catch (Exception ex)
            {
                /*   _logger.Error($"TeamsGraphAPIHandler.cs | GetAccessToken | error {ex.Message}");
                   _logger.Trace(ex.StackTrace);*/
                throw ex;
            }
        }
        public static async Task<TeamsUser> GetUsers(string accessToken)
        {
            TeamsUser users = new TeamsUser();
            try
            {
                var apiUrl = $"https://graph.microsoft.com/v1.0/users";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<TeamsUser>(request); // Use ExecuteAsync for asynchronous operation
                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    users = JsonConvert.DeserializeObject<TeamsUser>(response.Content);
                }
                return users;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<User> GetUserByDisplayName(string accessToken, string displayName)
        {
            TeamsUser users = new TeamsUser();
            User user = new User();
            try
            {
                var apiUrl = $"https://graph.microsoft.com/v1.0/users";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<TeamsUser>(request); // Use ExecuteAsync for asynchronous operation
                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    users = JsonConvert.DeserializeObject<TeamsUser>(response.Content);
                    user = users.Value.Find(x => x.DisplayName == displayName);
                }
                return user;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> GetUserIdByDisplayName(string accessToken, string displayName)
        {
            TeamsUser users = new TeamsUser();
            string userId = "";
            try
            {
                var apiUrl = $"https://graph.microsoft.com/v1.0/users";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<TeamsUser>(request); // Use ExecuteAsync for asynchronous operation
                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    users = JsonConvert.DeserializeObject<TeamsUser>(response.Content);
                    userId = users.Value.Find(x => x.DisplayName == displayName).Id;
                }
                return userId;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task<List<Chat>> GetUserChats(string accessToken, string userId)
        {
            try
            {
                TeamsUserChat teamsUserChats = new TeamsUserChat();
                var userChats = new List<Chat>();
                var apiUrl = $"https://graph.microsoft.com/v1.0/users/{userId}/chats";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<TeamsUserChat>(request); // Use ExecuteAsync for asynchronous operation
                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    teamsUserChats = JsonConvert.DeserializeObject<TeamsUserChat>(response.Content);
                    userChats = teamsUserChats.Value;
                }
                return userChats;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<Team>> GetTeams(string accessToken)
        {
            try
            {
                Teams teams = new();
                List<Team> result = new();
                var apiUrl = "https://graph.microsoft.com/v1.0/teams";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<Teams>(request);
                
                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    teams = JsonConvert.DeserializeObject<Teams>(response.Content);
                    result = teams.Value;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<Channel>> GetChannelsByTeamID(string accessToken, string channelID)
        {
            try
            {
                Channels channels = new();
                List<Channel> result = new();
                var apiUrl = $"https://graph.microsoft.com/v1.0/teams/{channelID}/allChannels";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<Channels>(request);

                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    channels = JsonConvert.DeserializeObject<Channels>(response.Content);
                    result = channels.Value;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<FilesFolders> GetFileFolderByTeamIDAndChannelID(string accessToken, string teamID, string channelID)
        {
            try
            {
                FilesFolders filesFolders = new();
                var apiUrl = $"https://graph.microsoft.com/v1.0/teams/{teamID}/channels/{channelID}/filesFolder";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<FilesFolders>(request);

                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    filesFolders = JsonConvert.DeserializeObject<FilesFolders>(response.Content);
                }

                return filesFolders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<Item>> GetItemsByDriveIDAndItemID(string accessToken, string driveID, string itemID)
        {
            try
            {
                Items items = new();
                List<Item> result = new();
                var apiUrl = $"https://graph.microsoft.com/v1.0/drives/{driveID}/items/{itemID}/children";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<Items>(request);

                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    items = JsonConvert.DeserializeObject<Items>(response.Content);
                    result = items.Value;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<Post>> GetPostsByTeamIDAndChannelID(string accessToken, string teamID, string channelID)
        {
            try
            {
                Posts items = new();
                List<Post> result = new();
                var apiUrl = $"https://graph.microsoft.com/v1.0/teams/{teamID}/channels/{channelID}/messages";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<Posts>(request);

                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    items = JsonConvert.DeserializeObject<Posts>(response.Content);
                    result = items.value;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<Post>> GetPostRepliesByTeamIDAndChannelIDAndMessageID(string accessToken, string teamID, string channelID, string messageID)
        {
            try
            {
                Posts items = new();
                List<Post> result = new();
                var apiUrl = $"https://graph.microsoft.com/v1.0/teams/{teamID}/channels/{channelID}/messages/{messageID}/replies";
                var restClient = new RestClient(apiUrl);

                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = await restClient.ExecuteAsync<Posts>(request);

                var isSuccessfull = response.IsSuccessful;
                if (isSuccessfull)
                {
                    items = JsonConvert.DeserializeObject<Posts>(response.Content);
                    result = items.value;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
