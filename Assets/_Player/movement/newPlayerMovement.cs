using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class newPlayerMovement : MonoBehaviour
{
   
    [Header("Rigidbody settings")]
    //rigidbody part
    Rigidbody PlayerRb;
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
    




    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
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
        //Movement
        Move();
        
    }

    void Move()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        // transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
        PlayerRb.AddForce(direction * speed);
    }



    private void FixedUpdate()
    {
        
    }
}
