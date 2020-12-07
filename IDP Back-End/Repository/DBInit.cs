using IDP_Back_End.Models;
using IDP_Back_End.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDP_Back_End.Repository
{
  public class DBInit
  {
    public static void SeedDB(DBContext ctx)
    {
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      #region hashing a password
      //Hashing the password "1234"
      //Will have to be changed Terrible password
      string password = "aztamindenit1";
      byte[] passwordHashAdmin, passwordSaltAdmin, passwordHashUser, passwordSaltUser;
      CreatePasswordHash(password, out passwordHashAdmin, out passwordSaltAdmin);
      CreatePasswordHash(password, out passwordHashUser, out passwordSaltUser);
            #endregion

            #region Adding Test Users for Dev

            var userAdmin = ctx.Users.Add(new User()
            {
                UserName = "admin",
                PasswordHash = passwordHashAdmin,
                PasswordSalt = passwordSaltAdmin,
                Admin = true
            }).Entity;

      #endregion

      ctx.SaveChanges();
    }
    #region Methods needed
    //Hashing method
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }
    #endregion
  }
}
