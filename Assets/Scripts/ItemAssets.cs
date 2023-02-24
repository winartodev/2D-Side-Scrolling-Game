using UnityEngine;
using System.Collections;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite swordSprite;
    public Sprite armorSprite;
    public Sprite shieldSprite;
    public Sprite keySprite;
}

