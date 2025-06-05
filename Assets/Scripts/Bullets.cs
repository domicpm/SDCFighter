using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{
    public float mindamage = 20;
    public float maxdamage = 125;
    public float mindamageSpell = 75;
    public float maxdamageSpell = 250;
    public float speed = 6f;
    public float damage;
    public float damageSpell;

    public PlayerMovement player;
    private Vector3 originalScale;
    private CooldownUI cdUI;
    public GameObject b;
    public projectile p;
    public Spell spell;

    // public DamageText dmgtxt;
    // public UIDamage uidmg;
    // public UIDamage uidmg1;

    private void Start()
    {
        cdUI = FindObjectOfType<CooldownUI>();
        UpdateDamage();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
            return;

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

    public float getDmgSpell()
    {
        return damageSpell;
    }
    public void UpdateDamage()
    {
        damage = Mathf.RoundToInt(Random.Range(mindamage, maxdamage));
    }
    public void UpdateDamageSpell()
    {
        damageSpell = Mathf.RoundToInt(Random.Range(mindamageSpell, maxdamageSpell));
    }
    public void shoot()
    {
        if (PauseManager.Instance.IsPaused) // wenn Pause gedrückt, werden keine weiteren Bullets gespawnt
            return;

        var bullet = Instantiate(b, p.transform.position, Quaternion.identity);
        UpdateDamage();
        Vector3 bulletDir = player.bp.transform.up;
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletDir * speed, ForceMode2D.Impulse);
        bullet.transform.localScale = originalScale;
    }
    public void shootLeft()
    {
        cdUI.ResetCooldown();
        if (PauseManager.Instance.IsPaused) // wenn Pause gedrückt, werden keine weiteren Bullets gespawnt
            return;

        var bullet = Instantiate(spell, p.transform.position, Quaternion.identity);
        UpdateDamageSpell();
        Vector3 bulletDir = player.bp.transform.up;
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletDir * speed, ForceMode2D.Impulse);
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
    }
}
