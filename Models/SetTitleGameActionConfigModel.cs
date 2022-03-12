using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SuchByte.TwitchPlugin.Models
{
    public class SetTitleGameActionConfigModel : ISerializableConfiguration
    {
        public string StreamTitle { get; set; } = "";
        public bool UseStreamTitle { get; set; } = true;
        public string Game { get; set; } = "";
        public bool UseGame { get; set; } = true;

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
        public static SetTitleGameActionConfigModel Deserialize(string config)
        {
            return ISerializableConfiguration.Deserialize<SetTitleGameActionConfigModel>(config);
        }
    }
}
