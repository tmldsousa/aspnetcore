using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CoreSandbox.Domain.Data;
using CoreSandbox.Domain.Entities;

namespace CoreSandbox.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        CoreSandboxContext Context { get; set; }

        public UsersController(CoreSandboxContext context)
        {
            Context = context;
        }

        // GET api/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Context.Users;
        }
        
        // POST api/users
        [HttpPost]
        public User Post()
        {
            var guid = Guid.NewGuid();
            var user = new User
            {
                Email = guid + "@aspnetcore-sandbox.xxx",
                Name = "User " + guid,
                Username = guid.ToString(),
                Password = guid.ToString()
            };
            Context.Users.Add(user);
            Context.SaveChanges();
            return user;
        }

        // PUT api/users/5
        [HttpPut("{username}")]
        public User Put(string username)
        {
            var user = new User
            {
                Email = username + "@aspnetcore-sandbox.xxx",
                Name = "User " + username,
                Username = username,
                Password = username
            };
            Context.Users.Add(user);
            Context.SaveChanges();
            return user;
        }

        // DELETE api/users/5
        [HttpDelete("{username}")]
        public void Delete(string username)
        {
            var user = Context.Users.FirstOrDefault(u => String.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
            Context.Users.Remove(user);
            Context.SaveChanges();
        }
    }
}
