using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{
    // um eine kopie des Healthbar prefabs mit instantiate zu instanziieren
    public HealthBar healthBarPrefab;

    // um die referenz zum Healthbar  zu speichern 
    // jeder player hat eine Healthbar, in der start methode wird dann die referenz von Healthbar zum player Object erzeugt
    HealthBar healthBar;


    // wird im Inspector angefügt, das Prefab muss ein Skript vom Typ Inventory beigefügt haben
    // also damit wird der Datentyp festgelegt
    public Inventory inventoryPrefab;

    public Hitpoints hitPoints;

    Inventory inventory;

    

    private void Start()
    {
        hitPoints.value = startHitPoints;
        ResetCharakter();
    }
    // gets called whenever this object overlaps with a trigger collider 
    void OnTriggerEnter2D(Collider2D collision)
    {

        // man kann die Kollision nutzen um sich das GameObjekt zu schnappen mit dem man Kollidiert ist und vergleicht dann halt den gewünschten Tag
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        { 
            //holt sich von dem Object mit dem es kollidiert ist die Consumable component und von dort die Item property 
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;


            //falls man vergessen hat dem Consumable script ein Item zuzuweisen 
            if (hitObject != null)
            {
                // wenn der spieler schon 10 leben hat wird das herz nicht diappearen
                bool shouldDisappear = false;

                print("Hit: " + hitObject.objectName);

                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.AddItem(hitObject);

                        // we dont need it anymore, weil wenn das item aufgeoben wird dann liefert die andere zeile schon
                        // true zurück;
                        // shouldDisappear = true;
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear =  AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }


                if (shouldDisappear)
                {
                    //lässt das Object mit dem man Kollidiert ist verschwinden 
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    private bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value += amount;
            print("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints.value);

            return true;
        }

        return false;
    }


    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            hitPoints.value = hitPoints.value - damage;
            if (hitPoints.value <= float.Epsilon)
            {
                KillCharakter();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public override void KillCharakter()
    {
        // mit dieser Zeile callt man den Code der in der parent klasse steht und dann kann man halt noch seinen für diese 
        // klasse spezifischen Code adden
        base.KillCharakter();

        // Healthbar und Inventory von diesem player Obj. werden zerstört
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override void ResetCharakter()
    {
        inventory = Instantiate(inventoryPrefab);
        // ich glaube das ist der Konstruktor für die Healthbar 
        healthBar = Instantiate(healthBarPrefab);
        healthBar.charakter = this;

        hitPoints.value = startHitPoints;
    }

}
