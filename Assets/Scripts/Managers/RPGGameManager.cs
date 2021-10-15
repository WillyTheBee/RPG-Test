using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Das ist eine Singleton klasse


public class RPGGameManager : MonoBehaviour
{
    public SpawnPoint playerSpawnPoint;

    // static damit die variable zur klasse an sich gehört und nicht zu einer Instanz der klasse
    public static RPGGameManager sharedInstance = null;

    // wenn man eine referenz zu sharedInstance braucht nimm man folgende Syntax ( also man braucht diese referenz nur in einem anderen Skript)
    // RPGGameManager gameManager = RPGGameManager.sharedInstance;


    // referenz zum Camera Manager wird gebraucht wenn wir den Player Spawnen
    public RPGCameraManager cameraManager;

    private void Awake()
    {
        // um zu vermeiden das es mehrere Instanzen der Klasse gibt
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }else
        {
            sharedInstance = this;
        }
    }

    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject();

            // die Camera soll dem Player folgen
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupScene();
    }

    private void SetupScene()
    {

        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
