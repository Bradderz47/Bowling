using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A main script to act as a Hub for all things gameplay related
/// </summary>
public class GameController : MonoBehaviour
{

    //[Header("Settings")]

    private bool locked = false;


    public void SwitchLocked() { locked = !locked; }
    public bool IsLocked() { return locked; }

}
