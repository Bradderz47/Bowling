using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A main script to act as a Hub for all things gameplay related
/// 
/// Needs a way to reset the round of pins as only one throw is able to be done.
/// </summary>
public class GameController : MonoBehaviour
{
    [Header("Initialisation")]
    [SerializeField] private Thrower_ thrower;
    [SerializeField] private pin[] pins;

    // Bowling awards a score of how many pins you knock down, with weird rules for spares and strikes
    // Strikes award 10, but also add the next 2 shots. So a maximum of 30 for a turkey
    // Spares award 10, but also add the next shot, so a maximum of 20 for a spare and then strike

    /// <summary>
    /// The time one must wait after the last pin has fallen, before the next throw is initiated
    /// </summary>
    [Header("Settings")]
    [SerializeField] private float timeToEndThrow = 4f;
    private float turnTimer;
    private bool firstPinKnocked;

    private bool locked = false;

    private int playerScore = 0;
    private int[] allScores = new int[10];
    private int[] pinsHitForFrames = new int[10];
    private int pinsKnockedThisFrame = 0;
    private int pinsKnockedThisThrow = 0;
    private int currentThrow = 1;
    private int noOfThrowsSinceLastScoring = 0;
    /// <summary>
    /// An array of the last 3 throws, allowing the scoring to function (the 0th element is the most recent throw)
    /// </summary>
    private int[] last3Throws = new int[3];
    /// <summary>
    /// The current frame the player is currently on
    /// </summary>
    private int currentFrame = 1;
    private int scoredFrames = 0;

    private void Update()
    {
        if (firstPinKnocked)
        {
            turnTimer += Time.deltaTime;
            if (turnTimer >= timeToEndThrow) 
            {
                firstPinKnocked = false;
                // Stop pins from falling after the timer, to avoid unforseen bugs
                foreach (pin pin in pins)
                {
                    pin.GetComponent<Rigidbody>().isKinematic = true;
                }
                EndThrow();
            }
        }
    }

    /// <summary>
    /// Called by a pin when it detects that it has fallen over
    /// </summary>
    public void PinKnocked()
    {
        //Debug.Log("Knocked a pin " + (pinsKnockedThisFrame+1) + " this frame");
        if (!firstPinKnocked) firstPinKnocked = true;
        turnTimer = 0;
        pinsKnockedThisFrame++;
        pinsKnockedThisThrow++;
    }
    public void EndThrow()
    {
        locked = false;
        noOfThrowsSinceLastScoring++;
        // Edit the last three throws
        last3Throws[2] = last3Throws[1];
        last3Throws[1] = last3Throws[0];
        last3Throws[0] = pinsKnockedThisThrow;
        // End if shot was a strike, or if two subsequent shots where thrown, ignoring the 3 round last frame
        if (pinsKnockedThisFrame == 10 || (currentThrow == 2 && currentFrame != 10))
        {
            Debug.Log("Starting Frame " + (currentFrame + 1));
            // Set pins knocked to 11, even though it was 10, so i know it was a strike and not a spare
            if (pinsKnockedThisThrow == 10 && currentThrow == 1) pinsHitForFrames[currentFrame - 1] = 11;
            else pinsHitForFrames[currentFrame - 1] = pinsKnockedThisFrame;

            // Reset ALL pins
            foreach (pin pin in pins) pin.ResetPin();
            if (currentFrame != 10)
            {
                pinsKnockedThisFrame = 0;
                currentFrame++;
                currentThrow = 0;
            }
        }
        else if (currentFrame == 10)
        {
            // The 10th frame works as usual, but the player gets up to 3 throws
            // Makje sure to calculate the score for the 9th frame

            // Reset the pins for a strike
            if (currentThrow == 2)
            {
                // If the player leaves the frame open (no spare or strike) - End the game
                if (pinsKnockedThisFrame < 10)
                {
                    Debug.Log("End Game");
                    playerScore += pinsKnockedThisFrame;
                    Debug.Log("FINAL SCORE : " + playerScore);
                    return;
                }
                else foreach (pin pin in pins) pin.ResetPin();
            }
            // All three throws have been utilised, calculate the final score
            if (currentThrow == 3)
            {
                CalculateScore(true);
                Debug.Log("FINAL SCORE : " + playerScore);
                return;
            }
        }
        if (noOfThrowsSinceLastScoring >= 3 && currentThrow < 3)
        {
            CalculateScore(false);
        }

        // Replace unknocked pins
        foreach (pin pin in pins)
        {
            if (!pin.IsKnocked()) pin.ResetPin();
        }

        Debug.Log("Next Throw");
        currentThrow++;
        pinsKnockedThisThrow = 0;
        //thrower.initiateNewThrow();
    }

    private void CalculateScore(bool lastFrame)
    {
        noOfThrowsSinceLastScoring--;
        // Do normal score if we are not at the last frame
        if (!lastFrame)
        {
            // Strike - 10 + next two throws
            if (pinsHitForFrames[scoredFrames] == 11) { allScores[scoredFrames] = last3Throws.Sum(); }
            // Spare - 10 + next throw
            else if (pinsHitForFrames[scoredFrames] == 10) { allScores[scoredFrames] = 10 + last3Throws[0]; noOfThrowsSinceLastScoring--; }
            // Anything else
            else { allScores[scoredFrames] = pinsHitForFrames[scoredFrames]; noOfThrowsSinceLastScoring--; }
            playerScore += allScores[scoredFrames];
            scoredFrames++;
        }
        else
        {
            Debug.Log("final frame");
            // The final throw is simply just the sum of all the throws of the final frame with a max of 30 for 3 strikes
            if (currentThrow == 3) { allScores[scoredFrames] = last3Throws.Sum(); }
            else { allScores[scoredFrames] = last3Throws[0] + last3Throws[1]; }
            playerScore += allScores[scoredFrames];
            scoredFrames++;
        }
        Debug.Log("Score for frame "+scoredFrames+" : " + allScores[scoredFrames-1] + " For a total score of " + playerScore);
    }


    public void SwitchLocked() { locked = !locked; }
    public bool IsLocked() { return locked; }

    /// <summary>
    /// The ball is finished, so act as if a pin was knocked, starting the timer to end the turn, even if no pins were knocked
    /// </summary>
    public void BallFinished()
    {
        firstPinKnocked = true;
    }

}
