using System.Data.SqlClient;

namespace CSUI_Teams_Sync.Services
{
    public class DbService
    {
        readonly string connectionString = "Data Source=localhost;Initial Catalog=teams_backup;User Id=sa;Password=P@ssw0rd;";
        public void CreateTeam(string ID, string name)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO teams (ID, Name) VALUES (@ID, @Name)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Name", name);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Team {name} Already Exist, Skipping...");
            }
        }
        public void CreateChannel(string ID, string name, string teamID)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO channels (ID, Name, TeamID) VALUES (@ID, @Name, @TeamID)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@TeamID", teamID);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Channel {name} Already Exist, Skipping...");
            }
        }
        public void CreatePost(string ID, string teamID, string channelID, int isMeeting, string data)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO posts (ID, TeamID, ChannelID, IsMeeting, Data) VALUES (@ID, @TeamID, @ChannelID, @IsMeeting, @Data)";

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
                Console.WriteLine($"Post {ID} Already Exist, Skipping...");
            }
        }
        public void CreatePostReply(string ID, string PostID, string data)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO post_replies (ID, PostID, Data) VALUES (@ID, @PostID, @Data)";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@PostID", PostID);
                command.Parameters.AddWithValue("@Data", data);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Post {ID} Already Exist, Skipping...");
            }
        }
    }
}
