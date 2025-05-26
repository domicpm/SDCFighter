using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PotUI : MonoBehaviour
{
    public Text potText;
    public PlayerMovement pl;
    // public Transform DamagePopUp;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        potText.text = pl.potamount.ToString();
        
    }
}
