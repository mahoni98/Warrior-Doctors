using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public static SceneControl instance = null;

    [HideInInspector]
    public int levelIndex;
    public bool firstLogin;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
        firstLogin = true;
    }

}
