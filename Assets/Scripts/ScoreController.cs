using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int amountCoin;
    public TextMeshProUGUI coinsText;

    // Update is called once per frame
    void Update()
    {
        coinsText.SetText("Coins :" + amountCoin);
    }
}
