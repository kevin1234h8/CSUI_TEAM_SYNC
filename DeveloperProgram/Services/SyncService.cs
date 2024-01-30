using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Components.Configurations;
using CSUI_Teams_Sync.Library;
using CSUI_Teams_Sync.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection.Metadata.Ecma335;

namespace CSUI_Teams_Sync.Services
{
    public class SyncService
    {
        private readonly HTMLService _htmlService;
        private readonly DbService _dbService;
        private readonly OTCSService _otcsService;
        private readonly SubscriptionService _subscriptionService;
        private readonly TeamsGraphAPIConfig _teamsGraphAPIConfig;
        private readonly NLog.Logger _logger;
        public SyncService(IOptions<TeamsGraphAPIConfig> teamsGraphAPIConfig, LogManagerCustom logManagerCustom, DbService dbService, OTCSService otcsService, SubscriptionService subscriptionService, HTMLService htmlService)
        {
            _teamsGraphAPIConfig = teamsGraphAPIConfig.Value;
            _logger = logManagerCustom.logger;
            _dbService = dbService;
            _otcsService = otcsService;
            _subscriptionService = subscriptionService;
            _htmlService = htmlService;
        }
        public async Task<string> GetTeamsService()
        {
            try
            {
                //return _htmlService.GeneratePosts();
                string accessToken = await TeamsGraphAPIHandler.GetAccessToken(_teamsGraphAPIConfig.TenantId);

                //await _subscriptionService.DeleteAllSubscriptions(accessToken);

                //await _subscriptionService.CreateDriveSubscription(accessToken);

                //string deltaLink = await TeamsGraphAPIHandler.GetDriveDelta(accessToken);

                //_dbService.CreateDeltaLink(deltaLink);

                //return "Done";
                var ticket = await _otcsService.GetTicket();

                var teams = await TeamsGraphAPIHandler.GetTeams(accessToken);

                foreach (Team team in teams)
                {
                    _dbService.CreateTeam(team.id, team.displayName);

                    var body = new OTCSCreateNode()
                    {
                        parent_id = 29002,
                        name = team.displayName,
                        type = 0
                    };

                    var teamFolder = await _otcsService.CreateFolder(ticket, body);

                    var channels = await TeamsGraphAPIHandler.GetChannelsByTeamID(accessToken, team.id);

                    foreach (var channel in channels)
                    {
                        _dbService.CreateChannel(channel.id, channel.displayName, team.id);

                        // Sync Files And Folders
                        var filesFolders = await TeamsGraphAPIHandler.GetFileFolderByTeamIDAndChannelID(accessToken, team.id, channel.id);
                        var items = await TeamsGraphAPIHandler.GetItemsByDriveIDAndItemID(accessToken, filesFolders.parentReference.driveId, filesFolders.id);

                        var html = "";
                        var posts = await TeamsGraphAPIHandler.GetPostsByTeamIDAndChannelID(accessToken, team.id, channel.id);

                        foreach (var post in posts)
                        {
                            var postReplies = await TeamsGraphAPIHandler.GetPostRepliesByTeamIDAndChannelIDAndMessageID(accessToken, team.id, channel.id, post.id);
                            var postCard = _htmlService.GeneratePost(post, postReplies);
                            html += postCard;
                        }

                        Console.WriteLine(html);

                        string filePath = $@"C:\work\temp_upload\{channel.id}.html";
                        File.WriteAllText(filePath, html);

                        // Create Post HTML File
                        var client = new RestClient("http://localhost");
                        var request = new RestRequest("/otcs/cs.exe/api/v1/nodes", Method.Post);

                        request.AddHeader("otcsticket", ticket);
                        request.AddParameter("type", 144);
                        request.AddParameter("parent_id", _dbService.GetItemNodeIDByDriveID(channel.id));
                        request.AddParameter("name", "backup.html");
                        request.AddFile("file", filePath);
                        RestResponse response = await client.ExecuteAsync(request);

                        continue;
                        //_dbService.CreateChannelItem(channel.id, filesFolders.id);


                        // Create Post and Files Folders
                        //var bodyChannel = new OTCSCreateNode()
                        //{
                        //    parent_id = teamFolder.results.data.properties.id,
                        //    name = channel.displayName,
                        //    type = 0
                        //};

                        //var teamChannel = await _otcsService.CreateFolder(ticket, bodyChannel);
                        
                        //// Create Files Folder
                        //var bodyChannelFiles = new OTCSCreateNode()
                        //{
                        //    parent_id = teamChannel.results.data.properties.id,
                        //    name = "Files",
                        //    type = 0
                        //};

                        //var teamChannelFiles = await _otcsService.CreateFolder(ticket, bodyChannelFiles);
                        //if (teamChannelFiles != null)
                        //{
                        //    _dbService.CreateItem(teamChannelFiles.results.data.properties.id, "Files", filesFolders.id);
                        //}
                        
                        //// Create Posts Folder
                        //var bodyChannelPosts = new OTCSCreateNode()
                        //{
                        //    parent_id = teamChannel.results.data.properties.id,
                        //    name = "Posts",
                        //    type = 0
                        //};

                        //var teamChannelPosts = await _otcsService.CreateFolder(ticket, bodyChannelPosts);
                        //if (teamChannelPosts != null)
                        //{
                        //    _dbService.CreateItem(teamChannelPosts.results.data.properties.id, "Posts", channel.id);
                        //}

                        //continue;

                        //foreach (var item in items)
                        //{
                        //    if (item.folder != null)
                        //    {
                        //        await SyncItemsInFolder.SyncItemsInFolderAsync(teamChannelFiles.results.data.properties.id, item, accessToken, ticket);
                        //    }
                        //    else
                        //    {
                        //        var download = await DownloadFile.DownloadFileAsync(item.downloadUrl, teamChannelFiles.results.data.properties.id, item.name, ticket);
                        //        if (download != null)
                        //        {
                        //            _dbService.CreateItem(download.id, item.name, item.id);
                        //        }
                        //    }
                        //}

                        //// Sync Posts
                        //var posts = await TeamsGraphAPIHandler.GetPostsByTeamIDAndChannelID(accessToken, team.id, channel.id);

                        //foreach (var post in posts)
                        //{
                        //    int isMeeting = 0;

                        //    if (post.eventDetail != null && post.eventDetail.callEventType == "meeting")
                        //    {
                        //        isMeeting = 1;
                        //    }

                        //    _dbService.CreatePost(post.id, post.channelIdentity.teamId, post.channelIdentity.channelId, isMeeting, JsonConvert.SerializeObject(post));

                        //    var postReplies = await TeamsGraphAPIHandler.GetPostRepliesByTeamIDAndChannelIDAndMessageID(accessToken, post.channelIdentity.teamId, post.channelIdentity.channelId, post.id);

                        //    foreach (var postReply in postReplies)
                        //    {
                        //        _dbService.CreatePostReply(postReply.id, post.id, JsonConvert.SerializeObject(postReply));
                        //    }
                        //}
                    }
                }

                return "Job Done";
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
