using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gest : MonoBehaviour
{
    Camera cam;

    [SerializeField] TrailRenderer trailPrefab = default;
    [SerializeField] TrailRenderer trail = default;

    [SerializeField] Spells spells = default;

    const int MAX_POSITIONS = 40;
    [SerializeField] Vector3[] TrailRecorded = new Vector3[MAX_POSITIONS];


    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCheckSymbol();

            Vector3 temp = Input.mousePosition;
            temp.z = 10f;
            transform.position = cam.ScreenToWorldPoint(temp);

            trail = Instantiate(trailPrefab, temp, Quaternion.identity);
            trail.transform.SetParent(transform);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 temp = Input.mousePosition;
            temp.z = 10f;
            trail.transform.position = cam.ScreenToWorldPoint(temp);
            trail.time = 5;
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckSymbol();
            Destroy(trail.gameObject);
        }

    }
    private void StartCheckSymbol()
    {
        TrailRecorded = new Vector3[MAX_POSITIONS];
    }

    private void CheckSymbol()
    {
        int numberOfPositions = trail.GetPositions(TrailRecorded);
        /*
         0                      0
         |                      |
         |                      |
         |                      |
         |                      |
         1---------    ---------1
        y0 > y1         y0 > y1
        &&              &&
        x1 > x2         x1 < x2
            x3 == Vector.zero
         */

        if(TrailRecorded[0].y > TrailRecorded[1].y && TrailRecorded[1].x > TrailRecorded[2].x)
        {
            Debug.Log("|_");
            spells.UseSkills(1, TrailRecorded[1]);
        }
        if (TrailRecorded[0].y > TrailRecorded[1].y && TrailRecorded[1].x < TrailRecorded[2].x)
        {
            Debug.Log("_|");
            spells.UseSkills(2, TrailRecorded[1]);
        }
    }
}
