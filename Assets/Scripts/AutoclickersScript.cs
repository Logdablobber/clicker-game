using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class AutoclickersScript : MonoBehaviour
{

    ClickManagerScript clickManager;

    AutoclickerSpawnerScript autoclickerSpawner;

    public Autoclicker[] autoclickers;

    int fixedUpdateIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        clickManager = GameObject.FindObjectOfType<ClickManagerScript>();

        autoclickerSpawner = GameObject.FindObjectOfType<AutoclickerSpawnerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        int updates_per_second = (int)math.floor(1 / Time.fixedDeltaTime);

        foreach (Autoclicker autoclicker in autoclickers)
        {

            if (autoclicker.secondsPerClick / autoclicker.amount < Time.fixedDeltaTime && fixedUpdateIndex % updates_per_second == 0)
            {

                clickManager.Click(autoclicker.amount, autoclicker.amountPerClick * Time.fixedDeltaTime);

                Debug.Log("Clicked " + autoclicker.amount * autoclicker.amountPerClick * Time.fixedDeltaTime + " Times After " + Time.fixedDeltaTime + "seconds");

            }

            else if (fixedUpdateIndex % math.floor(updates_per_second * autoclicker.secondsPerClick / autoclicker.amount) == 0)
            {

                clickManager.Click(1, autoclicker.amountPerClick);

                Debug.Log("Clicked after " + autoclicker.secondsPerClick / autoclicker.amount + " seconds");

            }

        }

        fixedUpdateIndex++;
    }

    public void BuyT1Autoclicker(int amount)
    {
        bool success = BuyAutoclickers(0, amount);

        if (success)
        {
            for (int i = 0; i < amount; i++)
            {
                autoclickerSpawner.CreateNewAutoclickerObject(autoclickers[0].amount - amount + i);
            }
        }

        UpdateBuyText(0);
    }

    int GetTotalCost(int tier, int amount)
    {

        float compounding_cost_max = autoclickers[tier].baseCost * math.pow(1 + autoclickers[tier].costMultiplier, autoclickers[tier].amount + amount - 1);

        float compounding_cost_min = autoclickers[tier].baseCost * math.pow(1 + autoclickers[tier].costMultiplier, autoclickers[tier].amount - 1);

        return (int)math.max(autoclickers[tier].baseCost*amount, math.floor(compounding_cost_max - compounding_cost_min));

    }

    public bool BuyAutoclickers(int tier, int amount_to_buy)
    {

        int total_cost = GetTotalCost(tier, amount_to_buy);

        if (total_cost > clickManager.click_currency)
        {

            Debug.Log("Could not afford " + autoclickers[tier].name + " (Total cost: " + total_cost + ")");
            return false;

        }

        autoclickers[tier].amount += amount_to_buy;

        clickManager.ModifyClickCurrency(-total_cost);

        clickManager.UpdateCPS();

        return true;

    }

    public float GetTotalCPS()
    {
        float CPS = 0;

        foreach (Autoclicker autoclicker in autoclickers)
        {

            CPS += autoclicker.amount * (1 / autoclicker.secondsPerClick) * autoclicker.amountPerClick;

        }

        return CPS;
    }

    void UpdateBuyText(int tier)
    {
        autoclickers[tier].buyText.text = "Buy T1\nCost: " + GetTotalCost(tier, 1);


        autoclickers[tier].buy2Text.text = "Buy 2 T1\nCost: " + GetTotalCost(tier, 2);
    }
}

[System.Serializable]
public struct Autoclicker
{

    public string name;
    public int amount;

    public float secondsPerClick;
    public int amountPerClick;

    public int baseCost;
    [Range(0f, 1f)]
    public float costMultiplier;

    public TextMeshProUGUI buyText;
    public TextMeshProUGUI buy2Text;

}

