using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RetryGame : MonoBehaviour
{
    public Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        myButton.gameObject.SetActive(false);
        myButton.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnButtonClick()
    {
        SceneManager.LoadScene("InputDevice");      
    }
}
