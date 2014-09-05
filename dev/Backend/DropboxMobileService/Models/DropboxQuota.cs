namespace DropboxMobileService.Models
{
    using Newtonsoft.Json;

    public class DropboxQuota
    {
        [JsonProperty("shared")]
        public string Shared { get; set; }

        [JsonProperty("quota")]
        public string Quota { get; set; }

        [JsonProperty("normal")]
        public string Normal { get; set; }
    }
}