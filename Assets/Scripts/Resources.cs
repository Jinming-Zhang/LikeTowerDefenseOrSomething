using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public float amount = 0;
    public bool gainFromDamage = true;
    public bool gainFromKill = true;
    public bool gainFromWave = true;

    public int gainFromWaveMultiplyer = 5;
}
