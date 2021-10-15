using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skript wird dafür verwantwortlich sein Game Objects / z.B Ammo in einem Bogen zu bewegen.

public class Arc : MonoBehaviour
{
    /// <summary>
    /// Ist eine Couroutine, die über mehrere Frames das Game Objekt in einem Bogen bewegen wird. 
    /// </summary>
    /// <param name="destination">das ziel, wo das Objekt hinbewegt werden soll</param>
    /// <param name="duration">bestimmt halt irgendwie auch die geschwindigkeit des Objektes</param>
    /// <returns></returns>
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        var startPosition = transform.position;

        var percentComplete = 0.0f;

        while (percentComplete < 1.0f)
        {
            // das ist eine gängige methode um game Object smooth zu bewegen
            percentComplete += Time.deltaTime / duration;

            var currentHeight = Mathf.Sin(Mathf.PI * percentComplete);

            transform.position = Vector3.Lerp(startPosition, destination, percentComplete) + Vector3.up * currentHeight;

            yield return null;
        }

        gameObject.SetActive(false);
    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
