using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the physics of the bowling ball, It will be thrown at the direction of the camera
/// <para> A small constant force is applied forwards at all times to make the 'roll' feel more natural </para>
/// <para> Throwing power is determined by a release to fire mechanism that is stronger the longer you hold it</para>
/// <para> (Subject to change) Spin is controlled directly in the ball object, so the player can't change it yet, but is fully functional, needs to be accessed by the player
/// by some kind of ui </para>>
/// </summary>
public class BowlingBall : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;

    /// <summary>
    /// How hard the ball is thrown
    /// </summary>
    [SerializeField] private float throwingPower;
    [SerializeField] private float throwingAngle;

    /// <summary>
    /// The amount of force applied to the ball each frame, (to keep it rolling)
    /// </summary>
    [SerializeField] private float rollStength;

    /// <summary>
    /// The strength of the initial ball spin (including spinning forwards)
    /// </summary>
    [SerializeField] private float spinPower;
    [SerializeField] private float spinAngle;

    Vector3 force;
    Vector3 torque;

    private bool thrown = false;
    private void FixedUpdate()
    {
        // Constantly add force in chosen directions to amplify the effects.
        if (thrown)
        {
            rb.AddForce(new Vector3(force.x, 0, force.z).normalized * rollStength * Time.deltaTime);
            rb.AddTorque(torque * Time.deltaTime);
        }
        
    }

    public void Throw(float x, float y, float power)
    {
        // Ensure throw is applied in the look direction

        // Convert the angle into radiens and calculate the force vector
        float horizontalRads = x / (180 / Mathf.PI);
        float verticalRads = y / (180 / Mathf.PI);
        force = new Vector3(Mathf.Sin(horizontalRads), Mathf.Tan(verticalRads), Mathf.Cos(horizontalRads)).normalized;

        Debug.DrawRay(transform.position, force * power, Color.red, 60f);

        // Spin is applied independent of player facing
        float forward = 90 - Mathf.Abs(spinAngle);
        float side = spinAngle;

        torque = new Vector3(forward + x, 0, side - x).normalized * spinPower;      
        if (!thrown)
        {
            thrown = true;
            rb.isKinematic = false;
            rb.AddForce(force * power);
        }   
    }

    public void SetValues(float power, float angle, float spin, float spinAngle) 
    {
        throwingPower = power;
        throwingAngle = angle;
        spinPower = spin;
        this.spinAngle = spinAngle;
    }

    public void EndThrow()
    {
        thrown = false;
    }
}
