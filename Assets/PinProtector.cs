using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PinProtector : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float protectTime;

    [Header("References")]
    [SerializeField] private TextMeshPro readyText;

    private bool move = false;
    private bool protek = false;
    private float distance = 0f;

    private void Update()
    {
        if (move)
        {
            distance += moveSpeed * Time.deltaTime;
            if (protek)
            {
                transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            }
            else
            {
                transform.position -= new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            }
            if (distance >= moveDistance)
            {
                distance = 0f;
                move = false;
            }
        }
    }
    public void StartProtect() 
    {
        move = true;
        protek = !protek;
        Invoke("EndProtect", protectTime);
    }
    private void EndProtect()
    {
        if (gameController.GameActive())
        {
            move = true;
            protek = !protek;
            readyText.text = "Ready";
        }
    }
}
