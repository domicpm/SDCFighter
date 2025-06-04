using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputDevice : MonoBehaviour
{
    public Button ButtonController;
    public Button ButtonMouse;
    public static bool mouse = false;
    // Start is called before the first frame update
    void Start()
    {
        ButtonController.onClick.AddListener(OnButtonClick); 
        ButtonMouse.onClick.AddListener(OnButtonClick1);


    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnButtonClick()
    {
        mouse = false;
        Debug.Log("MOUSE FALSCH");
        SceneManager.LoadScene("SampleScene");

    }
    void OnButtonClick1()
    {
        mouse = true;
        Debug.Log("MOUSE WAHR");
        SceneManager.LoadScene("SampleScene");

    }
}
