﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform orientation;
    //mine
    PlayerMovement playerMovement;
    bool jumpedAlreadyAfterWall = false


    [Header("Detection")]
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;

    [Header("Wall Running")]
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunJumpForce;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float wallRunfov;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float tilt { get; private set; }

    public bool wallLeft = false;
    public bool wallRight = false;
    private bool wallBack = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    RaycastHit wallBackHit;

    private Rigidbody rb;

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right.normalized, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right.normalized, out rightWallHit, wallDistance);
        wallBack = Physics.Raycast(transform.position, -orientation.forward, out wallBackHit, wallDistance);


    }

    private void Update()
    {
        //walljust double jump
        if (wallRight || wallLeft && !playerMovement.isGrounded && jumpedAlreadyAfterWall == false)
        {
            playerMovement.doubleJumps = 0;
            playerMovement.canDoubleJump = false;
            
        }
        else if (!wallRight || !wallLeft && !playerMovement.isGrounded)
        {
            playerMovement.doubleJumps = 1;
            playerMovement.canDoubleJump = false;
            jumpedAlreadyAfterWall = true;
        }


        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {
                StartWallRun();
                Debug.Log("wall running on the left");
            }
            else if (wallRight)
            {
                StartWallRun();
                Debug.Log("wall running on the right");
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

        if (wallLeft)
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if (wallRight)
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallJumpMultiplier = new Vector3(1, 0);
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal + orientation.forward + wallJumpMultiplier;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 25, ForceMode.Force);

            }
            else if (wallRight)
            {
                Vector3 wallJumpMultiplier = new Vector3(-1, 0);
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal + orientation.forward + wallJumpMultiplier;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 25, ForceMode.Force);
            }

        }

        /*if (!wallLeft || !wallRight && !playerMovement.isGrounded)
        {
            float smallJumpCd = 0.3f;
            smallJumpCd -= 1 * Time.deltaTime;
            if (smallJumpCd <= 0)
            {
                playerMovement.canDoubleJump = true;
                playerMovement.doubleJumps = 1;

            }

        }*/
    }

    void StopWallRun()
    {
        rb.useGravity = true;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}