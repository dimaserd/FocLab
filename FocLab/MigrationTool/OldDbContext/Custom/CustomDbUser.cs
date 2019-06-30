using System;
using PencilNCo.Model.Entities.Users;

namespace PencilNCo.Model.Entities.Custom
{
    public class CustomDbUser
    {
        public CustomDbUser()
        {

        }

        public CustomDbUser(ApplicationUserDto userDto)
        {
            Id = userDto.Id;
            Name = userDto.Name;
            Email = userDto.Email;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Email { get; set; }
    }

    public class CustomDbUserDto
    {
        public CustomDbUserDto()
        {

        }

        public CustomDbUserDto(ApplicationUserDto userDto)
        {
            Id = userDto.Id;
            Name = userDto.Name;
            Email = userDto.Email;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}