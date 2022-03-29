using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class OscillationHandler : MonoBehaviour
{
    [SerializeField]
    Vector3 movementVector;


    [SerializeField]
    float period = 5f;

    float movementFactor;
    Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so it's cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
