using Newtonsoft.Json;
using System;

namespace hb_desktop
{
    public class ClientModel
    {
        [JsonProperty("ID")]
        public String ID { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("address")]
        public String address { get; set; }

        [JsonProperty("city")]
        public String city { get; set; }

        [JsonProperty("state")]
        public String state { get; set; }

        [JsonProperty("zip")]
        public String zip { get; set; }

        [JsonProperty("phone")]
        public String phone { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }
    }
}
