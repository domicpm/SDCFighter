using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour
{
    public Button myButton;
    public Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myButton.gameObject.SetActive(false);
        myText.gameObject.SetActive(false);
        myButton.onClick.AddListener(OnButtonClick);

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnButtonClick()
    {
        Time.timeScale = 1f;
        myButton.gameObject.SetActive(false);
        myText.gameObject.SetActive(false);
    }
}
