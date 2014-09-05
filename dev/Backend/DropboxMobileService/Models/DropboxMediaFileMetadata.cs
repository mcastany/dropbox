namespace DropboxMobileService.Models
{
    using Newtonsoft.Json;

    public class DropboxMediaFileMetadata
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("expires")]
        public string Expires { get; set; }
    }
}