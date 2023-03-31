using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    
    public void OpenScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

}
