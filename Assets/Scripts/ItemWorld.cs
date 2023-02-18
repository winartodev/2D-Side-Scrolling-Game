using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;
using static UnityEditor.Progress;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    const string player = "Player";

    public ItemType itemType;

    bool _hitItem;

    Item _item;
    SpriteRenderer _spriteRenderer;
    PlayerController _playerController;

    private void Start()
    {
        _item = new Item();
        _item.itemType = itemType;

        _hitItem = false;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerController = GameObject.FindGameObjectWithTag(player).GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_hitItem && Input.GetKeyDown(KeyCode.E)) {
            _playerController.AddItem(gameObject);
        }
    }

    public Item GetItem() {
        return _item;
    }

    public void SetItem(Item item) {
        this._item = item;
        _spriteRenderer.sprite = item.GetSprite();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(player) && _item.itemType != ItemType.Key) {
            _hitItem = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(player) && _item.itemType != ItemType.Key) {
            _hitItem = false;
        }
    }
}
