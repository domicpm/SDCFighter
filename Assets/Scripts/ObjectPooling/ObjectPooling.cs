using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hinweis: Dieses Snippet stammt ursprünglich aus meinem Unity-Projekt,
/// wurde aber neutralisiert, um unabhängig von Game-Entwicklung verständlich zu sein.
/// </summary>
public class ObjectPooling : MonoBehaviour
{
    private Dictionary<GameObject, Queue<GameObject>> pool = new Dictionary<GameObject, Queue<GameObject>>();

    public GameObject resourceTypeA; // Auto Attack
    public GameObject resourceTypeB; // Spell 1   
    void Start()
    {
        // Erstelle Pools für verschiedene Objekttypen
        CreatePool(resourceTypeA, 30);
        CreatePool(resourceTypeB, 20);
    }
    // Erstellt einen Pool wiederverwendbarer Objekte
    private void CreatePool(GameObject prefab, int sizeOfPool)
    {
        Queue<GameObject> objectQueue = new Queue<GameObject>();
        for (int i = 0; i < sizeOfPool; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.GetComponent<PrefabPooling>().SetPrefab(prefab);
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }
        pool[prefab] = objectQueue;
    }
    // Aktiviert ein Objekt aus dem Pool an einer bestimmten Position und Rotation
    public GameObject ActivateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Queue<GameObject> newPool = pool[prefab];
        if (newPool.Count > 0)
        {
            GameObject obj = newPool.Dequeue();
            var bullet = obj.GetComponent<Bullets>();
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            if (bullet != null)
            {
                bullet.dmgApplied = false; // Schaden Reset für Bullet-Logik
            }
            return obj;
        }
        return null;
    }
    // Deaktiviert ein Objekt und legt es zurück in den Pool
    public void RemoveObject(GameObject obj)
    {
        obj.SetActive(false);
        pool[obj.GetComponent<PrefabPooling>().prefab].Enqueue(obj);
    }
    // Objekt wird nach einer kurzen Aktion (Explosionseffekt) wieder in den Pool zurückgelegt (Coroutines sind Unity-spezifisch, ähnlich zu async/await)
    public IEnumerator ReturnToPoolDelayed(GameObject obj, float delay, Vector3 resetScale)
    {
        if (obj == null) yield break;

        obj.transform.localScale = resetScale * 5.5f; 
        yield return new WaitForSeconds(delay);
        obj.transform.localScale = resetScale;
        RemoveObject(obj); 
    }
}
