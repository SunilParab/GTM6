using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControls {

public class PlayerController : MonoBehaviour
{
    //Movement Variables
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    GameObject model;
    [SerializeField]
    CarController car;
    [SerializeField]
    float moveAcceleration = 500;
    [SerializeField]
    float maxSpeed = 500;
    [SerializeField]
    float jumpForce = 50;
    [SerializeField]
    float sensitivity = 250;

    Collider myCollider;

    [SerializeField]
    bool driving = false;
    
    //Input systems
    InputAction moveAction;
    InputAction jumpAction;
    InputAction secondaryAction;
    InputAction interactAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        secondaryAction = InputSystem.actions.FindAction("Secondary");
        interactAction = InputSystem.actions.FindAction("Interact");

        myCollider = GetComponent<Collider>();
    }

    void Update() {

        if (interactAction.triggered) {Debug.Log("hds");
            driveToggle();
        }

        if (!driving) {
            //Jumping
            if (jumpAction.triggered && IsGrounded()) {
                rb.AddForce(jumpForce*Vector3.up,ForceMode.Impulse);
            }
        } else {
            transform.position = car.transform.position;
        }

        if (!secondaryAction.IsPressed()) {
            //Turn player
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, Space.Self);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!driving) {

            //Movement
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            Vector3 moveValue = new Vector3(moveInput.x, 0, moveInput.y);

            if (rb.linearVelocity.magnitude < maxSpeed) {
                rb.AddForce(transform.rotation*(moveAcceleration*Time.deltaTime*moveValue),ForceMode.Force);
            }
 
        }
    }
    

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y + 1.1f);
    }


    //Driving
    void driveToggle() {
        if (driving) {
            Exit();
        } else {
            Enter();
        }
    }

    void Enter() {
        driving = true;
        rb.linearVelocity = new Vector3();
        myCollider.enabled = false;
        car.Enter();
    }

    void Exit() {
        driving = false;
        transform.Translate(0,3,0);
        myCollider.enabled = true;
        car.Exit();
    }

}

}