using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    const string player = "Player";
    const string scoreUI = "ScoreUI";

    readonly int _amount = 1;

    ScoreController _scoreController;

    private void Start()
    {
        _scoreController = GameObject.FindGameObjectWithTag(scoreUI).GetComponent<ScoreController>();
        _scoreController.coinsText.SetText("Coins : " + _scoreController.amountCoin);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(player)) {
            _scoreController.amountCoin += _amount;
            _scoreController.coinsText.SetText("Coins : " + _scoreController.amountCoin);
            print(_scoreController.amountCoin);
            Destroy(gameObject);
        }
    }
}
