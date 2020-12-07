using IDP_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository
{
    public interface IUserRepository
    {
        //CREATE
        User RegisterUser(LoginInputModel model);
        //READ
        User GetUserById(int id);
        User GetUserByUserName(string name);
        List<string> GatAllUserNames(string name);
        //UPDATE
        User UpdateUser(User userUpdate);
        //DELETE
        User DeleteUser(int id);

        //Hashing
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        string GenerateToken(User user);
    }
}
