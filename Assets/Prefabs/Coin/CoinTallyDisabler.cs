using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTallyDisabler : MonoBehaviour
{

    public GameObject coinTally;

    void Start()
    {
		coinTally.SetActive(true);
    }

    void Update()
    {
        if(Player.self.isAlive){
			coinTally.SetActive(true);
		} else coinTally.SetActive(false);
    }
}
