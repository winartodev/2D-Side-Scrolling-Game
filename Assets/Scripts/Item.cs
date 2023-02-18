using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public enum ItemType {
        Sword,
        Armor,
        Sheild,
        Key,
    }

    public ItemType itemType;

    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.Armor: return ItemAssets.Instance.armorSprite;
            case ItemType.Sheild: return ItemAssets.Instance.shieldSprite;
            case ItemType.Key: return ItemAssets.Instance.keySprite;
        }
    }
}
