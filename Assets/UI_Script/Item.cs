using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC,
    }

    public string itemName;
    public Sprite itemImage;
    public ItemType itemType;
    public GameObject itemPrefab;

    public string weaponType;
}
