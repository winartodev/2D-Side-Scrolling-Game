using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxController : MonoBehaviour {
    const string player = "Player";
    const string boxIsEmpty = "Box is empty";
    const string boxAlreadyOpened = "Box already opened";

    [SerializeField] private List<GameObject> Items;

    public TextMeshProUGUI TextMeshProUGUI;
    public GameObject Canvas;

    bool _hitBoxByPlayer;

    Animator _boxAnimation;
    PlayerController _playerController;

    private void Start() {
        _playerController = GameObject.FindGameObjectWithTag(player).GetComponent<PlayerController>();
        _boxAnimation = GetComponent<Animator>();
        _hitBoxByPlayer = false;
        Canvas.SetActive(false);
    }

    private void Update()
    {
        if (_hitBoxByPlayer && Input.GetKey(KeyCode.E)) {
            Open();
        }
    }

    public void Open() {
        _hitBoxByPlayer = false;

        if (_boxAnimation.GetBool("isOpen")) {
            TextMeshProUGUI.SetText(boxAlreadyOpened);
            Canvas.SetActive(true);
            StartCoroutine(Deactivate(Canvas, 1f));
            return;
        }

        _boxAnimation.SetBool("isOpen", true);
        if (Items.Count > 0) {
            foreach (GameObject item in Items) {
                _playerController.Inventories.Add(item);
            }
            Items.Clear();
            return;
        } else {
            TextMeshProUGUI.SetText(boxIsEmpty);
            Canvas.SetActive(true);
            StartCoroutine(Deactivate(Canvas, 1f));
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        print(gameObject.name);

        if (collision.gameObject.CompareTag(player)) {
            _hitBoxByPlayer = true;
            TextMeshProUGUI.SetText("Press <b>E</b> to open the box");
            Canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _hitBoxByPlayer = false;
            StartCoroutine(Deactivate(Canvas, 0.5f));
        }
    }

    private IEnumerator Deactivate(GameObject gameObject, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
}
