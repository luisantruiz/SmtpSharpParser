using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Net.Mail;

namespace StmpSharpParser
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
            ParseSetting(settingValue);

            var smtpSection = new SmtpSection();
            smtpSection.From = GetValueByKey("from");
            smtpSection.DeliveryMethod = (SmtpDeliveryMethod)Enum.Parse(typeof(SmtpDeliveryMethod), GetValueByKey("deliveryMethod"), true);
            smtpSection.Network.Host = GetValueByKey("host");
            if (KeyExists("port"))
            {
                smtpSection.Network.Port = int.Parse(GetValueByKey("port"));
            }
            if (KeyExists("userName"))
            {
                smtpSection.Network.UserName = GetValueByKey("userName");
            }
            if (KeyExists("password"))
            {
                smtpSection.Network.Password = GetValueByKey("password");
            }
            if (KeyExists("enableSsl"))
            {
                smtpSection.Network.EnableSsl = bool.Parse(GetValueByKey("enableSsl"));
            }
            if (KeyExists("defaultCredentials"))
            {
                smtpSection.Network.DefaultCredentials = bool.Parse(GetValueByKey("defaultCredentials"));
            }
            if (KeyExists("clientDomain"))
            {
                smtpSection.Network.ClientDomain = GetValueByKey("clientDomain");
            }
            if (KeyExists("deliveryFormat"))
            {
                smtpSection.DeliveryFormat = (SmtpDeliveryFormat)Enum.Parse(typeof(SmtpDeliveryFormat), GetValueByKey("deliveryFormat"), true);
            }

            return smtpSection;
        }

        /// <summary>
        /// Parse a string into a dictionary, key=value;key1=value;
        /// </summary>
        /// <param name="value">Value</param>
        private static void ParseSetting(string value)
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
        private static string GetValueByKey(string key)
        {
            string value = null;
            if (KeyExists(key))
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
        private static bool KeyExists(string key)
        {
            return _dictionary.ContainsKey(key.ToLower());
        }
    }
}
