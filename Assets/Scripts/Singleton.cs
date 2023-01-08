using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public T Instance
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
                    DontDestroyOnLoad(gameObject);
                }
            }
            return _instance;
        }
    }
    static T _instance = null;
}
