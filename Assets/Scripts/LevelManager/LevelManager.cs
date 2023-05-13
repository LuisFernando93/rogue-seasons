using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager: MonoBehaviour
{
    [SerializeField] private AudioClip defaultSummerOST;
    [SerializeField] private AudioClip battleSummerOST;

    public static LevelManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayMusic(defaultSummerOST);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
