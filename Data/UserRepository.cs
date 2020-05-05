using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<StoreUser> _userManager;

        public UserRepository(UserManager<StoreUser> userManager)
        {
            _userManager = userManager;
        }
        public Task<StoreUser> GetUserByName(string userName)
        {
            return _userManager.FindByNameAsync(userName);
        }
    }
}
