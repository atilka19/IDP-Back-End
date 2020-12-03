using IDP_Back_End.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _ctx;

        public UserRepository(DBContext ctx)
        {
            _ctx = ctx;
        }
        public User RegisterUser(LoginInputModel model)
        {
            if (model.Password == null || model.Password.Length < 6 || model.Password.Length > 20)
            { throw new InvalidDataException("Password must be between 6 and 20 characters."); }
            if (!model.Password.Any(n => char.IsDigit(n)) || !model.Password.Any(n => char.IsLetter(n)))
            { throw new InvalidDataException("Password must contain atleast 1 number and letter."); }

            byte[] passwordHashReg, passwordSaltReg;
            CreatePasswordHash(model.Password, out passwordHashReg, out passwordSaltReg);
            return CreateUser(new User()
            {
                UserName = model.Username,
                PasswordHash = passwordHashReg,
                PasswordSalt = passwordSaltReg,
                Admin = false
            });
        }

        private User CreateUser(User user)
        {
            _ctx.Attach(user).State = EntityState.Added;
            _ctx.SaveChanges();
            return user;
        }

        public User DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            return _ctx.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByUserName(string name)
        {
            return _ctx.Users.FirstOrDefault(u => u.UserName == name);
        }

        public User UpdateUser(User userUpdate)
        {
            throw new NotImplementedException();
        }


        // Hashing stuff
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //Create a Salt key and a PWHash for the registering user.
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            //Applying the stored key to the chosen Crypt, if the wrong key is used different hash will be the result
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                //Computing hash of given password
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    //Checks every charachter of the stored hash.
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        // Used to generate JWT
        public string GenerateToken(User user)
        {
            //Addming username claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            //Adding admin Claim if it exists
            if (user.Admin)
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            //Creating a token for the user
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    JwtKey.Key,
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(
                    null,
                    null,
                    claims.ToArray(),
                    DateTime.Now,
                    DateTime.Now.AddYears(1)));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
