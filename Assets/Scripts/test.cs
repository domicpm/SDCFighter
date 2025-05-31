using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
        public Button myButton;
        public GameLogic GameLogic;

        // Start is called before the first frame update
        void Start()
        {
            myButton.onClick.AddListener(OnButtonClick);
        
            if (myButton != null)
            {
                Debug.Log("myButton ist zugewiesen: " + myButton.name);
                myButton.onClick.AddListener(OnButtonClick);
            }
            else
            {
                Debug.LogError("FEHLER: myButton ist NICHT zugewiesen!");
            }
        }


    

    // Update is called once per frame
    void Update()
        {

        }
        void OnButtonClick()
        {
        Debug.Log("KLIIICK");
        }
    }



