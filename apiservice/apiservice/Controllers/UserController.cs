using Microsoft.AspNetCore.Mvc;
using webservice.Entities;

namespace apiservice.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    [HttpGet(Name = "GetAllUser")]
    public IEnumerable<User> Get()
    {
        return new List<User>
        {
            new()
            {
                Id = 0,
                FirstName = "Alexander",
                LastName = "Gorodnikov",
                BirthDay = DateTime.Now,
                City = "Moscow"
            },
            new()
            {
                Id = 1,
                FirstName = "Maxim",
                LastName = "Ivanov",
                BirthDay = DateTime.Now,
                City = "Samara"
            },
            new()
            {
                Id = 2,
                FirstName = "Nikita",
                LastName = "Petrov",
                BirthDay = DateTime.Now,
                City = "Saratov"
            },
        }.ToArray();
    }
}