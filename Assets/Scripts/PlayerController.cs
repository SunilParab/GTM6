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
    float sensitivity = 5;

    public int score = 0;

    Collider collider;

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

        collider = GetComponent<Collider>();
    }

    void Update() {

        if (interactAction.triggered) {
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

        //Rotate to mouse
        float rotateHorizontal = Input.GetAxis("Mouse X");
        //Turn player
        transform.Rotate(Vector3.up * rotateHorizontal * sensitivity, Space.Self);

        if (secondaryAction.IsPressed()) { //Unturn model
            model.transform.Rotate(Vector3.up * rotateHorizontal * -sensitivity, Space.Self);
        } 

    }

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
        collider.enabled = false;
        car.Enter();
    }

    void Exit() {
        driving = false;
        transform.Translate(0,3,0);
        collider.enabled = true;
        car.Exit();
    }


    void OnTriggerEnter(Collider collision) {
        GameObject other = collision.gameObject;
        score += other.tag.Equals("Coin") ? 5 : -10;
        Destroy(other);
    }


    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y + 0.1f);
    }

}

}