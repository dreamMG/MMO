using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public Material material;

    private float rotate;

    private void Update()
    {
        material.SetFloat("_Rotation", rotate);

        rotate += 0.05f;
    }
}
