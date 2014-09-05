namespace DropboxMobileService.Models
{
    using Newtonsoft.Json;

    public class DropboxTeam 
    {
        [JsonProperty("name")]
        public string Name {get; set;}
    }
}