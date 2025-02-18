namespace WerewolfBot.Objects.Interfaces;

interface IGamePhase
{
    /// <summary>
    /// Starts a game-phase
    /// </summary>
    public void Start();

    /// <summary>
    /// Ends a game-phase
    /// </summary>
    public void End();

    /// <summary>
    /// The queue for Players that should be killed on the end of the game-phase.
    /// </summary>
    public List<Player> KillQueue { get; set; }
}