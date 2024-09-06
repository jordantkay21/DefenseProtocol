
public abstract class PlayerState 
{
    /// <summary>
    /// Called when entering this state. Use to initialize state-specific variables, set up behavior, or trigger animations.
    /// </summary>
    /// <param name="playerController"></param>
    public abstract void EnterState(PlayerController playerController);
    /// <summary>
    /// Called every frame while in this state. Use for logic that needs to be continuously checked or updated, such as movement or ongoing effects.
    /// </summary>
    /// <param name="playerController"></param>
    public abstract void UpdateState(PlayerController playerController);
    /// <summary>
    /// Called when exiting this state. Use to clean up or reset variables, stop animations, or handle transitions.
    /// </summary>
    /// <param name="playerController"></param>
    public abstract void ExitState(PlayerController playerController);

}
