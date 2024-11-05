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

        public override UserDto Create(UserDto dto)
        {
            var user = MapEntity(dto);
            users.Add(user);
            return Read(user.Id);
        }

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

        public override UserDto Read(int id)
        {
            UserDto result = new UserDto();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user is not null)
            {
                result = MapDto(user);
            }
            return result;
        }

        public override List<UserDto> Read(string? ricerca = null)
        {
            List<UserDto> result = new List<UserDto>();
            if (ricerca != null)
            {
                List<User> list = new List<User>();
                list = users
                    .Where(u => u.Nome.Contains(ricerca, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (list.Count == 0)
                {
                    list = users
                        .Where(u => u.Email.Contains(ricerca, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        result.Add(MapDto(item));
                    }
                }
            }
            else
            {
                foreach (var item in users)
                {
                    result.Add(MapDto(item));
                }
            }
            return result;
        }

        public override UserDto Update(int id, UserDto dto)
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
                Id = entity.Id,
                Nome = entity.Nome,
                Email = entity.Email
            };
            return userDto;
        }

        protected override User MapEntity(UserDto dto)
        {
            User user = new User
            {
                Id = users.Count + 1,
                Nome = dto.Nome,
                Email = dto.Email
            };
            return user;
        }
    }
}
