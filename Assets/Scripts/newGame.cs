using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class newGame : MonoBehaviour
{
    public Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);

    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void OnButtonClick()
    {
        Debug.Log("Button Clicked!");
        SceneManager.LoadScene("SampleScene");
    }
}
