using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Throw(GameObject MainCamera, float power)
    {
        transform.SetParent(null);     
        rb.isKinematic = false;
        rb.velocity = MainCamera.transform.forward * power;
    }
}
