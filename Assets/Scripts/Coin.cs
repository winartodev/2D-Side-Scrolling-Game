using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    const string player = "Player";
    int _amount = 1;

    ScoreController _scoreController;

    private void Start()
    {
        _scoreController = new ScoreController();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(player)) {
            _scoreController.amountCoin += _amount;
            print(_scoreController.amountCoin);
            Destroy(gameObject);
        }
    }
}
