using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorController : MonoBehaviour {
    const string player = "Player";
    const string findKey = "To open the door find the key frist";

    int _minKey = 3;
    bool _hitDoorByPlayer;

    public GameObject DoorLeaf;

    PlayerController _playerController;

    private void Start() {
        _playerController = GameObject.FindGameObjectWithTag(player).GetComponent<PlayerController>();
        _hitDoorByPlayer = false;
        _playerController.canvas.SetActive(false);
    }

    private void Update() {
        if (_playerController.sumOfKeys >= 3) {
            _playerController.isHaveKey = true;
        }

        if (_hitDoorByPlayer && Input.GetKey(KeyCode.E)) {
            Open(_playerController.isHaveKey, _playerController.sumOfKeys);
        }
    }

    private void Open(bool isHaveKey, int sumOfKey) {
        if (isHaveKey && sumOfKey >= _minKey) {
            var collider = gameObject.GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
            DoorLeaf.SetActive(false);
        } else {
            if (sumOfKey == 0) {
                _playerController.textMeshProUGUI.SetText(findKey);
                StartCoroutine(Deactivate(_playerController.canvas, 1.2f));
            } else {
                _playerController.textMeshProUGUI.SetText("find the key again <b>" + (_minKey - sumOfKey) + "</b> key left");
                StartCoroutine(Deactivate(_playerController.canvas, 1.2f));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _hitDoorByPlayer = true;
            _playerController.textMeshProUGUI.SetText("Press <b>E</b> to open the door");
            _playerController.canvas.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _hitDoorByPlayer = false;
            StartCoroutine(Deactivate(_playerController.canvas, 0.7f));
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _playerController.textMeshProUGUI.SetText("Press <b>E</b> to the next level");
            _playerController.canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            StartCoroutine(Deactivate(_playerController.canvas, 0.7f));
        }
    }

    private IEnumerator Deactivate(GameObject gameObject, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
}
