namespace DropboxMobileService.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class DropboxMetadata
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("rev")]
        public string Rev { get; set; }

        [JsonProperty("thumb_exists")]
        public bool ThumbExists { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("is_dir")]
        public bool IsDir { get; set; }
 
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("bytes")]
        public double Bytes { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("root")]
        public string Root { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("client_mtime")]
        public string ClientMtime { get; set; }

        [JsonProperty("contents")]
        public IEnumerable<DropboxMetadata> Contents { get; set; }
    }
}