using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Components.Configurations;
using CSUI_Teams_Sync.Library;
using CSUI_Teams_Sync.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CSUI_Teams_Sync.Services
{
    public class SyncService
    {
        private readonly DbService _dbService;
        private readonly TeamsGraphAPIConfig _teamsGraphAPIConfig;
        private readonly string enterprisePath = "C:/Work/Enterprise/Team Sync";
        private readonly NLog.Logger _logger;
        public SyncService(IOptions<TeamsGraphAPIConfig> teamsGraphAPIConfig, LogManagerCustom logManagerCustom, DbService dbService)
        {
            _teamsGraphAPIConfig = teamsGraphAPIConfig.Value;
            _logger = logManagerCustom.logger;
            _dbService = dbService;
        }
        public async Task<string> GetTeamsService()
        {
            try
            {
                string accessToken = await TeamsGraphAPIHandler.GetAccessToken(_teamsGraphAPIConfig.TenantId);
                var teams = await TeamsGraphAPIHandler.GetTeams(accessToken);

                foreach (Team team in teams)
                {
                    _dbService.CreateTeam(team.id, team.displayName);

                    string teamPath = $"{enterprisePath}/{team.displayName}";
                    Directory.CreateDirectory(teamPath);

                    string filesPath = $"{teamPath}/Files & Folders";
                    Directory.CreateDirectory(filesPath);

                    var channels = await TeamsGraphAPIHandler.GetChannelsByTeamID(accessToken, team.id);

                    foreach (var channel in channels)
                    {
                        _dbService.CreateChannel(channel.id, channel.displayName, team.id);

                        // Sync Posts
                        //var posts = await TeamsGraphAPIHandler.GetPostsByTeamIDAndChannelID(accessToken, team.id, channel.id);

                        //foreach (var post in posts)
                        //{
                        //    int isMeeting = 0;

                        //    if(post.eventDetail != null && post.eventDetail.callEventType == "meeting")
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

                        // Sync Files And Folders
                        //var filesFolders = await TeamsGraphAPIHandler.GetFileFolderByTeamIDAndChannelID(accessToken, team.id, channel.id);
                        //var items = await TeamsGraphAPIHandler.GetItemsByDriveIDAndItemID(accessToken, filesFolders.parentReference.driveId, filesFolders.id);

                        //var channelPath = $"{filesPath}/{filesFolders.name}";
                        //Directory.CreateDirectory(channelPath);

                        //foreach (var item in items)
                        //{
                        //    if (item.folder != null)
                        //    {
                        //        await SyncItemsInFolder.SyncItemsInFolderAsync(channelPath, item, accessToken);
                        //    }
                        //    else
                        //    {
                        //        await DownloadFile.DownloadFileAsync(channelPath, item.downloadUrl, item.name);
                        //    }
                        //}

                        var users = await TeamsGraphAPIHandler.GetUsers(accessToken);

                        foreach(var user in users.Value)
                        {
                            var chats = await TeamsGraphAPIHandler.GetUserChats(accessToken, user.Id);

                            foreach (var chat in chats)
                            {
                                _dbService.CreateChat(chat.Id, user.Id, chat.Topic, chat.ChatType, JsonConvert.SerializeObject(chat));

                                var messages = await TeamsGraphAPIHandler.GetChatMessages(accessToken, user.Id, chat.Id);

                                foreach (var message in messages)
                                {
                                    _dbService.CreateMessage(message.Id, user.Id, chat.Id, JsonConvert.SerializeObject(message));
                                }
                            }
                        }
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
