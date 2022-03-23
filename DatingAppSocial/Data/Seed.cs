using DatingAppSocial.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatingAppSocial.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.User.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/userSeedData.json");
              var users = System.Text.Json.JsonSerializer.Deserialize<List<AppUser>>(userData);
            //var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@ssw0rd"));
                user.PasswordSalt = hmac.Key;

                context.User.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}
