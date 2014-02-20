using System;

namespace Trydis.WebApi.Versioning
{
    /// <summary>
    /// Annotates a class with a media type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class VersionedMediaType : Attribute
    {
        public string MediaType { get; private set; }

        public VersionedMediaType(string mediaType)
        {
            if (mediaType == null) throw new ArgumentNullException("mediaType");
            MediaType = mediaType;
        }
    }
}