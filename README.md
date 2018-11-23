# Discord Achievement Bot
Master: [![Build status](https://ci.appveyor.com/api/projects/status/9ap9d3d2cb6uw36s/branch/master?svg=true)](https://ci.appveyor.com/project/Chris-Johnston/discordachievementbot/branch/master)
Current: [![Build status](https://ci.appveyor.com/api/projects/status/9ap9d3d2cb6uw36s?svg=true)](https://ci.appveyor.com/project/Chris-Johnston/discordachievementbot)

A bot that generates Xbox-style achievement popups for a Discord server.

![Gif showing example operation][ExampleGif]

## How to Use

**Server Owners**: [You can add this bot to your server using this link][InviteLink].

**Developers/Contributors**:

- Clone this repo.
- Rebuild/download NuGet dependencies.
  - Requires Discord.Net lib: https://github.com/RogueException/Discord.Net
- Modify `ExampleConfig.xml` with the correct paths and bot user tokens.
- Run with `dotnet run -config:C:\Path\To\config.xml`
- Add to your server(s). The command window will print out an invite url once the bot has connected.

### Deployment Notes on Linux

This bot requires that the font `segoeui.ttf` is installed on your system. This is not included
in any packages or distros, so you'll need to copy it from a Windows installation.

The font can be found under `C:\Windows\Fonts\segoeui.ttf`.

Copy it to the directory: `~/.fonts/segoeui.ttf`. Run the bot and try generating an image to verify that it works.

If it's still not working, and the error message `Fontconfig error: Cannot load default config file` appears in the
console output, [then try the following (from StackOverflow):](https://askubuntu.com/a/708541)

```console
export FONTCONFIG_PATH=/etc/fonts
```

Then, run the bot again. If the error is resolved, then you can append the command to the end of the 
`~/.bashrc` file.

#### Systemctl service

I've included a sample systemctl service that can be used to host the bot. This file should be adjusted
to match the specific configuration of the server the bot is being run on.

Install the systemctl service:

```console
chmod 664 achievementbot.service
sudo cp achievementbot.service /etc/systemd/service/
```

Enable and run the service:

```console
sudo systemctl enable achievementbot.service
sudo systemctl start achievementbot.service
```

Check that it's working:

```console
journalctl -u achievementbot.service
```

## Commands

_All commands must begin with the command prefix `++`, or must involve @Mentioning the bot._

#### Generate Command

```
++Get

++Generate "Your Achievement Text" [Numeric Gamerscore] [XboxOne|XboxOneRare|Xbox360]
```

Achievement text that contains whitespace must be surrounded with quotes. Currently,
no emotes are supported, including both unicode and guild emotes.

The numeric gamerscore must be an integer number.

The last parameter specifies the style. Either `XboxOne` `XboxOneRare` or `Xbox360`,
which will generate different styles of images. The `Xbox360` preset is not done yet.

Using this command will mention the user that requested it to indicate that it is processing, and then mention the user again once it is created.

#### Meta commands

```
++Help
```

Replies with help information.

```
++About
```

Replies with some meta information about the bot.

```
++InviteLink
```

Replies with the invite link for the bot.


## Permissions

**Permissions for Bot**

The bot requires the following permissions:

- Read Messages (So the bot can receive commands.)
- Send Messages (So the bot can reply to commands.)
- Attach Files (So the bot can include images.)

**Permissions for User**

Users do not require any permissions.

[ExampleGif]: http://i.imgur.com/9lzwx6j.gif
[InviteLink]: https://discordapp.com/oauth2/authorize?client_id=347632041300328459&scope=bot&permissions=35840