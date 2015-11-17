using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Routing;

namespace Trydis.WebApi.Versioning
{
    /// <summary>
    /// A constraint implementation that matches the Accept header against an expected version value.
    /// </summary>
    public class VersionConstraint : IHttpRouteConstraint
    {
        private const int DefaultVersion = 1;

        public int AllowedVersion { get; private set; }

        public static string MediaTypePattern { get; set; }
        
        public VersionConstraint(int allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                var version = GetVersionFromHeader(request) ?? DefaultVersion;
                if (version == AllowedVersion)
                {
                    return true;
                }
            }

            return false;
        }

        private static int? GetVersionFromHeader(HttpRequestMessage request)
        {
            string versionString = null;

            if (request.Headers.Accept.Count == 1)
            {
                var regex = new Regex(MediaTypePattern, RegexOptions.IgnoreCase);
                var accept = request.Headers.Accept;

                foreach (var match in accept.Select(mime => regex.Match(mime.MediaType)).Where(match => match.Success))
                {
                    versionString = match.Groups[2].ToString();
                    break;
                }
            }
            else
            {
                return null;
            }

            int version;
            if (versionString != null && int.TryParse(versionString, out version))
            {
                return version;
            }

            return null;
        }
    }
}