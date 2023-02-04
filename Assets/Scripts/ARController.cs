using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ARController : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject objectToMove;

    void Update()
    {
        // Get the input device and its pose.
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 devicePosition);
        device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion deviceRotation);

        // Update the position of the object based on the device's position.
        objectToMove.transform.position = devicePosition;

        // Update the rotation of the object based on the device's orientation.
        objectToMove.transform.rotation = deviceRotation;
    }
}