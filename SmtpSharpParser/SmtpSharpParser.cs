using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Net.Mail;

namespace SmtpSharpParser
{
    /// <summary>
    /// SmtpSharpParser main use is to parse a string into a SmtpSection variable.
    /// Instead of reading it from web.config, app.config, ServiceConfiguration.*.cscfg
    /// you can get the smtp configuration from a string, like a connection string.
    /// </summary>
    public static class SmtpSharpParser
    {
        private static Dictionary<string, string> _dictionary;

        /// <summary>
        /// Parses a string into a SmtpSection instance.
        /// </summary>
        /// <param name="settingName">Configuration setting</param>
        /// <returns></returns>
        public static SmtpSection Parse(string settingValue)
        {
            ParseSettingValue(settingValue);

            var smtpSection = new SmtpSection();
            smtpSection.From = GetValueAssociatedWithTheKey("from");
            smtpSection.DeliveryMethod = (SmtpDeliveryMethod)Enum.Parse(typeof(SmtpDeliveryMethod), GetValueAssociatedWithTheKey("deliveryMethod"), true);
            smtpSection.Network.Host = GetValueAssociatedWithTheKey("host");
            if (ContainsKey("port"))
            {
                smtpSection.Network.Port = int.Parse(GetValueAssociatedWithTheKey("port"));
            }
            if (ContainsKey("userName"))
            {
                smtpSection.Network.UserName = GetValueAssociatedWithTheKey("userName");
            }
            if (ContainsKey("password"))
            {
                smtpSection.Network.Password = GetValueAssociatedWithTheKey("password");
            }
            if (ContainsKey("enableSsl"))
            {
                smtpSection.Network.EnableSsl = bool.Parse(GetValueAssociatedWithTheKey("enableSsl"));
            }
            if (ContainsKey("defaultCredentials"))
            {
                smtpSection.Network.DefaultCredentials = bool.Parse(GetValueAssociatedWithTheKey("defaultCredentials"));
            }
            if (ContainsKey("clientDomain"))
            {
                smtpSection.Network.ClientDomain = GetValueAssociatedWithTheKey("clientDomain");
            }
            if (ContainsKey("deliveryFormat"))
            {
                smtpSection.DeliveryFormat = (SmtpDeliveryFormat)Enum.Parse(typeof(SmtpDeliveryFormat), GetValueAssociatedWithTheKey("deliveryFormat"), true);
            }

            return smtpSection;
        }

        /// <summary>
        /// Parse a string into a dictionary, key=value;key1=value;
        /// </summary>
        /// <param name="value">Value</param>
        private static void ParseSettingValue(string value)
        {
            var settingsDictionary = value.Split(new[] { ';' });
            _dictionary = new Dictionary<string, string>();
            foreach (var setting in settingsDictionary)
            {
                var values = setting.Split('=');
                _dictionary.Add(values[0].ToLower(), values[1]);
            }
        }

        /// <summary>
        /// Gets the value from the dictionary for the given key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        private static string GetValueAssociatedWithTheKey(string key)
        {
            string value = null;
            if (ContainsKey(key))
            {
                value = _dictionary[key.ToLower()];
            }
            return value;
        }

        /// <summary>
        /// Determinates if the key exits in the dictionary
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        private static bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key.ToLower());
        }
    }
}
