using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform orientation;
    //mine
    public CameraChanges camerachanges;
    PlayerMovement playerMovement;
    bool jumpedAlreadyAfterWall = false;


    [Header("Detection")]
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;

    [Header("Wall Running")]
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunJumpForce;
    [SerializeField] LayerMask wallrunLayer;
    [SerializeField] RaycastHit wallrunHitRay;

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
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight , wallrunLayer);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        //fov = camerachanges.MinFov;
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right.normalized, out leftWallHit, wallDistance, wallrunLayer);
        wallRight = Physics.Raycast(transform.position, orientation.right.normalized, out rightWallHit, wallDistance, wallrunLayer);
        wallBack = Physics.Raycast(transform.position, -orientation.forward, out wallBackHit, wallDistance, wallrunLayer);


    }

    private void Update()
    {
        //walljust double jump
        
       

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
        rb.useGravity = true;
        Physics.gravity = new Vector3(0, -15f, 0);
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);
        camerachanges.CameraFovChangUp();

        if (wallLeft)
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if (wallRight)
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)   
            {
                //Vector3 wallJumpMultiplier = new Vector3(0, -1f) - transform.up;
                Vector3 wallRunJumpDirection = transform.up /*- wallJumpMultiplier */+ leftWallHit.normal + orientation.forward;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 1, ForceMode.Impulse);
                playerMovement.doubleJumps += 1;
            }
            else if (wallRight)
            {
                //Vector3 wallJumpMultiplier = new Vector3(0, -1f);
                Vector3 wallRunJumpDirection = transform.up /*- wallJumpMultiplier*/ + rightWallHit.normal + orientation.forward;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 1, ForceMode.Impulse);
                playerMovement.doubleJumps += 1;

            }

        }

        
    }

    void StopWallRun()
    {
        rb.useGravity = true;

       // cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}