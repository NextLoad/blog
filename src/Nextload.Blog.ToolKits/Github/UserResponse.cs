using System;
using System.Collections.Generic;
using System.Text;

namespace Nextload.Blog.ToolKits.Github
{

    public class UserResponse
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string Node_id { get; set; }
        public string Avatar_url { get; set; }
        public string Gravatar_id { get; set; }
        public string Url { get; set; }
        public string Html_url { get; set; }
        public string Followers_url { get; set; }
        public string Following_url { get; set; }
        public string Gists_url { get; set; }
        public string Starred_url { get; set; }
        public string Subscriptions_url { get; set; }
        public string Organizations_url { get; set; }
        public string Repos_url { get; set; }
        public string Events_url { get; set; }
        public string Received_events_url { get; set; }
        public string Type { get; set; }
        public bool Site_admin { get; set; }
        public object Name { get; set; }
        public object Company { get; set; }
        public string Blog { get; set; }
        public object Location { get; set; }
        public object Email { get; set; }
        public object Hireable { get; set; }
        public object Bio { get; set; }
        public object Twitter_username { get; set; }
        public int Public_repos { get; set; }
        public int Public_gists { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int Private_gists { get; set; }
        public int Total_private_repos { get; set; }
        public int Owned_private_repos { get; set; }
        public int Disk_usage { get; set; }
        public int Collaborators { get; set; }
        public bool Two_factor_authentication { get; set; }
        public Plan Plan { get; set; }
    }

    public class Plan
    {
        public string Name { get; set; }
        public int Space { get; set; }
        public int Collaborators { get; set; }
        public int Private_repos { get; set; }
    }


}
