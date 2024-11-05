using System.Collections.Generic;
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


        protected override User AddEntity(User entity)
        {
            entity.Id = users.Max(u => u.Id) + 1;
            users.Add(entity);
            return entity;
        }

        protected override List<User> GetAll()
        {
            return users.ToList();
        }

        protected override List<User> GetByString(string ricerca)
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
            return list;
        }

        protected override User? GetEntityById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
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
                Nome = dto.Nome,
                Email = dto.Email
            };
            return user;
        }

        protected override void Remove(int id)
        {
            users.RemoveAll(u => u.Id == id);
        }

        protected override User UpdateEntity(User user, UserDto dto)
        {
            user.Nome = dto.Nome;
            user.Email = dto.Email;

            return user;
        }
    }
}
