# Discord Achievement Bot
Master: [![Build status](https://ci.appveyor.com/api/projects/status/9ap9d3d2cb6uw36s/branch/master?svg=true)](https://ci.appveyor.com/project/Chris-Johnston/discordachievementbot/branch/master)
Current: [![Build status](https://ci.appveyor.com/api/projects/status/9ap9d3d2cb6uw36s?svg=true)](https://ci.appveyor.com/project/Chris-Johnston/discordachievementbot)

A bot that generates Xbox-style achievement popups for a Discord server.

![Gif showing example operation][ExampleGif]

## How to Use

**Server Owners**: [You can add this bot to your server using this link](https://discordapp.com/oauth2/authorize?client_id=347632041300328459&scope=bot).

**Developers/Contributors**:

- Clone this repo.
- Rebuild/download NuGet dependencies.
  - Requires Discord.Net lib: https://github.com/RogueException/Discord.Net
- Modify `ExampleConfig.xml` with the correct paths and bot user tokens.
- Run with `dotnet run -config:C:\Path\To\config.xml`
- Add to your server(s). The command window will print out an invite url once the bot has connected.


## Commands

_All commands must begin with the command prefix `+`, or must involve @Mentioning the bot._

Example:

```
+Ping

@AchievementBot Ping
```

#### Generate Command

```
+Get

+Generate "Your Achievement Text" [Numeric Gamerscore] [XboxOne|XboxOneRare|Xbox360]
```

Achievement text that contains whitespace must be surrounded with quotes. Currently,
no emoji are supported, built-in or custom.

The numeric gamerscore must be an integer number.

The last parameter specifies the style. Either `XboxOne` `XboxOneRare` or `Xbox360`,
which will generate a differently styled image. Currently only the `XboxOne`
preset works.

Using this command will mention the user that requested it to indicate that it is processing, and then mention the user again once it is created.

#### Meta commands

```
+Help
```

Replies with help information.

```
+About
```

Replies with some meta information about the bot.

```
+InviteLink
```

Replies with the invite link for the bot.

```
+Ping
```

Replies back with "Pong!".


## Permissions

**Permissions for Bot**

The bot requires the following permissions:

- Read Messages (So the bot can get commands.)
- Send Messages (So the commands do something.)
- Attach Files (So the bot can include images.)

**Permissions for User**

Users who wish to use the bot require only one permission:

- Manage Messages (So that the user can delete the messages. This bot is meant to be reserved for moderators/admins, for special uses.)

[ExampleGif]: http://i.imgur.com/9lzwx6j.gif
