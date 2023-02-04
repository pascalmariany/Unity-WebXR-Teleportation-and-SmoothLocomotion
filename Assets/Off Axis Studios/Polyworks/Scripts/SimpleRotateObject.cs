using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotateObject : MonoBehaviour
{
    [Tooltip("Rotation speed.")]
    public float speed = 2.0f;

    [Tooltip("Which axis to rotate on. Select only one!")]
    public bool X;
    public bool Y;
    public bool Z;

    void Update()
    {
        if (X)
        {
            transform.Rotate(speed * 10 * Time.deltaTime, transform.rotation.y, transform.rotation.z);
        }
        else if (Y)
        {
            transform.Rotate(transform.rotation.x, speed * 10 * Time.deltaTime, transform.rotation.z);
        }
        else if (Z)
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y, speed * 10 * Time.deltaTime);
        }
        else
        {
            transform.Rotate(transform.rotation.x, speed * 10 * Time.deltaTime, transform.rotation.z);
        }
    }
}
