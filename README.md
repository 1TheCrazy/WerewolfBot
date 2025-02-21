# WerewolfBot

## Prerequisites

### Libs
To use the audio feature (e.g. narration via voice-chat), you need to the binary with the necessary DLL's. 
<br>They are located [here](WerewolfBot/libs).
<br>Just put them next to your binary.
<br>If you're using Visual Studio that would be `C:\Users\User\source\repos\WerewolfBot\WerewolfBot\bin\Debug\net8.0`.
<br>If you encounter any issues with this (especially as a Linux-User), please refer to [this documentation](https://docs.discordnet.dev/guides/voice/sending-voice.html) from Discord.Net.

### FFmpeg
Since Discord only accepts strict Opus formats as audio sources, we need to convert from mp3 or other formats to Opus.
<br>For that we need FFmpeg, a popular tool for exactly that.
<br>I recommend installing FFmpeg on windows by just typing this in the Terminal: `winget install ffmpeg`
<br>Again, if you encounter any issues here (especially as a Linux-User), please refer to [here](https://www.gyan.dev/ffmpeg/builds/) or [here](https://ffmpeg.org/download.html).
<br>If you install FFmpeg through any method, you'll likely need a system restart, as FFmpeg is added to the PATH.
