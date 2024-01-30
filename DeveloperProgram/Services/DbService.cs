using CSUI_Teams_Sync.Models;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Graph.Models;
using System.Data.SqlClient;

namespace CSUI_Teams_Sync.Services
{
    public class DbService
    {
        readonly string connectionString = "Data Source=localhost;Initial Catalog=OTCS;User Id=sa;Password=P@ssw0rd;";
        readonly string channelItemsTable = "TeamsBackup_ChannelItems";
        readonly string itemsTable = "TeamsBackup_Items";
        readonly string teamsTable = "TeamsBackup_Teams";
        readonly string postsTable = "TeamsBackup_Posts";
        readonly string deltaLinksTable = "TeamsBackup_DeltaLinks";
        readonly string configurationsTable = "TeamsBackup_Configurations";
        readonly string channelConfigurationsTable = "TeamsBackup_ChannelConfigurations";
        readonly string channelsTable = "TeamsBackup_Channels";
        readonly string messagesTable = "TeamsBackup_Messages";
        readonly string chatsTable = "TeamsBackup_Chats";
        readonly string postRepliesTable = "TeamsBackup_PostReplies";
        public void CreateTeam(string ID, string name)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = $"INSERT INTO {teamsTable} (ID, Name) VALUES (@ID, @Name)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Name", name);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Team {name} Already Exist, Skipping...");
            }
        }
        public void CreateChannelItem(string channelID, string itemID)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = $"INSERT INTO {channelItemsTable} (ChannelID, ItemID) VALUES (@ChannelID, @ItemID)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ChannelID", channelID);
                command.Parameters.AddWithValue("@ItemID", itemID);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Channels {channelID} Already Exist, Skipping...");
            }
        }
        public void CreateDeltaLink(string link)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = $"INSERT INTO {deltaLinksTable} (Link) VALUES (@Link)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Link", link);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CreateItem(long nodeID, string name, string driveID)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = $"INSERT INTO {itemsTable} (DriveID, NodeID, Name) VALUES (@DriveID, @NodeID, @Name)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@DriveID", driveID);
                command.Parameters.AddWithValue("@NodeID", nodeID);
                command.Parameters.AddWithValue("@Name", name);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Item {name} Already Exist, Skipping...");
            }
        }
        public void CreateChannel(string ID, string name, string teamID)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = $"INSERT INTO {channelsTable} (ID, Name, TeamID) VALUES (@ID, @Name, @TeamID)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@TeamID", teamID);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Channel {name} Already Exist, Skipping...");
            }
        }
        public void CreatePost(string ID, string teamID, string channelID, int isMeeting, string data)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = $"INSERT INTO {postsTable} (ID, TeamID, ChannelID, IsMeeting, Data) VALUES (@ID, @TeamID, @ChannelID, @IsMeeting, @Data)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@TeamID", teamID);
                command.Parameters.AddWithValue("@IsMeeting", isMeeting);
                command.Parameters.AddWithValue("@ChannelID", channelID);
                command.Parameters.AddWithValue("@Data", data);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Post {ID} Already Exist, Skipping...");
            }
        }
        public void CreatePostReply(string ID, string PostID, string data)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = $"INSERT INTO {postRepliesTable} (ID, PostID, Data) VALUES (@ID, @PostID, @Data)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@PostID", PostID);
                command.Parameters.AddWithValue("@Data", data);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Post {ID} Already Exist, Skipping...");
            }
        }
        public void CreateChat(string ID, string userID, string? topic, string chatType, string data)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = $"INSERT INTO {chatsTable} (ID, UserID, Topic, ChatType, Data) VALUES (@ID, @UserID, @Topic, @ChatType, @Data)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Topic", topic ?? "");
                command.Parameters.AddWithValue("@ChatType", chatType);
                command.Parameters.AddWithValue("@Data", data);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Chat {ID} Already Exist, Skipping...");
            }
        }
        public void CreateMessage(string ID, string userID, string chatID, string data)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = $"INSERT INTO {messagesTable} (ID, UserID, ChatID, Data) VALUES (@ID, @UserID, @ChatID, @Data)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@ChatID", chatID);
                command.Parameters.AddWithValue("@Data", data);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Message {ID} Already Exist, Skipping...");
            }
        }
        public List<TeamsJoinChannel> GetTeamsJoinChannel()
        {
            using SqlConnection connection = new(connectionString);
            List<TeamsJoinChannel> result = new();

            try
            {
                connection.Open();

                string query = $"SELECT tt.ID \"TeamID\", tt.Name \"TeamName\", tc.Name \"ChannelName\", tc.ID \"ChannelID\" FROM {teamsTable} tt\r\nLEFT JOIN {channelsTable} tc\r\nON tc.TeamID = tt.ID";

                using SqlCommand command = new(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TeamsJoinChannelChannel channel = new()
                    {
                        id = reader["ChannelID"].ToString(),
                        name = reader["ChannelName"].ToString(),
                        items = new()
                    };

                    TeamsJoinChannel data = new()
                    {
                        channel = channel,
                        id = reader["TeamID"].ToString(),
                        name = reader["TeamName"].ToString()
                    };

                    result.Add(data);
                }

                foreach(var res in result)
                {
                    var config = GetChannelConfiguration(res.channel.id);

                    foreach(var conf in config)
                    {
                        TeamsJoinChannelChannelItems channelConfig = new()
                        {
                            value = conf.ID.ToString(),
                            label = conf.Name
                        };
                        res.channel.items.Add(channelConfig);
                    }

                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public List<ChannelConfiguration> GetChannelConfiguration(string channelID)
        {
            using SqlConnection connection = new(connectionString);
            List<ChannelConfiguration> result = new();

            try
            {
                connection.Open();

                string query = $"SELECT tc.Name, tc.ID FROM {channelConfigurationsTable} tcc\r\nLEFT JOIN {configurationsTable} tc\r\nON tc.ID = tcc.ConfigurationID\r\nWHERE ChannelID = '{channelID}'";

                using SqlCommand command = new(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ChannelConfiguration row = new()
                    {
                        ID = (int)reader["ID"],
                        Name = reader["Name"].ToString()
                    };
                    result.Add(row);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public string GetRecentDeltaLink()
        {
            using SqlConnection connection = new(connectionString);
            string result = "";

            try
            {
                connection.Open();

                string query = $"SELECT TOP 1 Link FROM {deltaLinksTable} ORDER BY ID DESC";

                using SqlCommand command = new(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = reader["Link"].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public List<string> GetChannelConfigurations(List<string> names)
        {
            using SqlConnection connection = new(connectionString);
            List<string> result = new();

            try
            {
                connection.Open();

                string[] parameterNames = names.Select((config, index) => $"@config{index}").ToArray();

                string query = $"SELECT ID FROM {configurationsTable} WHERE Name IN ({string.Join(", ", parameterNames)})";

                using SqlCommand command = new(query, connection);

                for (int i = 0; i < names.Count; i++)
                {
                    command.Parameters.AddWithValue($"@config{i}", names[i]);
                }

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(reader["ID"].ToString());
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public long GetItemNodeIDByDriveID(string ID)
        {
            using SqlConnection connection = new(connectionString);
            long result = 0;

            try
            {
                connection.Open();

                string query = $"SELECT TOP 1 NodeID FROM {itemsTable} WHERE DriveID = '{ID}'";

                using SqlCommand command = new(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = long.Parse(reader["NodeID"].ToString());
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public string GetChannelByItemID(string itemID)
        {
            using SqlConnection connection = new(connectionString);
            string result = "";

            try
            {
                connection.Open();

                string query = $"SELECT TOP 1 ChannelID FROM {channelItemsTable} WHERE ItemID = '{itemID}'";

                using SqlCommand command = new(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = reader["ChannelID"].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public void DeleteChannelConfigurationByChannelID(string channelID)
        {
            using SqlConnection connection = new(connectionString);

            try
            {
                connection.Open();

                string query = $"DELETE FROM {channelConfigurationsTable} WHERE ChannelID = '{channelID}'";

                using SqlCommand command = new(query, connection);

                using SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CreateChannelConfiguration(string channelID, int ID)
        {
            using SqlConnection connection = new(connectionString);

            try
            {
                connection.Open();

                string query = $"INSERT INTO {channelConfigurationsTable} (ChannelID, ConfigurationID) VALUES ('{channelID}', {ID})";

                using SqlCommand command = new(query, connection);

                using SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
