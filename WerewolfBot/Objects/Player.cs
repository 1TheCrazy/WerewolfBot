using Discord;
using WerewolfBot.Objects.Interfaces;

namespace WerewolfBot.Objects;

class Player
{
    /// <summary>
    /// The discord user associated with this <see cref="Player"/> instance.
    /// </summary>
    public IGuildUser discordUser;

    /// <summary>
    /// The killed event. Fired when a player is killed in any game-phase.
    /// </summary>
    public event Action onKilled;

    /// <summary>
    /// The <see cref="ICard"/> associated with this player instance.
    /// </summary>
    public ICard card;

    /// <summary>
    /// Gets wether the <see cref="Player"/> instance is alive.
    /// </summary>
    public bool isAlive;

    /// <summary>
    /// Kills the <see cref="Player"/> instance.
    /// </summary>
    public void Kill()
    {
        onKilled?.Invoke();
    }

    public Player(IGuildUser _discordUser, ICard _card)
    {
        discordUser = _discordUser;
        card = _card;
        isAlive = true;
    }
}