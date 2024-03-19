using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thrower_ : MonoBehaviour
{
    [SerializeField] private float minPower;
    [SerializeField] private float maxPower;
    [SerializeField] private float chargeTime;

    private bool charging = false;
    private float power = 0f;
    //Power Slider
    public Slider slider;

    private BowlingBall Ball;
    //Bowling origin
    public GameObject BowlingBall_Point;


    public GameObject MainCamera;
    void Update()
    {
        if(Ball != null) Charge();
        Selected();
    }
    /// <summary>
    /// Should be in the player controller
    /// </summary>
    private void Selected()
    {
        if (Input.GetMouseButtonDown(0) && Ball == null)
        {
            RaycastHit hitInfo;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000);
            Vector3 halfExtents = new Vector3(0.001f, 0.001f, 0.001f);
            Collider[] colliders = Physics.OverlapBox(hitInfo.point, halfExtents);
            if (colliders.Length > 0 && colliders[0].tag.Equals("Ball"))
            {
                colliders[0].transform.position = BowlingBall_Point.transform.position;
                colliders[0].transform.parent = MainCamera.transform;
                Ball = colliders[0].GetComponent<BowlingBall>();
                colliders[0].GetComponent<Rigidbody>().isKinematic = true;
            }
            else if (colliders[0].tag.Equals("BuyBall") && !colliders[0].GetComponent<purchase>().ispurchase)
            {
                colliders[0].GetComponent<purchase>().Sp();
            }
        }
    }
    private void Charge()
    {
        // Release the ball
        if (Input.GetMouseButtonUp(1))
        {
            Ball.Throw(Camera.main,power);
            Ball = null;
            charging = false;
            power = 0f;
        }
        // Start Charging
        else if (!charging && Input.GetMouseButtonDown(1))
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
        slider.value = (power / maxPower);
    }
}
