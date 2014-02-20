using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Trydis.WebApi.Versioning.Sample.Models;

namespace Trydis.WebApi.Versioning.Sample.Controllers
{
    [RoutePrefix("users")]
    public class UsersV1Controller : ApiController
    {
        private readonly UserV1[] _usersV1;

        public UsersV1Controller()
        {
            _usersV1 = new[] { new UserV1 { Id = 1, Name = "John Doe" } };
        }

        [VersionedRoute("", 1)]
        public IEnumerable<UserV1> GetUsers()
        {
            return _usersV1;
        }

        [VersionedRoute("{id}", 1)]
        public UserV1 GetUser(int id)
        {
            var user = _usersV1.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user;
        }
    }
}