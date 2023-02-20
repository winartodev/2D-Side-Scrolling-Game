using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorController : MonoBehaviour {
    const string player = "Player";
    const string findKey = "To open the door find the key frist";

    public TextMeshProUGUI textMeshProUGUI;
    public GameObject Canvas;
    public GameObject DoorLeaf;

    int _minKey = 3;
    bool _hitDoorByPlayer;

    PlayerController _playerController;

    private void Start() {
        _playerController = GameObject.FindGameObjectWithTag(player).GetComponent<PlayerController>();
        _hitDoorByPlayer = false;
        Canvas.SetActive(false);
    }

    private void Update() {
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
                textMeshProUGUI.SetText(findKey);
                StartCoroutine(Deactivate(Canvas, 1.2f));
            } else {
                textMeshProUGUI.SetText("find the key again <b>" + (_minKey - sumOfKey) + "</b> key left");
                StartCoroutine(Deactivate(Canvas, 1.2f));
            }
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _hitDoorByPlayer = true;
            textMeshProUGUI.SetText("Press <b>E</b> to open the door");
            Canvas.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _hitDoorByPlayer = false;
            StartCoroutine(Deactivate(Canvas, 0.7f));
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            textMeshProUGUI.SetText("Press <b>E</b> to the next level");
            Canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            StartCoroutine(Deactivate(Canvas, 0.7f));
        }
    }

    private IEnumerator Deactivate(GameObject gameObject, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
}
