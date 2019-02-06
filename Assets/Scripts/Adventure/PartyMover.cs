using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public partial class PartyMover : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 3.0f;
    [SerializeField] float reverseSpeed = 2.0f;
    [SerializeField] float strafeSpeed = 2.0f;
    [SerializeField] float turnSpeed = 10.0f;
    [SerializeField] float lookSpeed = 3;
    [SerializeField] float gravity = 10.0f;

    private Camera mainCamera;
    private CharacterController controller;
    private new Transform transform;
    private bool movementEnabled = true;
    private bool mouseLookEnabled = false;
    private Vector2 lookRotation = Vector2.zero;


    private void Awake()
    {
        transform = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    public void EnableControl()
    {
        mouseLookEnabled = true;
        movementEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableControl()
    {
        mouseLookEnabled = false;
        movementEnabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (movementEnabled == true)
        {
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                mouseLookEnabled = !mouseLookEnabled;

                if (Cursor.lockState == CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.None;
                else if (Cursor.lockState == CursorLockMode.None)
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void FixedUpdate()
    {
        if (movementEnabled == true)
        {
            Vector3 moveDirection = transform.forward;

            if (controller.isGrounded)
            {
                Vector3 angles = transform.eulerAngles;
                Vector3 forward = Vector3.zero;
                Vector3 strafe = Vector3.zero;

                angles.y += (Input.GetAxis("Turn") * turnSpeed);
                transform.eulerAngles = angles;

                if (Input.GetAxis("Vertical") > 0)
                {
                    forward = Vector3.forward * forwardSpeed;
                }
                else if (Input.GetAxis("Vertical") < 0)
                {
                    forward = -Vector3.forward * reverseSpeed;
                }

                if (Input.GetAxis("Horizontal") > 0)
                {
                    strafe = Vector3.right * strafeSpeed;
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    strafe = -Vector3.right * strafeSpeed;
                }

                moveDirection = forward + strafe;
                moveDirection = transform.TransformDirection(moveDirection);
            }

            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
            moveDirection.z = moveDirection.z * Time.deltaTime;
            moveDirection.x = moveDirection.x * Time.deltaTime;

            controller.Move(moveDirection);

            if (mouseLookEnabled == true)
            {
                Look();
            }
        }
    }

    public void Look() // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {
        lookRotation.y += Input.GetAxis("Mouse X");
        lookRotation.x += -Input.GetAxis("Mouse Y");
        lookRotation.x = Mathf.Clamp(lookRotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, lookRotation.y) * lookSpeed;
        mainCamera.transform.localRotation = Quaternion.Euler(lookRotation.x * lookSpeed, 0, 0);
    }
}