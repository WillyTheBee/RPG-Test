using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// inherts from Charakters, hat also zugriff auf public variablen und Methoden
/// </summary>
public class Enemy : Characters
{
    // diese Variable wird bestimmen wie viel schaden der Player bekommt wenn er in einen Enemy reinläuft
    public int damageStrength;


    // weil running Coroutines in einer Variablen gespeichert werden können wird die Damage Charakter Coroutine in dieser 
    // Variable gespeichert 
    Coroutine damageCoroutine;

    float hitpoints;

    public override IEnumerator DamageCharacter(int damage, float interval)
    {

        while (true)
        {
            hitpoints = hitpoints - damage;

            // epsilon ist ein ersatz für 0, da float bei 0 sonst manchmal buggy ist. 
            if (hitpoints <= float.Epsilon)
            {
                KillCharakter();
                break;
            }

            if (interval > float.Epsilon)
            {
                // warte eine bestimme anzahl von sekunden (pausiere die Methode) 
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// set the Character Variables back to their original state
    /// can also be used to setup the variables when the Character is first created 
    /// </summary>
    public override void ResetCharakter()
    {
        // starthitpoints wird im Prefab im Unity editor angepasst
        hitpoints = startHitPoints;
    }

    private void OnEnable()
    {
        ResetCharakter();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // nur der spieler wird gedamaged wenn er einen enemy berühert
        if (collision.gameObject.CompareTag("Player"))
        {
            // man zieht sich die Skript Component heraus 
            Player player = collision.gameObject.GetComponent<Player>();

            if (damageCoroutine == null)
            {
                // wir speichern die Coroutine in einer Variablen, damit wir jederzeit auch StopCoroutine aufrufen können
                // charakter wird in diesem fall jede Sekunde gedamaged bis die Coroutine pausiert wird 
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
            }
        }
    }

    // wird von Unity gecallt wenn man die Hitbox vom enemy wieder verlässt und ist dafür da die Coroutine wieder zu stoppen
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
}
