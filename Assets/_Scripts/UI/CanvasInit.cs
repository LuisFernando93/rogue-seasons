using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInit : MonoBehaviour
{
    public static CanvasInit Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
