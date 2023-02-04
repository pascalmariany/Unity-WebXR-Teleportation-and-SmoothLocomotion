using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMouseController : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal, 0, vertical) * Time.deltaTime * speed;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(-mouseY, mouseX, 0));
    }
}