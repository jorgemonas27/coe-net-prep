using Backend.API.Models;

namespace Backend.API.Services
{
    // this interface was created in order to get users from different sources, json files, dbs, etc.
    public interface IGetUsersInformation
    {
        List<UserInformation> GetAllUsersInformation();
    }
}
