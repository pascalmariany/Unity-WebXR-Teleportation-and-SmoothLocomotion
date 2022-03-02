using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class SmoothLocomotion : MonoBehaviour
{
    public bool fly = false;
    public float speed = 1.0f;
    public float scale = 0.01f;

    private GameObject player;
    private WebXRCamera cameraGroup;
    private Transform camera;
    private WebXRController controller;
    private Vector3 moveVector = new Vector3();
    private Quaternion orientation = new Quaternion();
    private Vector3 orientationEuler = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        player = WebXRManager.Instance.gameObject;
        cameraGroup = WebXRManager.Instance.GetComponentInChildren<WebXRCamera>();
        camera = cameraGroup.transform.GetChild(cameraGroup.transform.childCount - 1);
        controller = GetComponent<WebXRController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveVector.Set(0, 0, 0);
        // Get input vector from controller
        Vector2 axes = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick);
        if (axes.magnitude < 0.1f) return;
        // Get our initial move vector and normalize it
        axes.Normalize();
        // If we don't want to fly, this zeroes out any movement that isn't side-to-side
        float rotation = Mathf.Atan2(axes.x, axes.y) * Mathf.Rad2Deg;
        if (!fly) orientation = Quaternion.Euler(0, camera.rotation.eulerAngles.y + rotation, 0);
        // Adjust our vector based on where we're looking
        moveVector += orientation * Vector3.forward;
        // Scale movement vector
        moveVector.x *= scale;
        moveVector.y *= scale;
        moveVector.z *= scale;
        // Move the player
        player.transform.Translate(moveVector);
    }
}
