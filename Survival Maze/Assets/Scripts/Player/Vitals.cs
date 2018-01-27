using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script allows the inspector in Unity to see Vital as an object
// Click player then in "Player Vitals (Script)" component, you'll see "Vitals"
// that is a List where each element of the list is the Vital object 

[System.Serializable]
public class Vital {

    public Slider slider;
    public int max;
    public float fallRate;

}
