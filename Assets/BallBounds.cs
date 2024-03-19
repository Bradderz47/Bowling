using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounds : MonoBehaviour
{
    [SerializeField] private BoxCollider trigger;
    [SerializeField] private GameController gameController;

    [SerializeField] private Transform returnLocation;

    [SerializeField] private float rollSpeed;
    [SerializeField] private float returnDelay;

    private GameObject ball = null;

    private void OnTriggerEnter(Collider collider)
    {
        // setting ball to isKinematic removes all momentum so the next throw is unbiased.
        if (collider.tag.Equals("Ball") || collider.tag.Equals("BuyBall"))
        {
            collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            collider.gameObject.SetActive(false);
            collider.GetComponent<BowlingBall>().EndThrow();
            gameController.BallFinished();

            ball = collider.gameObject;
            Invoke("ReturnBall", returnDelay);
        }
    }
    private void ReturnBall()
    {
        ball.transform.position = returnLocation.position;
        ball.SetActive(true);
        ball.GetComponent<Rigidbody>().velocity = -Vector3.forward * rollSpeed * Time.deltaTime;
    }
}
