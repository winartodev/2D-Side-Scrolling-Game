using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour {
    const string ground = "Ground";

    [HideInInspector] public bool isHaveKey;
    [HideInInspector] public int sumOfKeys;

    public float MoveSpeed;
    public float JumpForce;

    float _inputHoizontal;
    float _inputVertical;
    bool _isFlipRight;
    bool _isJumping;

    public GameObject canvas;
    public TextMeshProUGUI textMeshProUGUI;

    Rigidbody2D _rigidbody2D;
    Inventory _inventory;
    InventoryController _inventoryController;

    // Start is called before the first frame update
    void Awake() {
        _isFlipRight = true;
        _isJumping = false;
        isHaveKey = false;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inventoryController = GameObject.FindGameObjectWithTag("InventoryController").GetComponent<InventoryController>();

        _inventory = new Inventory();
        _inventoryController.SetInventory(_inventory);

        if (MoveSpeed == 0 || JumpForce == 0) {
            MoveSpeed = 2f;
            JumpForce = 25f;
        }
    }

    // Update is called once per frame
    void Update() {
        _inputHoizontal = Input.GetAxisRaw("Horizontal");
        _inputVertical = Input.GetAxisRaw("Vertical");

        if (_inputHoizontal > 0 && !_isFlipRight) {
            FlipPlayer();
        }

        if (_inputHoizontal < 0 && _isFlipRight) {
            FlipPlayer();
        }
    }

    private void FixedUpdate() {
        MovingPlayer();
    }

    // MovingPlayer is function to make player can move
    void MovingPlayer() {
        if (_inputHoizontal > 0.1f || _inputHoizontal < -0.1f) {
            _rigidbody2D.AddForce(new Vector2(_inputHoizontal * MoveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (_inputVertical > 0.1 && !_isJumping) {
            _rigidbody2D.AddForce(new Vector2(0f, _inputVertical * JumpForce), ForceMode2D.Impulse);
        }
    }

    // FlipPlayer is function to flip the player using scale from player
    void FlipPlayer() {
        _isFlipRight = !_isFlipRight;
        Vector2 currentScale = transform.localScale;

        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    public void AddItem(GameObject item) {
        if (_inventory.ItemList.Count < 20) {
            if (item.TryGetComponent<ItemWorld>(out var itemWorld)) {
                _inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(ground)) {
            _isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(ground)) {
            _isJumping = true;
        }
    }
}
