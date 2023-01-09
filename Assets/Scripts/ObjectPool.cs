using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;
    public int PoolSize
    {
        get { return _prefabs.Length; }
    }
    [SerializeField] GameObject[] _prefabs;
    [Tooltip("Make prefab's clones in advance.")]
    [SerializeField] int _initialCount = 0;
    Dictionary<string, GameObject> _dictionary = new Dictionary<string, GameObject>();
    Dictionary<string, Queue<GameObject>> _qDictionary = new Dictionary<string, Queue<GameObject>>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        for (int i = 0; i < _prefabs.Length; i++)
        {
            if (_prefabs[i] == null)
                throw new System.Exception("Object pool has null game object(s).");

            var prefabName = _prefabs[i].name;
            _dictionary.Add(prefabName, _prefabs[i]);
            _qDictionary.Add(prefabName, new Queue<GameObject>());
            for (int j = 0; j < _initialCount; j++)
            {
                var clone = Instantiate(_prefabs[i]);
                clone.name = prefabName;
                clone.SetActive(false);
                _qDictionary[prefabName].Enqueue(clone);
            }
        }
    }

    public GameObject Get(int prefabIndex)
    {
        if (prefabIndex < 0 || prefabIndex >= _prefabs.Length)
        {
            Debug.LogError("This index of prefab doesn't exist.");
            return null;
        }

        if (_qDictionary[_prefabs[prefabIndex].name].Count == 0)
        {
            var clone = Instantiate(_prefabs[prefabIndex]);
            clone.name = _prefabs[prefabIndex].name;
            clone.SetActive(false);
            return clone;
        }
        var obj = _qDictionary[_prefabs[prefabIndex].name].Dequeue();
        return obj;
    }
    public GameObject Get(string prefabName)
    {
        if (!_dictionary.ContainsKey(prefabName))
        {
            Debug.LogError("This name of prefab doesn't exist.");
            return null;
        }

        if (_qDictionary[prefabName].Count == 0)
        {
            var clone = Instantiate(_dictionary[prefabName]);
            clone.name = prefabName;
            clone.SetActive(false);
            return clone;
        }
        var obj = _qDictionary[prefabName].Dequeue();
        return obj;
    }
    public GameObject GetRandom()
    {
        return Get(Random.Range(0, _prefabs.Length));
    }
    /// <summary>
    /// Return a list that is 'count' size of random instantiated prefabs.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<GameObject> GetRandoms(int count)
    {
        List<GameObject> gameObjects = new List<GameObject>();
        for (int i = 0; i < count; i++)
            gameObjects.Add(GetRandom());
        return gameObjects;
    }
    /// <summary>
    /// Return all instantiated prefabs.
    /// </summary>
    /// <returns>Array Type</returns>
    public GameObject[] Gets()
    {
        GameObject[] gameObjects = new GameObject[_prefabs.Length];
        for (int i = 0; i < _prefabs.Length; i++)
            gameObjects[i] = Get(_prefabs[i].name);
        return gameObjects;
    }
    /// <summary>
    /// Return doesn't have null objects.
    /// </summary>
    /// <param name="prefabNames"></param>
    /// <returns>List Type</returns>
    public List<GameObject> Gets(int[] prefabIndexes)
    {
        List<GameObject> gameObjects = new List<GameObject>();
        for (int i = 0; i < prefabIndexes.Length; i++)
        {
            var obj = Get(prefabIndexes[i]);
            if (obj != null) gameObjects.Add(obj);
        }
        return gameObjects;
    }
    public List<GameObject> Gets(string[] prefabNames)
    {
        List<GameObject> gameObjects = new List<GameObject>();
        for (int i = 0; i < prefabNames.Length; i++)
        {
            var obj = Get(prefabNames[i]);
            if (obj != null) gameObjects.Add(obj);
        }
        return gameObjects;
    }
    public void Put(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("This object is null.");
            Destroy(obj);
            return;
        }
        else if (!_dictionary.ContainsKey(obj.name))
        {
            Debug.LogError("Object pool doesn't contain this object's name.");
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        _qDictionary[obj.name].Enqueue(obj);
    }
    public void Clear()
    {
        foreach (var pair in _qDictionary)
            while (pair.Value.Count != 0)
                Destroy(pair.Value.Dequeue());
    }
}
