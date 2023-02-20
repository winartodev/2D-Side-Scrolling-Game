using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour {
    const string player = "Player";

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
