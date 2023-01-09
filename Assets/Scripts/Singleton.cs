using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected virtual void Awake()
    {
        if (_instance == null)
            _instance = this as T;
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var instances = FindObjectsOfType<T>();
                for (int i = 0; i < instances.Length; i++)
                    if (instances[i] == _instance)
                        return _instance = instances[i];

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
