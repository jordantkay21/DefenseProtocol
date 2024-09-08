/// <summary>
/// Stores and manages the player's core attributes and state flags.
/// Provides methods for adjusting player data.
/// </summary>
public class PlayerModel 
{
    #region Variables
    //Player Stats
    public int health;
    public int runMultiplier;

    //State Management
    public bool isGrounded;
    public bool isSprinting;
    public bool isCrouching;
    public bool isJumping;
    public bool isAttacking;
#endregion

    #region Constructors
    /// <summary>
    /// Initilize a new Player with default values
    /// </summary>
    public PlayerModel()
    {
        health = 100;
        runMultiplier = 2;
        isGrounded = true;
        isSprinting = false;
        isCrouching = false;
        isJumping = false;
        isAttacking = false;
    }
    #endregion

    #region Data Adjustments
    /// <summary>
    /// Deplete players health by damage amount
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //handle player death
        }
    }
    #endregion
}
