using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class newPlayerMovement : MonoBehaviour
{
    CameraController camerascript;

    [Header("Rigidbody settings")]
    //rigidbody part
    [SerializeField] Rigidbody PlayerRb;
    [SerializeField] float Mass;
    [SerializeField] float Drag;
    [SerializeField] float AngularDrag;
    [SerializeField] bool Gravity;

    //movement part
    PlayerInput playerInput;
    InputAction moveAction;
    [SerializeField] float speed;
    //-----------------------------------------------//
    [Header("Camera settings")]
    public Camera mainCam;
    public Transform camRot;
    private Vector3 rotcam;



    // Start is called before the first frame update
    void Start()
    {
        //PlayerRb = GetComponent<Rigidbody>();
        PlayerRb.mass = Mass;
        PlayerRb.drag = Drag;
        PlayerRb.angularDrag = AngularDrag;
        PlayerRb.useGravity = Gravity;


        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");


    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, mainCam.transform.up);
        //this.transform.localRotation = Quaternion.Euler(0 , camRot.transform.rotation.eulerAngles.y, 0);
        //transform.Rotate(Vector3.up * camRot.rotation.x);
        //Movement
        Move();

    }

    void Move()
    {
        Vector3 direction = moveAction.ReadValue<Vector3>();
        // transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
        //print(direction);
        direction = transform.TransformDirection(direction);
        PlayerRb.AddForce(direction * speed * Time.deltaTime, ForceMode.Force);
        //haha u gay

    }


    void LateUpdate()
    {
        float cameraYRotation = camRot.eulerAngles.y;

        // Apply the camera's Y rotation to the player
        transform.rotation = Quaternion.Euler(0, cameraYRotation, 0);
    }

    private void FixedUpdate()
    {

    }
}
