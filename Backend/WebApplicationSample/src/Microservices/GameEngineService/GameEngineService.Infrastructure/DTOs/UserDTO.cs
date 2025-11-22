using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Domain.Entities;

namespace GameEngineService.Infrastructure.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public UserDTO() { }
        public UserDTO(string id) { Id = id; }
        public static User ToUser(UserDTO dto)
        {
            var id = Int32.Parse(dto.Id);
            var user = new User(id);
            return user;
        }
    }
}
