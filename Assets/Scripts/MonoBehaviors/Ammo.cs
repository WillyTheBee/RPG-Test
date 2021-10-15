using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int damageInflicted;


    // Bullet brühert den Gegner 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // es ist wichtig hier den Boxcollider zu prüfen weil wenn wir auf den Enemy Tag prüfen würden könnten wir auch den Circle Collider vom Enemy treffen 
        if (collision is BoxCollider2D)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // damage wird nur 1 mal inflicted :D
            StartCoroutine(enemy.DamageCharacter(damageInflicted,0.0f));

            // Ammo inactive setzen anstatt Destory Objekt aufzurufen um mit Objekt Pooling bessere Performance zu erreichen
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
