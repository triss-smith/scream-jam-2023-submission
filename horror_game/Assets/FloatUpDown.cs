using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpDown : MonoBehaviour
{
    public float amplitude = 1.0f;  // The distance the object moves up and down.
    public float speed = 1.0f;     // The speed of the movement.

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position using the sine function to create the oscillating effect.
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // Update the object's position.
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
