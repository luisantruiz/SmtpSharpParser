# SmtpSharpParser
SmtpSharpParser main use is to parse a string into a SmtpSection variable. Instead of reading it from web.config, app.config, ServiceConfiguration.*.cscfg you can get the Smtp configuration from a string, like a connection string.

## Installation

1. Open the Nuget Package Manager Console
2. Install-Package SmtpSharpParser
3. Or from the Nuget Package Manager search for SmtpSharpParser and install it from there

## Usage

Example #1:
```javascript
var values = "from=emailaddress@smtp.com;userName=user@smtp.com;password=password;deliveryMethod=Network;deliveryFormat=SevenBit;"
                + "host=mail.smtp.com;enableSsl=true;port=25;defaultCredentials=true;";
var smtpSection = SmtpSharpParser.Parse(values);
```

Example #2:
```javascript
var values = "from=emailaddress@smtp.com,userName=user@smtp.com,password=password,deliveryMethod=Network,deliveryFormat=SevenBit,"
                + "host=mail.smtp.com,enableSsl=true,port=25,defaultCredentials=true,";
var smtpSection = SmtpSharpParser.Parse(values, ",");
```

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## License

MIT License
