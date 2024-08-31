using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton instance
    public static GameManager Instance { get; private set; }
    
    //The single instance of EventManager for weapons
    public EventManager WeaponEvents{ get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist the GameManager across scenes

            //Initialize the EventManager
            WeaponEvents = new EventManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
