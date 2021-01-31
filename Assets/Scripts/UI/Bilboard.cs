using Mirror;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    [SerializeField] private Camera cameraPlayer;

    private void Start()
    {
        cameraPlayer = Camera.main;
    }

    private void LateUpdate()
    {
        if (cameraPlayer != null)
            transform.LookAt(transform.position + cameraPlayer.transform.forward);
    }
}
