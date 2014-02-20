using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Trydis.WebApi.Versioning.Sample.Models;

namespace Trydis.WebApi.Versioning.Sample.Controllers
{
    [RoutePrefix("users")]
    public class UsersV2Controller : ApiController
    {
        private readonly UserV2[] _usersV1;

        public UsersV2Controller()
        {
            _usersV1 = new[] { new UserV2 { Id = 1, Name = "John Doe", Age = 75} };
        }

        [VersionedRoute("", 2)]
        public IEnumerable<UserV2> GetUsers()
        {
            return _usersV1;
        }

        [VersionedRoute("{id}", 2)]
        public UserV2 GetUser(int id)
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