using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI currentStateDisplay;
    [SerializeField] TextMeshProUGUI healthDisplay;

    // Update is called once per frame
    void Update()
    {
        SetCurrentStateDisplay(PlayerController.Instance.RetrieveCurrentState());
        SetHealthDisplay(PlayerController.Instance.RetrieveHealthStat());
    }

    public void SetCurrentStateDisplay(IPlayerState currentState)
    {
        currentStateDisplay.text = $"Current State: {currentState}";
    }

    public void SetHealthDisplay(int health)
    {
        healthDisplay.text = $"Health: {health}";
    }
}
