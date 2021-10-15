using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour
{

    /// <summary>
    /// wird im Unity Editor gesetzt
    /// </summary>
    public float maxHitPoints;


    /// <summary>
    /// wird im Unity Editor gesetzt
    /// </summary>
    public float startHitPoints;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// this method is called when the charakters hit-points reach zero
    /// ist eine "virtual" Methode, kann also in subclasses überschrieben werden.
    /// </summary>
    public virtual void KillCharakter()
    {
        Destroy(gameObject);
    }


    /// <summary>
    /// set the charakter back to its original starting state, so that it can be used again
    /// </summary>
    public abstract void ResetCharakter();

    /// <summary>
    /// Called by other Charakters to damage the current Character 
    /// </summary>
    /// <param name="damage">amount of damage that the character takes</param>
    /// <param name="interval">can be used for Damage over time, if interval = 0 the damage will inflict a single time,
    /// if the intervall > 0 the charakter will be damaged every [intervall] seconds</param>
    /// <returns>Der Returntype IEnumerator ist von einer Coroutine gefodert</returns>
    public abstract IEnumerator DamageCharacter(int damage, float interval);


}
