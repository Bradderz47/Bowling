using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purchase : MonoBehaviour
{
    public bool ispurchase;
    private Rigidbody rb;
    public GameObject can;
    public money mo;
    public int price=30;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ispurchase = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Sp()
    {
        if(mo.M> price)
        {
            mo.M-= price;
            ispurchase = true;
            can.SetActive(false);
            this.GetComponent<BowlingBall>().enabled = true;
            rb.isKinematic = false;
            Debug.Log("succeed");
            return true;
        }
        else
        {
            Debug.Log("Failed");
            return false;
        }
    }
}