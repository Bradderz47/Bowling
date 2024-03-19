using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class score : MonoBehaviour
{

    public bool Lock;
    private Vector3 posi;
    private Vector3 rota;
    public money _money;
    // Start is called before the first frame update
    void Start()
    {
        Lock=false;
        posi = transform.position;
        rota = transform.localEulerAngles;


    }

    // Update is called once per frame
    void Update()
    {
        if (Lock==false)
        if (Mathf.Abs(transform.localEulerAngles.x) > 20 || Mathf.Abs(transform.localEulerAngles.z) > 20)
        {
            Lock =true;
            transform.parent.gameObject.GetComponent<GameController_>().Isbump = true;
                Debug.Log(gameObject.name);
        }
    }
    public void reset()
    {
        if (Mathf.Abs(transform.localEulerAngles.x) > 30 || Mathf.Abs(transform.localEulerAngles.z) > 30)
        {
            transform.parent.gameObject.GetComponent<GameController_>().N++;//set score
            _money.M++;
        }
           

        transform.position = posi;
        transform.localEulerAngles = rota;
        Lock = false; 
    }
}
