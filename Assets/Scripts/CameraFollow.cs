using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private Space offsetPositionSpace = default;
    bool lookAt = true;

    Vector3 pos;

    private void LateUpdate()
    {
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.TransformPoint(offset), ref pos, 0.5f);
        }
        else
        {
            transform.position = target.position + offset;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
