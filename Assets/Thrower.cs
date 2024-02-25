using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    [SerializeField] private BowlingBall ball;

    [SerializeField] private float minPower;
    [SerializeField] private float maxPower;
    [SerializeField] private float chargeTime;

    [Header("References")]
    [SerializeField] private GameController gameController;


    private bool charging = false;

    private float power = 0f;

    // Update is called once per frame
    void Update()
    {
        // Release the ball
        if (Input.GetKeyUp(KeyCode.Space) && gameController.IsLocked())
        {
            gameObject.SetActive(false);
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);

            // Get the current facing of the player
            float Xrotation = transform.rotation.eulerAngles.y;
            if (Xrotation > 45) Xrotation -= 360;

            float Yrotation = transform.rotation.eulerAngles.x;
            if (Yrotation > 60) Yrotation -= 360;


            ball.Throw(Xrotation, -Yrotation, power);

            charging = false;
            power = 0f;
        }
        // Start Charging
        else if (Input.GetKeyDown(KeyCode.Space) && gameController.IsLocked() && !charging)
        {
            power = minPower;
            charging = true;
        }
        // Hold Charge
        else if (charging && power < maxPower)
        {
            power += (maxPower * Time.deltaTime / chargeTime); 
            if (power > maxPower) power = maxPower;
        }
    }
}
