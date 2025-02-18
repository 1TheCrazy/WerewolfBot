using WerewolfBot.Objects.Interfaces;

namespace WerewolfBot.Objects.Cards;

/// <summary>
/// A register of all cards in the currently implemented.
/// </summary>
class CardRegister
{
    /// <summary>
    /// A <see cref="List{T}"/> of all cards implemented
    /// </summary>
    /// <remarks>When creating a new card, put it here in the order when it should wake up at night. 
    /// When your card does not need to wake up, just put it anywhere.
    /// </remarks>
    public static readonly List<ICard> Cards = [];
}