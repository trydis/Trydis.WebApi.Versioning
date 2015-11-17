using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Trydis.WebApi.Versioning.Formatting
{
    /// <summary>
    /// Typed media type JSON formatter.
    /// </summary>
    public class TypedJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private readonly Type _resourceType;

        public TypedJsonMediaTypeFormatter(Type resourceType, MediaTypeHeaderValue mediaType)
        {
            _resourceType = resourceType;

            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(mediaType);
        }

        private bool CompareTypes(Type type)
        {
            var typesMatch = _resourceType == type;
            if (typesMatch)
            {
                return true;
            }

            var enumerableType = type.GenericTypeArguments.FirstOrDefault();
            if (enumerableType == null)
            {
                return false;
            }

            return _resourceType == enumerableType;
        }

        public override bool CanReadType(Type type)
        {
            return CompareTypes(type);
        }

        public override bool CanWriteType(Type type)
        {
            return CompareTypes(type);
        }
    }
}