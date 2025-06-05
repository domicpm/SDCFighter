using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // dmgtxt.spawnDmg(damage);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            StartCoroutine(MySequence());
        }
    }
    IEnumerator MySequence()
    {
        gameObject.transform.localScale *= 6f;

        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);

    }
}
