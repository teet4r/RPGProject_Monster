using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected void Awake()
    {
        if (_instance == null)
            _instance = this as T;
        else if (_instance != this)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    var newObj = new GameObject();
                    _instance = newObj.AddComponent<T>();
                    newObj.name = typeof(T).ToString();
                }
            }
            return _instance;
        }
    }
    static T _instance = null;
}
