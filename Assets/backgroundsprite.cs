using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundsprite : MonoBehaviour
{
    public static backgroundsprite Instance;


    void Awake()
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
