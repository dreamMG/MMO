using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 4, -10);
    private Vector3 pos;

    private bool lookAt = true;

    private Space offsetPositionSpace = Space.Self;

    public void FollowPlayer(Transform target)
    {
        this.target = target;
        transform.position = target.position;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            RaycastHit collisionHit;

            if (Physics.Linecast(transform.position, target.TransformPoint(offset), out collisionHit, LayerMask.GetMask("Terrain")))
            {
                transform.localPosition = Vector3.SmoothDamp(transform.position, collisionHit.point + new Vector3(0, 1, 0), ref pos, 0.25f);
            } else

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
}
