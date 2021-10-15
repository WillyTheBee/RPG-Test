using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // set up the properties

    // hält die referenz zu einem Slot prefab
    public GameObject slotPrefab;

    public const int numSlots = 5;


    // dieses Array hält die Item images 
    Image[] itemImages = new Image[numSlots];


    // items die sich dann in den slots befinden (also die scriptable objects) 
    Item[] items = new Item[numSlots];


    /// hält die referenz zu den erzeugten slots, wird benutzt um das text Object innerhalb des slots zu finden
    GameObject[] slots = new GameObject[numSlots];

    ///<summary>
    ///INSTANTIALTE THE SLOT PREFABS
    ///for dynamically creating the Slot objects from the prefab
    ///</summary>
    public void CreateSlots()
    {
        if (slotPrefab != null)
        {
            for (int i = 0; i < numSlots; i++)
            {
                //instanziiere eine kopie des slot prefabs 
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i;


                // Das Skript wird zu InventoryObject attached, und das hat wiederum nur 1 child Objekt: InventoryBackground
                // dieses child Objekt hat dann den index 0 und wird der Parent des Slots 
                // Inventory Background hat auch die Horizontal Layout group wodurch die slots dann schön geordnet werden 
                // gameObject funktioniert undgefähr wie "this"
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = newSlot;

                // das child auf index 1 beim Slot ist Itemimage und bei dem Itemimage Object gibt es eine Image Component 
                // die man herausziehen und speichern kann 
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }


    /// <summary>
    /// Fill in the Start() Method 
    /// </summary>
    public void Start()
    {
        CreateSlots();
    }

    /// <summary>
    /// method to actually add an Item to the Inventory 
    /// </summary>
    /// <param name="itemToAdd"> The Item that you want to add to the Inventory</param>
    public bool AddItem(Item itemToAdd)
    {
        //in C# gibt es komische getter und setter 
        for (int i = 0; i < items.Length; i++)
        {

            // wenn es ein stackable item ist such man nach dem slot das schon dieses item enthält und will dann
            // die quantity um 1 erhöhen 
            // wir stacken ein item!
            if (items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.stackable == true)
            {

                // Adding to existing Slot 
                items[i].quantity = items[i].quantity + 1;

                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();

                // das ist nur eine referenz und keine kopie! 
                // man wäre auch mit getChild and das Text Objekt gekommen 
                Text quantityText = slotScript.qtyText;

                // halt den text sichtbar machen
                quantityText.enabled = true;

                // setze den text halt auf die richtige Menge
                quantityText.text = items[i].quantity.ToString();

                return true;
            }

            // wir fügen ein item frisch hinzu 
            if (items[i] == null)
            {
                // Adding to empty slot
                // copy item & add to inventory. copying so we dont change original Scriptable Object

                items[i] = Instantiate(itemToAdd);

                items[i].quantity = 1;

                itemImages[i].sprite = itemToAdd.sprite;


                // beim ersten Item was geadded wird muss das Image enabelt werden, oben in dem if braucht man das nicht
                // weil es dort ja schon enablet ist
                itemImages[i].enabled = true;

                // damit das item nicht zu alles slots geadded wird, sonder beim ersten leeren aufhört cancelt man
                // hier die Methode 
                return true;
            }
        }

        return false;
    }
}
