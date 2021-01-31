using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public GameObject skill_1;
    public GameObject skill_2;

    GameObject skills;

    public void UseSkills(int index, Vector3 pos)
    {
        if (skills == null)
        {
            switch (index)
            {
                case (1):
                    skills = Instantiate(skill_1, pos, Quaternion.identity);
                    Destroy(skills, 2);
                    break;
                case (2):
                    skills = Instantiate(skill_2, pos, Quaternion.identity);
                    Destroy(skills, 2);
                    break;
            }
        }
    }
}
