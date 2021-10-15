using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    // eine referenz zu dem GameObjekt, dass wir spawnen wollen (player oder enemy) 
    public GameObject prefabToSpawn;

    public float repeatInterval;

    void Start()
    {
        if (repeatInterval > 0)
        {
            // mit dieser Methode kann in einem Festen Zeitintervall immer wieder ein Methode gecallt werden
            // Parameter: Die Methode to call, the time to wait before invoking the first time, intervall to wait between invocations
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }        


    }

    public GameObject SpawnObject()
    {
        if (prefabToSpawn != null)
        {
            return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }

        return null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
