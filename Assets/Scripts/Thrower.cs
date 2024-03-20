using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Thrower : MonoBehaviour
{
    [SerializeField] private float minPower;
    [SerializeField] private float maxPower;
    [SerializeField] private float chargeTime;

    [SerializeField] private TextMeshProUGUI degreeText;
    [SerializeField] private Slider spinSlider;

    private bool charging = false;
    private float power = 0f;
    //Power Slider
    public Slider slider;

    private BowlingBall ball;
    //Bowling origin
    [SerializeField] private GameObject BowlingBall_Point;
    
    // Spin angle control
    [Range(-90,90)]
    private float spinAngle = 0;
    private float editDelay = 0;

    void Update()
    {
        if(ball != null) Charge();
        Selected();

        if (Input.GetKey(KeyCode.Q) && editDelay <= 0)
        {
            if (spinAngle > -90 ) spinAngle -= 1;
            degreeText.text = "Spin Angle : " + spinAngle + "*";
            spinSlider.value = 0.5f + (0.5f/90 * spinAngle);
            ball.SetSpin(spinAngle);
            editDelay = 0.05f;
        }
        else if (Input.GetKey(KeyCode.E) && editDelay <= 0)
        {
            if (spinAngle < 90) spinAngle += 1;
            degreeText.text = "Spin Angle : " + spinAngle + "*";
            spinSlider.value = 0.5f + (0.5f / 90 * spinAngle);
            ball.SetSpin(spinAngle);
            editDelay = 0.05f;
        }
            editDelay -= Time.deltaTime;

        // Ball Spin
    }
    /// <summary>
    /// Should be in the player controller
    /// </summary>
    private void Selected()
    {
        if (Input.GetMouseButtonDown(0) && ball == null)
        {
            RaycastHit hitInfo;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000);
            Vector3 halfExtents = new Vector3(0.001f, 0.001f, 0.001f);
            Collider[] colliders = Physics.OverlapBox(hitInfo.point, halfExtents);
            if (colliders.Length > 0 && colliders[0].tag.Equals("Ball"))
            {
                colliders[0].transform.position = BowlingBall_Point.transform.position;
                colliders[0].transform.parent = Camera.main.transform;
                ball = colliders[0].GetComponent<BowlingBall>();
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
            ball.Throw(Camera.main,power);
            ball = null;
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
