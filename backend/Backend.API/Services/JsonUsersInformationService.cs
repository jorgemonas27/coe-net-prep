﻿using Backend.API.Models;
using Newtonsoft.Json;

namespace Backend.API.Services
{
    public class JsonUsersInformationService : IUsersInformation
    {
        public List<UserInformation> GetAllUsersInformation()
        {
            var json = File.ReadAllText("usersdb.json");
            return JsonConvert.DeserializeObject<List<UserInformation>>(json)!;
        }
    }
}