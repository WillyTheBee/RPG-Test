using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class RPGCameraManager : MonoBehaviour
{
    //Singleton Pattern 

    public static RPGCameraManager sharedInstance = null;


    // reference to the Cinemachine Vitual Camera, public so that other calsses can access it.
    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        // Singleton pattern
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }

        // find the VirtualCamera in the Sceene
        GameObject vCamGameObject = GameObject.FindWithTag("VirtualCamera");

        // referenz speichern, damit man alle Properties der Virtual Camera über Code anpassen kann
        virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
