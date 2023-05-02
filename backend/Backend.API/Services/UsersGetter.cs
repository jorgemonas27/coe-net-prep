using Backend.API.Models;

namespace Backend.API.Services
{
    public class UsersGetter
    {
        public IUsersInformation users;

        public UsersGetter(IUsersInformation users)
        {
            this.users = users;
        }

        public List<UserInformation> GetUsers() => users.GetAllUsersInformation();
    }
}
