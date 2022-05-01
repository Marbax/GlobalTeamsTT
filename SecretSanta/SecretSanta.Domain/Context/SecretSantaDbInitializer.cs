using SecretSanta.Domain.Models;
using System.Linq;

namespace SecretSanta.Domain.Context
{
    public static class SecretSantaDbInitializer
    {
        public static void Initialize(SecretSantaDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
                return;

            var users = new User[]
            {
                new User{FirstName = "Nik", LastName = "Morozov" },
                new User{FirstName = "Ilon", LastName = "Mask" },
                //new User{FirstName = "Putin", LastName = "Huilo" },
                new User{FirstName = "Ivan", LastName = "Ivanov" },
            };
            foreach (var item in users)
                context.Add(item);
            context.SaveChanges();
        }
    }
}
