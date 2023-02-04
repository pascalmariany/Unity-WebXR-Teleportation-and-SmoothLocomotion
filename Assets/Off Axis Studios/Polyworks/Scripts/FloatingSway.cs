using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSway : MonoBehaviour
{
    public bool Enabled = true;
    public float Speed = 1f;
    public float TravelDistance = 0.05f;

    [Tooltip("Which axis to sway. Enable only one of the below!")]
    public bool X;
    public bool Y;
    public bool Z;

    private Vector3 pointA;
    private Vector3 pointB;

    IEnumerator Start()
    {
        pointA = transform.position;
        if (X)
        {
            pointB = new Vector3(transform.position.x + TravelDistance,
                        transform.position.y,
                        transform.position.z);
        }
        else if (Y)
        {
            pointB = new Vector3(transform.position.x,
                                    transform.position.y + TravelDistance,
                                    transform.position.z);
        }
        else if (Z)
        {
            pointB = new Vector3(   transform.position.x,
                                    transform.position.y,
                                    transform.position.z + TravelDistance);
        }
        else
        {
            pointB = new Vector3(transform.position.x,
                        transform.position.y,
                        transform.position.z + TravelDistance);
        }


        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        if (Enabled)
        {
            var i = 0.0f;
            var rate = Speed / time / 2f;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                thisTransform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }
        }
    }
}
