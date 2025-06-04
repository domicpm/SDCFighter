using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugJoySticks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Horizontal (X axis): " + Input.GetAxis("Horizontal"));
        Debug.Log("Vertical (Y axis): " + Input.GetAxis("Vertical"));
        Debug.Log("RightStickHorizontal (4th axis): " + Input.GetAxis("RightStickHorizontal"));
        Debug.Log("RightStickVertical (5th axis): " + Input.GetAxis("RightStickVertical"));
    }
}
