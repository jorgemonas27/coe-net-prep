using Backend.API.Models;
using Newtonsoft.Json;

namespace Backend.API.Services
{
    public class JsonUsersInformationService : IUsersInformation
    {
        public List<UserInformation> Users { get; } = new List<UserInformation>();

        public JsonUsersInformationService()
        {
            var json = File.ReadAllText("usersdb.json");
            Users = JsonConvert.DeserializeObject<List<UserInformation>>(json) ?? new List<UserInformation>();
        }
    }
}
