/// <summary>
/// Stores and manages the player's core attributes and state flags.
/// Provides methods for adjusting player data.
/// </summary>
public class PlayerModel 
{
    #region Variables
    //Player Stats
    public float health;
    public float walkSpeed;
    public float sprintSpeed;

    //State Management
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
        health = 100f;
        walkSpeed = 5f;
        sprintSpeed = 10f;
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
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //handle player death
        }
    }
    #endregion
}
