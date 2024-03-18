using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thrower : MonoBehaviour
{
    [SerializeField] private float minPower;
    [SerializeField] private float maxPower;
    [SerializeField] private float chargeTime;
    private bool charging = false;
    private float power = 0f;
    //Power Slider
    public Slider slider;

    private Ball_Controller Ball;
    //Bowling origin
    public GameObject BowlingBall_Point;


    public GameObject MainCamera;
    private void Start()
    {
     
    }
    void Update()
    {
        if(Ball!=null)  Charge();
        Selected();
    }
    private void Selected()
    {
        RaycastHit hitInfo;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000);
        Vector3 halfExtents = new Vector3(0.001f, 0.001f, 0.001f); 
        Collider[] colliders = Physics.OverlapBox(hitInfo.point, halfExtents);
        if (colliders.Length > 0&& colliders[0].tag.Equals("Ball"))
        {    

            if (Input.GetMouseButtonDown(0)&& Ball==null)
            {
                if(colliders[0].name.Equals("qingmo")&&!colliders[0].GetComponent<purchase>().ispurchase)
                {
                    colliders[0].GetComponent<purchase>().Sp();
                }
                else
                {
                    colliders[0].transform.position = BowlingBall_Point.transform.position;
                    colliders[0].transform.parent = MainCamera.transform;
                    Ball = colliders[0].GetComponent<Ball_Controller>();
                    colliders[0].GetComponent<Rigidbody>().isKinematic = true;
                }                
            }
        }        
    }
    private void Charge()
    {
        // Release the ball
        if (Input.GetMouseButtonUp(1))
        {
            Ball.Throw(MainCamera,power);
            Ball=null;

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
