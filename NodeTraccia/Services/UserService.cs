using NodeTraccia.Dtos;
using NodeTraccia.Models;
using NodeTraccia.Repositories;

namespace NodeTraccia.Services
{
    public class UserService : CrudServiceBase<User, UserDto>
    {
        private static List<User> users = new List<User> {
            new User { Id = 1, Nome = "Alice", Email = "alice@example.com" },
            new User { Id = 2, Nome = "Marco", Email = "marco@example.com" },
            new User { Id = 3, Nome = "Giorgio", Email = "giorgio@example.com" },
            new User { Id = 4, Nome = "Martina", Email = "martina@example.com" }
        };

        public extern User Create(UserDto dto);
        

        public override bool Delete(int id)
        {
            users.RemoveAll(u => u.Id == id);
            var result = users.FirstOrDefault(u => u.Id == id);
            if (result is not null)
            {
                return false;
            }
            return true;
        }

        public extern User Read(int id);
        

        public override List<User> Read(string? ricerca = null)
        {
            List<User> list = new List<User>();
            if (ricerca != null)
            {

                list = users
                    .Where(u => u.Nome.Contains(ricerca, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (list.Count == 0)
                {
                    list = users
                        .Where(u => u.Email.Contains(ricerca, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                return list;
            }
            return users;
        }

        public override User Update(int id, UserDto dto)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user is not null)
            {
                user.Nome = dto.Nome;
                user.Email = dto.Email;
            }
            return Read(id);
        }
        protected override UserDto MapDto(User entity)
        {
            UserDto userDto = new UserDto
            {
                Nome = entity.Nome,
                Email = entity.Email
            };
            return userDto;
        }

        protected override User MapEntity(UserDto dto)
        {
            User user = new User
            {
                Id = users.Max(u => u.Id) + 1,
                Nome = dto.Nome,
                Email = dto.Email
            };
            return user;
        }
    }
}
