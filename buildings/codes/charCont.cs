using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CharacterController))]
public class charCont : MonoBehaviour
{
    public bool control;
    float speed = 6.0f;
    public float walkSpeed, runSpeed;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    public KeyCode runKey = KeyCode.LeftShift;
    public float mouseSensitivity = 100.0f;
    private float xRotation = 0.0f;
    private bool running;
    Animator animator;
    public Transform lookTarget,posTarget;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (control)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            speed = running ? runSpeed : walkSpeed;
            animator.SetBool("walk", moveX != 0 || moveZ != 0);


            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            running = (Input.GetKey(runKey) && Input.GetAxis("Vertical") > 0);



            move = transform.right * moveX + transform.forward * moveZ;
            controller.Move(move * speed * Time.deltaTime);


            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }


            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);


            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;



        

        if (lookTarget == null)
        {
            if (cameraTransform.localEulerAngles.y != 0)
            {
                transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            }
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(transform.up * mouseX);
        }
        else
        {
            Vector3 direction = lookTarget.position - cameraTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, 5 * Time.deltaTime);
            xRotation = cameraTransform.localEulerAngles.x;
        }
        
        if (posTarget != null && !control)
        {
            transform.position = Vector3.Lerp(transform.position,posTarget.position, 5 * Time.deltaTime);
        }
    }
    
}