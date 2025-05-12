using System.Data.Common;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Shop;
using Guns;

namespace PlayerControls {

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    public float health = 500;
    [SerializeField]
    public float maxHealth = 500;
    [SerializeField]
    UnityEngine.UI.Image healthBar;

    [SerializeField]
    int money;
    int totalMoney = 0;
    [SerializeField]
    TextMeshProUGUI moneyText;

    public int pistolAmmo = 100;
    public int shotgunAmmo = 20;
    public int sniperAmmo = 10;
    public readonly static int maxPistolAmmo = 250;
    public readonly static int maxShotgunAmmo = 50;
    public readonly static int maxSniperAmmo = 25;
    [SerializeField]
    TextMeshProUGUI ammoDisplay;

    public bool hasShotgun = false;
    public bool hasSniper = false;

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
    int boostStacks = 0;
    [SerializeField]
    float boostTime = 30;
    public bool boosted = false;
    float boostedMult = 1; //Actual variable for calculations
    [SerializeField]
    float boostAmountValue = 2;

    [SerializeField]
    float shopRange = 10;
    public bool shopping = false;

    bool firstPerson = false;

    [SerializeField]
    Guns.GunController myGun;
    float gunRotation;

    ShopManager curShop = null;

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

        transform.Find("Gun").GetComponent<GunController>().Init();
    }

    void Update() {

        if (interactAction.triggered) {

            if (shopping) {
                StopShopping();
            } else if (driving || !CheckStore()) { //if not driving check for shop
                DriveToggle();
            }
        }

        if (shopping) {return;}

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

        //Choose Gun
        if (Input.GetKey(KeyCode.Alpha1)) {
            myGun.SetGun(Guns.GunController.GunType.Pistol);
        } else if (Input.GetKey(KeyCode.Alpha2) && hasShotgun) {
            myGun.SetGun(Guns.GunController.GunType.Shotgun);
        } else if (Input.GetKey(KeyCode.Alpha3) && hasSniper) {
            myGun.SetGun(Guns.GunController.GunType.Sniper);
        }

        //Point Gun
        gunRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        gunRotation = Mathf.Clamp(gunRotation, myCamera.maxUp, myCamera.maxDown);
        myGun.transform.localEulerAngles = new Vector3(gunRotation, 0, 0);

        //Skip shooting
        if (!(driving || shopping)) {

            //Shooting
            if (attackAction.triggered) {
                switch (myGun.curGun) {
                case Guns.GunController.GunType.Pistol:
                    if (pistolAmmo > 0 && myGun.Shoot()) {
                        pistolAmmo--;
                    }
                    break;
                case Guns.GunController.GunType.Shotgun:
                    if (shotgunAmmo > 0 && myGun.Shoot()) {
                        shotgunAmmo--;
                    }
                    break;
                case Guns.GunController.GunType.Sniper:
                    if (sniperAmmo > 0 && myGun.Shoot()) {
                        sniperAmmo--;
                    }
                    break;
                }
            }
        }

    }


    void LateUpdate() {
        //Update Healthbar and Money
        healthBar.fillAmount = health/maxHealth;
        moneyText.text = money.ToString();

        switch (myGun.curGun) {
            case Guns.GunController.GunType.Pistol:
                ammoDisplay.text = pistolAmmo.ToString();
                break;
            case Guns.GunController.GunType.Shotgun:
                ammoDisplay.text = shotgunAmmo.ToString();
                break;
            case Guns.GunController.GunType.Sniper:
                ammoDisplay.text = sniperAmmo.ToString();
                break;
        }
    }


    void FixedUpdate()
    {
        if (!driving && !shopping) {

            //Movement
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            Vector3 moveValue = new Vector3(moveInput.x, 0, moveInput.y);

            if (rb.linearVelocity.magnitude < maxSpeed * boostedMult) {
                rb.AddForce(transform.rotation*(moveAcceleration*1000*Time.deltaTime*moveValue * boostedMult),ForceMode.Force);
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

        if (PlayerPrefs.HasKey("HighScore")) {
            if (totalMoney > PlayerPrefs.GetInt("HighScore")) {
                PlayerPrefs.SetInt("HighScore",totalMoney);
            }
        } else {
            PlayerPrefs.SetInt("HighScore",totalMoney);
        }

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

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, carRange);

        float carDist = -1;
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Car")) {
                if (myCar == null) {
                    myCar = hitCollider.GetComponent<CarController>();
                    carDist = Vector3.Distance(transform.position, hitCollider.transform.position);
                } else {
                    float newCarDist = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (newCarDist < carDist) {
                        myCar = hitCollider.GetComponent<CarController>();
                        carDist = newCarDist;
                    }
                }
            }
        }

        if (myCar != null) {

            UnZoom();

            driving = true;
            rb.linearVelocity = new Vector3();
            //myCollider.enabled = false;
            Physics.IgnoreLayerCollision(6, 8, true);
            myCar.Enter();
        }

    }

    void Exit() {
        driving = false;
        transform.Translate(0,3,0);
        //myCollider.enabled = true;
        myCar.Exit();

        myCar = null;

        Physics.IgnoreLayerCollision(6, 8, false);
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

    bool CheckStore() {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shopRange);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Shop")) {
                StartShopping(hitCollider.GetComponent<ShopManager>());
                return true;
            }
        }

        return false;
    }

    void StartShopping(ShopManager shop) {
        shop.OpenShop();
        curShop = shop;
        shopping = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StopShopping() {
        curShop.CloseShop();
        curShop = null;
        shopping = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GainAmmo(Guns.GunController.GunType gun, int amount) {
        switch (gun) {
            case Guns.GunController.GunType.Pistol:
                pistolAmmo += amount;
                if (pistolAmmo > maxPistolAmmo) {
                    pistolAmmo = maxPistolAmmo;
                }
                break;
            case Guns.GunController.GunType.Shotgun:
                shotgunAmmo += amount;
                if (shotgunAmmo > maxShotgunAmmo) {
                    shotgunAmmo = maxShotgunAmmo;
                }
                break;
            case Guns.GunController.GunType.Sniper:
                sniperAmmo += amount;
                if (sniperAmmo > maxSniperAmmo) {
                    sniperAmmo = maxSniperAmmo;
                }
                break;
        }
    }

    public void GainBoost() {
        maxHealth -= 10;

        if (health > maxHealth) {
            health = maxHealth;
            if (health <= 0) {
                Die();
                return;
            }
        }

        boostStacks++;
        boosted = true;
        boostedMult = boostAmountValue;
        Invoke("BoostEnd",boostTime);

    }

    void BoostEnd() {
        boostStacks--;
        if (boostStacks == 0) {
            boosted = false;
            boostedMult = 1;
        }
    }

    public void GainMoney(int amount) {
        money += amount;
        totalMoney += amount;
    }

    public bool SpendMoney(int amount) {
        if (amount > money) {
            return false;
        }

        money -= amount;
        return true;
    }

}

}