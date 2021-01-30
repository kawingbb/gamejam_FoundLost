using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    private static ScenesManager _instance;

    public static ScenesManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<ScenesManager>();
            }
            return _instance;
        }    
    }

    public void GoToStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
    public void GoToGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
    public void GoToEndScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
