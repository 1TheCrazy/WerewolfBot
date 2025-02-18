namespace WerewolfBot.Objects.Interfaces;
/// <summary>
/// The contract for a card.
/// </summary>
interface ICard
{
    /// <summary>
    /// The name of the card.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of the card.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The path to the sound played when the card is woken during the night. If the card should not be woken cou can leave this empty.
    /// </summary>
    public string SoundPath { get; set; }

    /// <summary>
    /// The text-form narration for when the card is woken during the night.
    /// </summary>
    /// <remarks>It's recommended to make this the same the sound narration.</remarks>
    public string TextNarration { get; set; }

    /// <summary>
    /// The path to an image shown when you get the card as your role.
    /// </summary>
    /// 
    public string ImagePath { get; set; }

    /// <summary>
    /// The <see cref="Action"/> of this card.
    /// </summary>
    public IAction Action { get; set; }
}