using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {
    const string player = "Player";

    public GameObject Canvas;
    public float DelayTime;

    [HideInInspector] public bool HitKeyByPlayer;

    PlayerController _playerController;

    private void Start() {
        _playerController = GameObject.FindGameObjectWithTag(player).GetComponent<PlayerController>();
        Canvas.SetActive(false);
        DelayTime = 0.5f;
    }

    private void Update()
    {
        if (HitKeyByPlayer && Input.GetKey(KeyCode.E)) {
            _playerController.isHaveKey = CollectKey();
            _playerController.sumOfKeys += 1;
        }
    }

    public bool CollectKey() {
        gameObject.SetActive(false);
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            HitKeyByPlayer = true;
            ShowInteractPanel(Canvas);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            HitKeyByPlayer = false;
            if (gameObject.activeSelf) {
                StartCoroutine(Deactivate(Canvas, DelayTime));
            }
        }
    }

    private void ShowInteractPanel(GameObject gameObject) {
        gameObject.SetActive(true);
    }

    private IEnumerator Deactivate(GameObject gameObject, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
}
