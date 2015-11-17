namespace Trydis.WebApi.Versioning.Sample.Models
{
    [VersionedMediaType("application/vnd.versioningsample.user-v2+json")]
    public class UserV2 : UserV1
    {
        public int Age { get; set; }
    }
}