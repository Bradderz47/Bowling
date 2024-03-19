using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// The class for the pins, determining when they have fallen
/// </summary>
public class pin : MonoBehaviour
{
    /// <summary>
    /// The y position that the pins need to be under in order to be considered 'knocked over' 
    /// <para>NOTE : this will need to be changed if the model of pins are changed, or the height of the alley is changed.</para>
    /// </summary>
    [SerializeField] private float fallenHeight = -0.25f;
    private bool knocked = false;
    private bool dead = false;
    private bool reset = false;
    private Vector3 initialPos = Vector3.zero;

    [SerializeField] private float deathDelay = 10f;
    [SerializeField] private float deathSpeed = 1f;
    [SerializeField] private float resetTime = 1f;
    private float timer = 0f;

    private Rigidbody rb;

    private GameController gameController;
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        initialPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // The pin was knocked over
        if ( ( (Mathf.Abs(transform.localEulerAngles.x - 180) < 160)
            || (Mathf.Abs(transform.localEulerAngles.z - 180) < 160) )
            && !knocked && !reset)
        {
            gameController.PinKnocked();
            knocked = true;
        }
        // A death effect to remove the pins
        if (knocked && !dead && !reset)
        {
            timer += Time.deltaTime; 
            if (timer >= deathDelay) 
            {
                dead = true;
                rb.isKinematic = true;
            }
        }
        if (dead && transform.localPosition.y > fallenHeight)
        {
            transform.position -= new Vector3 (0, deathSpeed, 0) * Time.deltaTime;
            return;
        }
        if (reset)
        {
            transform.position += resetMovement;
            timer += Time.deltaTime;
            float percentageComplete = timer / resetTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, percentageComplete);
            if (timer >= resetTime)
            {
                transform.localPosition = initialPos;
                transform.rotation = Quaternion.identity;
                rb.isKinematic = false;
                reset = false;
                knocked = false;
            }
        }
    }
    private Vector3 resetMovement = Vector3.zero;
    public void ResetPin()
    {
        timer = 0;
        resetMovement = (initialPos - transform.localPosition) / resetTime * Time.deltaTime;

        reset = true;
        dead = false;
    }

    public bool IsKnocked() { return knocked; }
}
