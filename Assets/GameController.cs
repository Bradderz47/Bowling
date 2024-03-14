using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A main script to act as a Hub for all things gameplay related
/// 
/// Needs a way to reset the round of pins as only one throw is able to be done.
/// </summary>
public class GameController : MonoBehaviour
{

    //[Header("Settings")]

    private bool locked = false;


    public void SwitchLocked() { locked = !locked; }
    public bool IsLocked() { return locked; }

}
