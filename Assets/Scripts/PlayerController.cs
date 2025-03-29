using System.Data.Common;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PlayerControls {

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float health = 500;
    [SerializeField]
    float maxHealth = 500;
    [SerializeField]
    UnityEngine.UI.Image healthBar;

    public double money;
    [SerializeField]
    TextMeshProUGUI moneyText;

    public static PlayerController reference;

    //Movement Variables
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    GameObject model;
    [SerializeField]
    Collider myCollider;
    [SerializeField]
    CameraController myCamera;
    CarController myCar;
    [SerializeField]
    float moveAcceleration = 500;
    [SerializeField]
    float maxSpeed = 500;
    [SerializeField]
    float jumpForce = 50;
    [SerializeField]
    float sensitivity = 250;

    [SerializeField]
    float carRange = 10;
    bool driving = false;

    [SerializeField]
    bool firstPerson = false;

    [SerializeField]
    Guns.GunController myGun;
    float gunRotation;
    
    //Input systems
    InputAction moveAction;
    InputAction jumpAction;
    InputAction secondaryAction;
    InputAction interactAction;
    InputAction attackAction;
    InputAction zoomAction;

    void Awake()
    {
        reference = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        secondaryAction = InputSystem.actions.FindAction("Secondary");
        interactAction = InputSystem.actions.FindAction("Interact");
        attackAction = InputSystem.actions.FindAction("Attack");
        zoomAction = InputSystem.actions.FindAction("Zoom");

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {

        if (interactAction.triggered) {
            DriveToggle();
        }

        if (!driving) {

            //First Person
            if (zoomAction.triggered) {
                if (firstPerson) {
                    UnZoom();
                } else {
                    Zoom();
                }
            }

            //Jumping
            if (jumpAction.triggered && IsGrounded()) {
                rb.AddForce(jumpForce*Vector3.up,ForceMode.Impulse);
            }

        } else {
            transform.position = myCar.transform.position;
        }

        if (!secondaryAction.IsPressed()) {
            //Turn player
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, Space.Self);
        }


        //Point Gun
        gunRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        gunRotation = Mathf.Clamp(gunRotation, myCamera.maxUp, myCamera.maxDown);
        myGun.transform.localEulerAngles = new Vector3(gunRotation, 0, 0);

        //Shooting
        if (attackAction.triggered) {
            myGun.Shoot();
        }

        if (Input.GetKey(KeyCode.Alpha1)) {
            myGun.SetGun(Guns.GunController.GunType.Pistol);
        } else if (Input.GetKey(KeyCode.Alpha2)) {
            myGun.SetGun(Guns.GunController.GunType.Shotgun);
        } else if (Input.GetKey(KeyCode.Alpha3)) {
            myGun.SetGun(Guns.GunController.GunType.Sniper);
        }

        //Update Healthbar
        healthBar.fillAmount = health/maxHealth;

        moneyText.text = money.ToString();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!driving) {

            //Movement
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            Vector3 moveValue = new Vector3(moveInput.x, 0, moveInput.y);

            if (rb.linearVelocity.magnitude < maxSpeed) {
                rb.AddForce(transform.rotation*(moveAcceleration*1000*Time.deltaTime*moveValue),ForceMode.Force);
            }
 
        }
    }
    

    //Internal Functions
    public void TakeDamage(float damage) {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        SceneManager.LoadScene("Restart Menu");
        Cursor.lockState = CursorLockMode.None;
    }

    public void Heal(float amount) {
        health += amount;

        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y + 1.1f);
    }


    //Driving
    void DriveToggle() {
        if (driving) {
            Exit();
        } else {
            Enter();
        }
    }

    void Enter() {

        UnZoom();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, carRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Car")) {
                myCar = hitCollider.GetComponent<CarController>();
                break;
            }
        }

        if (myCar != null) {
            driving = true;
            rb.linearVelocity = new Vector3();
            myCollider.enabled = false;
            myCar.Enter();
        }

    }

    void Exit() {
        driving = false;
        transform.Translate(0,3,0);
        myCollider.enabled = true;
        myCar.Exit();

        myCar = null;
    }


    //Zoom behaviors
    void Zoom() {
        firstPerson = true;
        model.SetActive(false);
        myCamera.Zoom();
    }

    void UnZoom() {
        firstPerson = false;
        model.SetActive(true);
        myCamera.UnZoom();
    }

}

}