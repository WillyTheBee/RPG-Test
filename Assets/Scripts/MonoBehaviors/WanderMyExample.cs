using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//jedes Objekt an das das Wander Skript angeheftet wird muss diese komponenten haben!
// wenn das Objekt diese Komponenten nicht besitzt werden sie automatisch hinzugefügt, wenn das skript hinzugefügt wird

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]



public class WanderMyExample : MonoBehaviour
{
    // current speed will be one of the previous speeds...
    public float pursuitSpeed; // pursuit = verfolgen
    public float wanderSpeed;
    float currentSpeed;

    // will be used to determine how often the Enemy should change direction
    public float directionChangeInterval;

    // flag that can be used if i want an Enemy that Wander around but dont chase the Player 
    public bool followPlayer;

    // to save the Movement Coroutine which will move the Enemy a little bit each frame toward the destination 
    Coroutine moveCoroutine;

    // store the rb2d and the animator attached to the game objekt 
    Rigidbody2D rb2d;
    Animator animator;

    // transform from the player (which will be chased) 
    Transform targetTransform = null;

    // destination where the Enemy is wandering
    Vector3 endPosition;

    // when a new destination is set, a new angle is added to the existing angle. That angle is used to generate a vector which becommes the destination
    float currentAngle = 0; 

    
    
    void Start()
    {
        // setting Values for the Variables
        animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;
        // rigidboady brauchen wir um den Enemy zu bewegen
        rb2d = GetComponent<Rigidbody2D>();


        StartCoroutine(WanderRoutine());
    }


    // this Method is a coroutine because it doubtlessly (zweifellos) run ober multiple frames
    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndpoint();

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // start the Move() Coroutine and stores it 
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));

            // wartet bis die Zeit abgelaufen ist und startet dann die while schleife neu (pausiert die Coroutine)  
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }


    /// <summary>
    /// wird gecallt wenn der player in den "sichbereich" des Enemys kommt
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            currentSpeed = pursuitSpeed;

            targetTransform = collision.gameObject.transform;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // in Move() wird der Zielpunk als position des players dann festgelegt
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
        }
    }

    /// <summary>
    /// wenn der Player die sicht des gegners wieder verlässt muss der Gegner wieder auf seinen normalen pfad zurückkehren
    /// bzw alles muss gestoppt werden bis die
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // weil der gegner sein ziel nicht erreicht muss hier die laufani gestoppt werden 
            animator.SetBool("isWalking", false);

            currentSpeed = wanderSpeed;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            targetTransform = null; 
        }
    }

    /// <summary>
    /// actually moves the Enemy 
    /// </summary>
    /// <param name="rb2d"></param>
    /// <param name="currentSpeed"></param>
    /// <returns></returns>
    public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        // die gleichung gleichung gibt einen Vector zurück und durch sqr.Magnitude wird der Vector in eine float distanz umgewandelt
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            // wenn der gegner in der Verfolgung ist wir die endPosition umgeschrieben
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            // make sure we have a rigidboady to move
            if (rigidbodyToMove != null)
            {

                animator.SetBool("isWalking", true);

                // the Method moveTowards doesnt actually move the Rigidboady it just calculates the Movement by taking 3 Parameters
                // ( current position,  end position, distance ) 
                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endPosition, speed * Time.deltaTime);

                rb2d.MovePosition(newPosition);

                // update the remaining distance 
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }

            // pausierte ausführung bis zum nächsten fixed Frame update 
            yield return new WaitForFixedUpdate(); 

        }

        // Enemy hat sein ziel erreicht und wartet auf den nächsten durchlaufe der WanderCoroutine
        animator.SetBool("isWalking", false);
    }

    /// <summary>
    /// chooeses a new endpoint (random) but doesnt start the Enemy moving toward it 
    /// </summary>
    private void ChooseNewEndpoint()
    {
        // unity Engine beschreibt den Namespace, aus welchem Random genommen werden soll...
        // es wird also eine neue richtung mit einem winkel beschrieben und zum derzeitigen winkel dazuaddiert 
        currentAngle += UnityEngine.Random.Range(0,360);

        // wenn durch das addieren der wert über 360 gehen sollte wird fängt es wieder bei 0 an, also ist der winkel auf einen bereich zwischen 0 und 360 begrenzt
        currentAngle = Mathf.Repeat(currentAngle, 360);


        endPosition += getVector3FormAngle(currentAngle);
    }

    /// <summary>
    /// Wandelt den winkel in einen Vector3 um... 
    /// </summary>
    /// <param name="currentAngle"></param>
    /// <returns></returns>
    Vector3 getVector3FormAngle(float inputAngleDegrees)
    {
        // umwandeln von Degrees zu Radiant durch mutliplizieren der dafür vorgesehenen Constante
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        // herstellen eines normalisierten Vectors 
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
