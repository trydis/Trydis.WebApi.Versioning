using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using Trydis.WebApi.Versioning.Formatting;

namespace Trydis.WebApi.Versioning
{
    /// <summary>
    /// Registers the versioned media types.
    /// </summary>
    public static class VersionedMediaTypeConfig
    {
        public static void RegisterTypedFormatters(HttpConfiguration config)
        {
            var types = Assembly.GetCallingAssembly().GetTypes();

            foreach (var type in types)
            {
                var customAttributes = type.GetCustomAttributes(true);
                if (customAttributes.Length == 0) continue;
                var matchingCustomAttributes = customAttributes.OfType<VersionedMediaType>();

                foreach (var matchingCustomAttribute in matchingCustomAttributes)
                {
                    var formatterExists = config.Formatters
                        .OfType<TypedJsonMediaTypeFormatter>()
                        .Any(typeFormatter => typeFormatter.CanReadType(type) && typeFormatter.CanWriteType(type));

                    if (!formatterExists)
                    {
                        config.Formatters.Add(new TypedJsonMediaTypeFormatter(type, new MediaTypeHeaderValue(matchingCustomAttribute.MediaType)));
                    }
                }
            }
        }
    }
}