using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    // die referenz zum hitpoints scriptableObject 
    public Hitpoints hitpoints;

    // damit dieses attribut nicht im Inspector angezeigt wird (warum setzt man das dann nicht private)
    // diese referenz wird über das Programm gesetzt 
    [HideInInspector]
    public Player charakter;

    public Image imageMeter;

    public Text hpText;

    float maxHitpoints;

    // Start is called before the first frame update
    void Start()
    {
     //   maxHitpoints = charakter.maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (charakter != null )
        {
            // fill amount geht nur von 0 bis 1 
            imageMeter.fillAmount = hitpoints.value / charakter.maxHitPoints;

            hpText.text = "HP:" + (imageMeter.fillAmount * 100); 
        }
    }
}
