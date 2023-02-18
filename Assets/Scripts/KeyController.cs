using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {
    const string player = "Player";

    float DelayTime;
    bool _hitKeyByPlayer;

    PlayerController _playerController;

    private void Start() {
        _playerController = GameObject.FindGameObjectWithTag(player).GetComponent<PlayerController>();
        DelayTime = 0.5f;
    }

    private void Update() {
        if (_hitKeyByPlayer && Input.GetKeyDown(KeyCode.E)) {
             CollectKey();
        }
    }

    public void CollectKey() {
        _playerController.AddItem(gameObject);
        _playerController.sumOfKeys += 1;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _hitKeyByPlayer = true;
            _playerController.textMeshProUGUI.SetText("Press <b>E</b> to collect the key");
            ShowInteractPanel(_playerController.canvas);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _hitKeyByPlayer = false;
            if (gameObject.activeSelf) {
                StartCoroutine(Deactivate(_playerController.canvas, DelayTime));
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
