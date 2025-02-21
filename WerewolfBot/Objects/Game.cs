﻿using Discord;
using Discord.Audio;
using Discord.WebSocket;
using System.Reflection;
using WerewolfBot.Objects.Interfaces;
using WerewolfBot.Objects.Play;

namespace WerewolfBot.Objects;

/// <summary>
/// A <see cref="Game"/>
/// </summary>
class Game
{
    /// <summary>
    /// The <see cref="GameSettings"/> of the game.
    /// </summary>
    public GameSettings settings;

    /// <summary>
    /// A <see cref="List{T}"/> of <see cref="Player"/> that are participating in this game.
    /// </summary>
    public List<Player> players;

    /// <summary>
    /// A <see cref="List{T}"/> of <see cref="Night"/> representing the nights that have passed.
    /// </summary>
    public List<Night> passedNights;

    /// <summary>
    /// A <see cref="List{T}"/> of <see cref="Night"/> representing the days that have passed.
    /// </summary>
    public List<Day> passedDays;

    /// <summary>
    /// The current game-phase.
    /// </summary>
    public IGamePhase currentGamePhase;

    /// <summary>
    /// The Text-Channel associated with this Game.
    /// </summary>
    public IGuildChannel textChannel;

    /// <summary>
    /// The <see cref="IAudioClient"/> representing a connection to the Audio-Channel associated with this game.
    /// </summary>
    public IAudioClient currentVoiceConnection;

    public bool isRunning { get; private set; }

    /// <summary>
    /// Creates a new <see cref="Game"/> instance
    /// </summary>
    public Game()
    {
        settings = GameSettings.DefaultSettings;

        isRunning = false;
    }

    /// <summary>
    /// Starts the <see cref="Game"/> instance.
    /// </summary>
    public void Start()
    {
        isRunning = true;

        // Correct number of werewolfs.
    }

    /// <summary>
    /// Abandones the Game while notifying Players.
    /// </summary>
    public void Abandone(SocketUser user)
    {

    }

    // Keep the audio connection alive
    public async Task KeepAudioAlive()
    {
        var connection = currentVoiceConnection.CreateDirectOpusStream();

        byte[] silenceFrame = { 0xF8, 0xFF, 0xFE }; // 48kHz Opus silence frame

        while (currentVoiceConnection.ConnectionState == ConnectionState.Connected)
        {
            await connection.WriteAsync(silenceFrame, 0, silenceFrame.Length); // Send silence
            await Task.Delay(10000); // Delay for 10s, to not spam anything
        }
    }
}