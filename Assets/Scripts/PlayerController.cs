using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float speed = 10f;
    public float jumpHeight = 3f;

    public float gravity = -9.81f;
    private Vector3 velocity;

    public Transform groundCheck;
    private bool isGrounded;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //touch grass?
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) { velocity.y = -2; }

        //Get Player Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //playermovement
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        //boing boing weeeeee
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) { velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
