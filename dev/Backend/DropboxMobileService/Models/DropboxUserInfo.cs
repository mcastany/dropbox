namespace DropboxMobileService.Models
{
    using Newtonsoft.Json;

    public class DropboxUserInfo
    {
        [JsonProperty("referral_link")]
        public string ReferralLink { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("team")]
        public DropboxTeam Team { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("quota_info")]
        public DropboxQuota QuotaInfo { get; set; }
    }
}