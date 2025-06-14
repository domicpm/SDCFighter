using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCircle : MonoBehaviour
{
    public bool inRange = false;
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
        if (collision.CompareTag("Player")){
            setInRange(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            setInRange(false);
        }
    }

    void setInRange(bool iR)
    {
        inRange = iR;
    }
    public bool getInRange()
    {
        return inRange;
    }

}
