
using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Models;
using HandlebarsDotNet;

namespace CSUI_Teams_Sync.Services
{
    public class HTMLService
    {
        public string GeneratePost(Post post, List<Post> replies)
        {
            string repliesSection = "";
            string source =
            @"<div key=""{{PostID}}"" style=""padding: 1rem; margin: auto; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); display: flex; flex-direction: column; gap: 1rem; width: 80%; background-color: white;"">
                <div style=""display: flex; align-items: center; gap: 0.5rem; padding: 0.25rem;"">
                  <div style=""width: 2px; height: 30px; background-color: #6366f1; border-radius: 4px;""></div>
                  <div style=""display: flex; align-items: center; justify-content: center; width: 30px; height: 30px; text-align: center; color: white; border-radius: 50%; background-color: #6366f1;"">
                    {{UserName}}
                  </div>
                  <div>{{UserName}}</div>
                  <div style=""font-size: 0.875rem; color: #718096;"">{{Created}}</div>
                </div>
                <div style=""flex: 1; display: flex; flex-direction: column; gap: 1rem; padding: 0.5rem;"">
                  <div style=""font-size: 1rem;"">{{BodyContent}}</div>
                </div>
                {{Replies}}
              </div>";

            var template = Handlebars.Compile(source);

            if(replies.Count > 0)
            {
                repliesSection  += @"<div style=""flex-direction: column; gap: 0.5rem;""><div style=""border-bottom: 1px solid #e2e8f0;""></div><div>";

                foreach(var reply in replies)
                {
                    if(reply.body.content != "<systemEventMessage/>" && reply.subject != null)
                    {
                        repliesSection += $@"<div key=""{reply.id}"" style=""padding: 0.5rem;""><div style=""display: flex; align-items: center; gap: 0.5rem;""><div style=""width: 2px; height: 20px; background-color: #6366f1; border-radius: 4px;""></div><div style=""display: flex; align-items: center; justify-content: center; width: 20px; height: 20px; font-size: 0.75rem; color: white; border-radius: 50%; background-color: #6366f1;"">{reply.from.user.displayName}</div><div style=""font-size: 0.875rem;"">";
                    }
                }
            }

            var data = new{
                PostID = post.id,
                Created = TimeUtils.FormatTeamsDate(post.createdDateTime),
                UserName = post.from?.user?.displayName,
                BodyContent = post.body.content,
                Replies = repliesSection
            };

            var result = template(data);

            return result;
        }
    }
}
