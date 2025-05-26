using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIDamage : MonoBehaviour
{
    public Text Damage;
    public Text Damage1;
    public Enemy hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    public void printDmg(float dmg)
    {           
        Damage1.text = "" + dmg;

        if (hit.isHit == true)
        {
            Damage.gameObject.SetActive(true);
        }
    }
}
