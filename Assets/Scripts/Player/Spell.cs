using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectPooling objectPooling;
    private Vector3 originalScale;
    public bool isConsumable;
    private void Awake()
    {
        objectPooling = FindObjectOfType<ObjectPooling>();
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectPooling == null) return;

        if ((collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Dummy")) && !isConsumable)
        {
            StartCoroutine(ReturnAfterDelay());
            
        }
        else if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("S-Tier-Enemy")) && !isConsumable)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            //StartCoroutine(MySequence()); // wird aktuell nicht gebraucht, da Logik in ObjectPooling implementiert wird
            objectPooling.StartCoroutine(objectPooling.ReturnToPoolDelayed(gameObject, 0.05f, originalScale));
        }
    }

    // wird aktuell nicht gebraucht, da Logik in ObjectPooling implementiert wird
    IEnumerator MySequence()
    {
        transform.localScale = originalScale * 5.5f;

        yield return new WaitForSeconds(0.05f);

        transform.localScale = originalScale;

        objectPooling.RemoveObject(gameObject);
    }
    IEnumerator ReturnAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        objectPooling.RemoveObject(gameObject);
        transform.localScale = originalScale;
    }
}
