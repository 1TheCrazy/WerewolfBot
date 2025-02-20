using WerewolfBot.Objects.Interfaces;
using WerewolfBot.Objects.Cards;

namespace WerewolfBot.Objects;

/// <summary>
/// The settings used when creating a game of werewolf.
/// </summary>
class GameSettings
{
    /// <summary>
    /// The default <see cref="GameSettings"/>.
    /// </summary>
    public static GameSettings DefaultSetting { get { return new GameSettings(CardRegister.Cards, TimeSpan.Zero, true, true); } }

    /// <summary>
    /// A list of cards that can be assigned.
    /// </summary>
    List<ICard> allowedCards;

    /// <summary>
    /// The maximum time for accusations during the day. If the time is reached and noone was killed, it just skips to the night.
    /// </summary>
    /// <remarks>When assigned 0, there won't be a time limit for accusations.</remarks>
    TimeSpan accuseTime;

    /// <summary>
    /// Specify wether audio narration should be used.
    /// </summary>
    bool audioNarration;

    /// <summary>
    /// Specify wether text narration should be used.
    /// </summary>
    bool textNarration;

    /// <summary>
    /// Specify wether the auto-assingment of Cards should prefer that cards that wake up at night should be preferenced (Auto-Assignment is activated once all cards are allowed)
    /// </summary>
    bool preferActionCards;

    /// <summary>
    /// Specify wether cards should be revealed on death
    /// </summary>
    bool reavealCardsOnDeath;

    /// <summary>
    /// The number of Werewolfs (if set to -1 the number will be set to n/3)
    /// </summary>
    int numberOfWerewolfs;

    /// <summary>
    /// Create a new <see cref="GameSettings"/> instance.
    /// </summary>
    public GameSettings(List<ICard> _allowedCards, TimeSpan _accuseTime, bool _audioNarration, bool _textNarration)
    {
        allowedCards = _allowedCards;
        accuseTime = _accuseTime;
        audioNarration = _audioNarration;
        textNarration = _textNarration;
    }
}