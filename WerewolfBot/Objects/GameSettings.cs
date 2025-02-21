using WerewolfBot.Objects.Interfaces;
using WerewolfBot.Objects.Cards;
using Newtonsoft.Json;

namespace WerewolfBot.Objects;

/// <summary>
/// The settings used when creating a game of werewolf.
/// </summary>
class GameSettings
{
    /// <summary>
    /// The default <see cref="GameSettings"/>.
    /// </summary>
    [JsonIgnore]
    public static GameSettings DefaultSettings { get { return new GameSettings(CardRegister.Cards, TimeSpan.Zero, true, true, true, true, -1); } }

    /// <summary>
    /// A list of cards that can be assigned.
    /// </summary>
    public List<ICard> allowedCards;

    /// <summary>
    /// The maximum time for accusations during the day. If the time is reached and noone was killed, it just skips to the night.
    /// </summary>
    /// <remarks>When assigned 0, there won't be a time limit for accusations.</remarks>
    public TimeSpan accuseTime;

    /// <summary>
    /// Specify wether audio narration should be used.
    /// </summary>
    public bool audioNarration;

    /// <summary>
    /// Specify wether text narration should be used.
    /// </summary>
    public bool textNarration;

    /// <summary>
    /// Specify wether the auto-assingment of Cards should prefer that cards that wake up at night should be preferenced (Auto-Assignment is activated once all cards are allowed)
    /// </summary>
    public bool preferActionCards;

    /// <summary>
    /// Specify wether cards should be revealed on death
    /// </summary>
    public bool revealCardsOnDeath;

    /// <summary>
    /// The number of Werewolfs (if set to -1 the number will be set to n/3)
    /// </summary>
    public int numberOfWerewolfs;

    /// <summary>
    /// Create a new <see cref="GameSettings"/> instance.
    /// </summary>
    public GameSettings(List<ICard> _allowedCards, TimeSpan _accuseTime, bool _audioNarration, bool _textNarration, bool _preferActionCards, bool _revealCardsOnDeath, int _numberOfWerewolfs)
    {
        allowedCards = _allowedCards;
        accuseTime = _accuseTime;
        audioNarration = _audioNarration;
        textNarration = _textNarration;
        preferActionCards = _preferActionCards;
        revealCardsOnDeath = _revealCardsOnDeath;
        numberOfWerewolfs = _numberOfWerewolfs;
    }
}