using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    public static Canvas instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
