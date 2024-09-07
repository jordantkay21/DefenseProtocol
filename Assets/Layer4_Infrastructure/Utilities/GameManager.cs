using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton instance
    public static GameManager Instance { get; private set; }

    [Header("Debug Tools")]
    [SerializeField] bool isDebugMode;    
    //This will show a mask field in the Inspector where you can select multiple DebugTags
    [EnumFlags]
    public DebugTag enabledDebugTags;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist the GameManager across scenes

            InitializeDebugUtility();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDebugUtility()
    {
        DebugUtility.ClearTags();

        //Enable selected tags in the DebugUtility
        foreach(DebugTag tag in Enum.GetValues(typeof(DebugTag)))
            if((enabledDebugTags & tag) == tag && tag != DebugTag.None)
            {
                DebugUtility.EnableTag(tag);
            }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        DebugUtility.IsDebugMode = isDebugMode;
        InitializeDebugUtility();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
