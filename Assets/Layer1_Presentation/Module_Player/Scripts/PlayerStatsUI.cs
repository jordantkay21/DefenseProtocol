using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI currentStateDisplay;
    [SerializeField] TextMeshProUGUI healthDisplay;
    [SerializeField] TextMeshProUGUI isGroundedDisplay;

    // Update is called once per frame
    void Update()
    {
        SetCurrentStateDisplay(PlayerController.Instance.RetrieveCurrentState());
        SetHealthDisplay(PlayerController.Instance.RetrieveHealthStat());
        SetIsGrounded(PlayerController.Instance.RetrieveIsGrounded());
    }

    private void SetIsGrounded(bool isGrounded)
    {
        isGroundedDisplay.text = $"IsGrounded: {isGrounded}";
    }

    private void SetCurrentStateDisplay(IPlayerState currentState)
    {
        currentStateDisplay.text = $"Current State: {currentState}";
    }

    private void SetHealthDisplay(int health)
    {
        healthDisplay.text = $"Health: {health}";
    }
}
