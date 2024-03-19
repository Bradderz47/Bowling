using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounds : MonoBehaviour
{
    [SerializeField] private BoxCollider trigger;
    [SerializeField] private Collider ball;
    [SerializeField] private GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        // setting ball to isKinematic removes all momentum so the next throw is unbiased.
        if (other == ball)
        {
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.GetComponent<BowlingBall>().EndThrow();
            gameController.BallFinished();
        }
    }
}
