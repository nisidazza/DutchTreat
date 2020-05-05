using DutchTreat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public interface IUserRepository
    {
        Task<StoreUser> GetUserByName(string userName);
    }
}
