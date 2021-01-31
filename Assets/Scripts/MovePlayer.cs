using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Joystick joystick;
    public float speed;
    public float rotationSpeed;

    CharacterController thisPlayer;
    float rotation;

    Animator animator; 
    public int stateMove = -1;


    private void Start()
    {
        animator = GetComponent<Animator>();
        thisPlayer = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerControl();
    }

    private void PlayerControl()
    {
        float v = joystick.Vertical * speed * Time.deltaTime;
        float h = joystick.Horizontal * rotationSpeed * Time.deltaTime;

        Vector3 pos = transform.up * -9.81f * Time.deltaTime + transform.forward * v;

        rotation += h;
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotation, transform.rotation.z);

        thisPlayer.Move(pos);

        Animations(v);
    }

    private void Animations(float move)
    {
        if (move != 0)
        {
            stateMove = 1;
        }
        else
        {
            stateMove = 0;
        }

        animator.SetInteger("StateMove", stateMove);
    }
}
