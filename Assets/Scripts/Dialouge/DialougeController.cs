using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeController : MonoBehaviour {
    const string player = "Player";

    public Dialouge dialouge;
    public GameObject canvas;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI dialougeText;
    public TextMeshProUGUI instructionText;

    bool _hitByPlayer;
    bool _startDialouge;
    float _delayTime;

    Queue<string> _sentences;
    PlayerController _playerController;

    // Start is called before the first frame update
    void Start() {
        _sentences = new Queue<string>();
        _playerController = GameObject.FindGameObjectWithTag(player).GetComponent<PlayerController>();

        _hitByPlayer = false;
        _startDialouge = true;

        _delayTime = 0.5f;
    }

    private void Update()
    {
        if (_hitByPlayer && Input.GetKeyDown(KeyCode.E)) {
            StartDialouge();
        }

        if (_startDialouge && Input.GetKeyDown(KeyCode.Space)) {
            DisplayNextSentence();
        }
    }

    public void StartDialouge() {
        canvas.SetActive(true);
        playerNameText.SetText(dialouge.name);
        _sentences.Clear();

        foreach (string sentence in dialouge.sentences) {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (_sentences.Count >= 1) {
            instructionText.SetText("press spacebar >>");
        }

        if (_sentences.Count == 0) {
            EndDialouge();
            return;
        }

        string sentence = _sentences.Dequeue();
        dialougeText.SetText(sentence);
    }

    void EndDialouge() {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(player)) {
            _hitByPlayer = true;
            _playerController.textMeshProUGUI.SetText("Press <b>E</b> to interact with " + dialouge.name);
            ShowInteractPanel(_playerController.canvas);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player)) {
            _hitByPlayer = false;
            if (gameObject.activeSelf) {
                StartCoroutine(Deactivate(_playerController.canvas, _delayTime));
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
