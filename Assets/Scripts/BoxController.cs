using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class BoxController : MonoBehaviour {
    const string player = "Player";
    const string boxIsEmpty = "Box is empty";
    const string boxAlreadyOpened = "Box already opened";

    bool _hitBoxByPlayer;
    bool _boxOpen;

    [SerializeField] private List<GameObject> _items;

    public GameObject Spawner;

    Animator _boxAnimation;
    PlayerController _playerController;

    private void Start() {
        _playerController = GameObject.FindGameObjectWithTag(player).GetComponent<PlayerController>();
        _boxAnimation = GetComponent<Animator>();
        _hitBoxByPlayer = false;
        _boxOpen = false;
    }

    private void Update()
    {
        if (!_boxOpen && _hitBoxByPlayer && Input.GetKeyDown(KeyCode.E)) {
            Open();
        }
    }

    public void Open() {
        _boxOpen = true;
        _hitBoxByPlayer = false;

        if (_boxAnimation.GetBool("isOpen")) {
            _playerController.canvas.SetActive(true);
            _playerController.textMeshProUGUI.SetText(boxAlreadyOpened);
            StartCoroutine(Deactivate(_playerController.canvas, 1f));
            return;
        }

        _boxAnimation.SetBool("isOpen", true);
        if (_items.Count > 0) {
            foreach (GameObject item in _items) {
                if (item == null) {
                    _playerController.canvas.SetActive(true);
                    _playerController.textMeshProUGUI.SetText(boxIsEmpty);
                    StartCoroutine(Deactivate(_playerController.canvas, 1f));
                    _boxOpen = true;
                    return;
                };
                Instantiate(item, Spawner.transform.position, Spawner.transform.rotation);
            }
            _items.Clear();
            _boxOpen = true;
            return;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
         if (collision.gameObject.CompareTag(player) && !_boxOpen) {
            _hitBoxByPlayer = true;
            _playerController.textMeshProUGUI.SetText("Press <b>E</b> to open the box");
            _playerController.canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _hitBoxByPlayer = false;
            StartCoroutine(Deactivate(_playerController.canvas, 0.5f));
        }
    }

    private IEnumerator Deactivate(GameObject gameObject, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
}
