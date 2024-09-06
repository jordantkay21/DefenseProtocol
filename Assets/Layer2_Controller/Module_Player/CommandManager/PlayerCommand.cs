
public abstract class PlayerCommand
{
    /// <summary>
    /// Performs the core action or behavior associated with this command. Use to trigger the main logic or functionality that this command is responsible for.
    /// </summary>
    /// <param name="playerModel"></param>
    /// <param name="playerView"></param>
    public abstract void Execute(PlayerModel playerModel, PlayerView playerView);
}
