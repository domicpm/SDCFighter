using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{
    public float mindamage = 20;
    public float maxdamage = 125;
    public float speed = 6f;
    public float damage;

    public PlayerMovement player;
    private Vector3 originalScale;

    public GameObject b;
    public weapon stab;
    public projectile p;

    // public DamageText dmgtxt;
    // public UIDamage uidmg;
    // public UIDamage uidmg1;

    private void Start()
    {
        UpdateDamage();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
            return;

        stab.transform.localRotation = Quaternion.Euler(0, 0, player.angle);
        b.transform.localRotation = Quaternion.Euler(0, 0, player.angle);
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
            Destroy(gameObject);
        }
    }

    public float getDmg()
    {
        return damage;
    }

    public void UpdateDamage()
    {
        damage = Mathf.RoundToInt(Random.Range(mindamage, maxdamage));
    }

    public void shoot()
    {
        if (PauseManager.Instance.IsPaused) // wenn Pause gedrückt, werden keine weiteren Bullets gespawnt
            return;

        var bullet = Instantiate(b, p.transform.position, Quaternion.identity);
        UpdateDamage();
        Vector3 bulletDir = transform.up;
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletDir * speed, ForceMode2D.Impulse);
        bullet.transform.localScale = originalScale;
    }
}
