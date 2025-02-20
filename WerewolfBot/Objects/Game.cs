using Discord;
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
    public GameSettings Settings { get; private set; }

    /// <summary>
    /// A <see cref="List{T}"/> of <see cref="Player"/> that are participating in this game.
    /// </summary>
    public List<Player> players;

    /// <summary>
    /// A <see cref="List{T}"/> of <see cref="Night"/> representing the nights that have passed.
    /// </summary>
    List<Night> passedNights;
    
    /// <summary>
    /// A <see cref="List{T}"/> of <see cref="Night"/> representing the days that have passed.
    /// </summary>
    List<Day> passedDays;

    /// <summary>
    /// The current game-phase.
    /// </summary>
    IGamePhase currentGamePhase;

    /// <summary>
    /// The Text-Channel associated with this Game.
    /// </summary>
    IGuildChannel textChannel;

    /// <summary>
    /// Creates a new <see cref="Game"/> instance
    /// </summary>
    /// <param name="_settings">The <see cref="GameSettings"> that should be used for this instance.</see></param>
    public Game(GameSettings _settings, List<Player> _players)
    {
        Settings = _settings;
        players = _players;
    }

    /// <summary>
    /// Starts the <see cref="Game"/> instance.
    /// </summary>
    public void Start()
    {

    }

    /// <summary>
    /// Abandones the Game while notifying Players.
    /// </summary>
    public void Abandone()
    {

    }
}