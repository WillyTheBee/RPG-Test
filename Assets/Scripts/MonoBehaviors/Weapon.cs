using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject ammoPrefab;

    // static, also ein AmmoPool für alle Waffen
    static List<GameObject> ammoPool;

    public int poolSize;

    // wird genutzt um die travel Duration der ammo zu bestimmen, also bei weponVelocity 2 -> 1/2 = 0,5 
    // wird die Kugel 0,5 Sekunden brauchen um das ziel zu erreichen
    public float weponVelocity;

    // wenn die Skriptinstanz geladen wird wird der Ammo Pool der waffe gefüllt / siehe Object Pooling
    private void Awake()
    {
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject ammoObject = Instantiate(ammoPrefab);
                ammoObject.SetActive(false);
                ammoPool.Add(ammoObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // checkt jeden Frame ob ein Schuss abgefeuert wurde
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireAmmo();
        }
       
    }


  
    /// <summary>
    /// wird dafür verwantwortlich sein Objekte aus dem Object Pool ein-/auszulagern
    /// </summary>
    /// <param name="location">location gibt an wo die Ammo gespawnt werden soll </param>
    /// <returns> Das Game Objekt welches aus der Liste des Object Pools aktiviert wurde</returns>
    public GameObject SpawnAmmo(Vector3 location)
    {

        foreach (GameObject ammo in ammoPool)
        {

            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);

                ammo.transform.position = location;

                // hier Springt man aus der Schleife heraus, es wird also nur 1 Ammo Objekt zurückgegeben
                return ammo; 
            }

        }

        return null; 
    }

    /// <summary>
    /// wird dafür verwantwortlich sein die Ammo vom startpunkt (Spawn) bis zum endpunkt (Mausposition wo der button geklickt wurde) zu bewegen
    /// </summary>
    void FireAmmo()
    {
        // siehe Screen Points and World Points...
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // müsste eigentlich vom typ eine Weapon sein oder ? 
        GameObject ammo = SpawnAmmo(transform.position);

        if (ammo != null)
        {
            Arc arcScript = ammo.GetComponent<Arc>();

            float travelDuration = 1.0f / weponVelocity;

            StartCoroutine(arcScript.TravelArc(mousePosition, travelDuration));
        }
    }

    private void OnDestroy()
    {
        ammoPool = null; 
    }
}
