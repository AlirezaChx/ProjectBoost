using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    private float _movementFactor;
    private Vector3 _startingPosition;
    private readonly float _period = 2f;

    void Start()
    {
        _startingPosition = transform.position;
    }

    void Update()
    {
        var cycles = Time.time / _period;

        const float tau = Mathf.PI * 2;
        var rawSinWave = Mathf.Sin(cycles * tau);

        _movementFactor = (rawSinWave + 1f) * 0.5f;

        var offset = movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
