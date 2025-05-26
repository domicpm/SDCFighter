using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCode : MonoBehaviour
{
    public static SingletonCode Instance { get; private set; }
    public GameObject referenceObject;

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

    public void UpdateReference(GameObject newReference)
    {
        referenceObject = newReference;
    }

}
