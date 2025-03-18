using UnityEngine;

namespace PlayerControls {

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 500;
    [SerializeField]
    float spinCons = 250;
    float verticalRotation;

    [SerializeField]
    float maxUp;
    [SerializeField]
    float maxDown;

    [SerializeField]
    float xOffset;
    [SerializeField]
    float yOffset;
    [SerializeField]
    float radius;

    float startX;
    float startY;
    float startZ;

    void Start()
    {
        startX = transform.localPosition.x;
        startY = transform.localPosition.y;
        startZ = transform.localPosition.z;
    }


        // Update is called once per frame
        void Update()
    {
        verticalRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        verticalRotation = Mathf.Clamp(verticalRotation, maxUp, maxDown);

        transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);

        transform.localPosition = new Vector3(startX, startY + radius * Mathf.Sin(verticalRotation * Mathf.PI / 180), startZ - radius * Mathf.Cos(verticalRotation * Mathf.PI / 180));

        //set y and z

        //transform.RotateAround(gameObject.transform.parent.transform.position,gameObject.transform.parent.transform.right,-verticalRotation*spinCons);

    }
}

}