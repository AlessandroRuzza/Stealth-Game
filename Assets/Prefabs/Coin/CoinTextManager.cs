using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{

    public GameObject coinTally;
    public int coinsCollected;
    public int totalCoins;

    TextMeshProUGUI coinTallyText;


    void Start()
    {
        coinsCollected = 0;
        totalCoins = Player.self.totCoins();
        coinTallyText = coinTally.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
        coinsCollected = Player.self.GetCoins();
        coinTallyText.text = coinsCollected.ToString() + "/" + totalCoins.ToString();
    }
}
