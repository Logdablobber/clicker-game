using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    ClickManagerScript clickManager;

    void Start()
    {
        
        clickManager = GameObject.FindObjectOfType<ClickManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
        

    }

    

    private void OnMouseDown()
    {

        clickManager.Click(1);

    }
}
