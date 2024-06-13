using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ClickManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI ClickDataText;
    public float click_currency = 0;

    public float clicks_per_second = 0;

    AutoclickersScript autoclickersScript;

    void Start()
    {
        
        autoclickersScript = GameObject.FindObjectOfType<AutoclickersScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateClickDataText()
    {

        if (ClickDataText != null)
        {

            ClickDataText.text = "Clicks: " + math.floor(click_currency) + "\nClicks/Second: " + clicks_per_second;

        }

    }

    public void UpdateCPS()
    {

        clicks_per_second = autoclickersScript.GetTotalCPS();

        UpdateClickDataText();

    }

    public void ModifyClickCurrency(float amount)
    {

        click_currency += amount;

        UpdateClickDataText();

    }

    public void Click(int amount, float amount_per_click = 1f)
    {

        ModifyClickCurrency(amount * amount_per_click);

    }
}
