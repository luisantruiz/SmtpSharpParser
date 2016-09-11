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
        #region Private Members Variables

        private static Dictionary<string, string> _dictionary;
        private static SmtpSection _smtpSection;
        private static string _settingValue;
        private static char _splitCharacter = ';';

        #endregion

        #region Constructors

        static SmtpSharpParser()
        {
            _smtpSection = new SmtpSection();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Parses a string into a SmtpSection instance.
        /// </summary>
        /// <param name="settingName">Configuration setting</param>
        /// <returns></returns>
        public static SmtpSection Parse(string settingValue)
        {
            _settingValue = settingValue;
            Parse();

            return _smtpSection;
        }

        /// <summary>
        /// Parses a string into a SmtpSection instance with a custom values separator.
        /// </summary>
        /// <param name="settingName">Configuration setting</param>
        /// <returns></returns>
        public static SmtpSection Parse(string settingValue, string splitCharacter)
        {
            _settingValue = settingValue;
            _splitCharacter = Char.Parse(splitCharacter);
            Parse();

            return _smtpSection;
        }

        /// <summary>
        /// Parses a string into a SmtpSection instance with a custom values separator.
        /// </summary>
        /// <param name="settingName">Configuration setting</param>
        /// <returns></returns>
        public static SmtpSection Parse(string settingValue, char splitCharacter)
        {
            _settingValue = settingValue;
            _splitCharacter = splitCharacter;
            Parse();

            return _smtpSection;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Internal method to parse the string into a SmtpSection instance
        /// </summary>
        private static void Parse()
        {
            ParseSettingValue(_settingValue);

            _smtpSection.From = GetValueAssociatedWithTheKey("from");
            _smtpSection.DeliveryMethod = (SmtpDeliveryMethod)Enum.Parse(typeof(SmtpDeliveryMethod), GetValueAssociatedWithTheKey("deliveryMethod"), true);
            _smtpSection.Network.Host = GetValueAssociatedWithTheKey("host");
            if (ContainsKey("port"))
            {
                _smtpSection.Network.Port = int.Parse(GetValueAssociatedWithTheKey("port"));
            }
            if (ContainsKey("userName"))
            {
                _smtpSection.Network.UserName = GetValueAssociatedWithTheKey("userName");
            }
            if (ContainsKey("password"))
            {
                _smtpSection.Network.Password = GetValueAssociatedWithTheKey("password");
            }
            if (ContainsKey("enableSsl"))
            {
                _smtpSection.Network.EnableSsl = bool.Parse(GetValueAssociatedWithTheKey("enableSsl"));
            }
            if (ContainsKey("defaultCredentials"))
            {
                _smtpSection.Network.DefaultCredentials = bool.Parse(GetValueAssociatedWithTheKey("defaultCredentials"));
            }
            if (ContainsKey("clientDomain"))
            {
                _smtpSection.Network.ClientDomain = GetValueAssociatedWithTheKey("clientDomain");
            }
            if (ContainsKey("deliveryFormat"))
            {
                _smtpSection.DeliveryFormat = (SmtpDeliveryFormat)Enum.Parse(typeof(SmtpDeliveryFormat), GetValueAssociatedWithTheKey("deliveryFormat"), true);
            }
        }

        /// <summary>
        /// Parse a string into a dictionary, key=value;key1=value;
        /// </summary>
        /// <param name="value">Value</param>
        private static void ParseSettingValue(string value)
        {
            var settingsDictionary = value.Split(new[] { _splitCharacter }, StringSplitOptions.RemoveEmptyEntries);
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

        #endregion
    }
}
