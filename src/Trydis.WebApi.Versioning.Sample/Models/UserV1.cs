namespace Trydis.WebApi.Versioning.Sample.Models
{
    [VersionedMediaType("application/vnd.versioningsample.user-v1+json")]
    public class UserV1
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}