using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// erschaft quasi einen Menu eintrag im create Untermenue bei den Assets, um items dann einfacher erstellen zu können
[CreateAssetMenu(menuName = "Item")]

public class Item : ScriptableObject
{

    public string objectName;

    public Sprite sprite;

    public int quantity;

    public bool stackable;

    public enum ItemType
    {
        COIN,
        HEALTH
    }

    public ItemType itemType;

    

}
