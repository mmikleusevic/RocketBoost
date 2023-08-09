using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 _movementVector;
    [SerializeField] float _period = 2f;

    float _movementFactor;
    Vector3 _startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_period <= Mathf.Epsilon) return;

        float cycles = Time.time / _period; //continually growing over time
        const float tau = Mathf.PI * 2; // constant value of 6.283;
        float rawSinWave = Mathf.Sin(cycles * tau);

        _movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1

        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
