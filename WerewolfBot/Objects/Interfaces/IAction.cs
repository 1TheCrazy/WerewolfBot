namespace WerewolfBot.Objects.Interfaces;

/// <summary>
/// The contract for an implementation of a Card-Action.
/// </summary>
interface IAction
{
    /// <summary>
    /// The method used for executing an action of a card.
    /// </summary>
    /// <param name="game">The <see cref="Game"/> that this Action should be executed on.</param>
    public void Execute(Game game);
}