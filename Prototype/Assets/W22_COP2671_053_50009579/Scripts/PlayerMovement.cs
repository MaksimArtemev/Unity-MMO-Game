using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]    //used to be able to set the attriburte value from the inspector window
    private float rotationSpeed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float jumpButtonGracePeriod;
    [SerializeField]
    private Transform cameraTransform;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;    //either float or null
    private float? jumpButtonPressedTime;   //either float or null

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //user input for horizontal and vertical axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Making sure magnitude doesnt go above 1
        //getting the magnitude of movement
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;    //to slow Player to half speed
        }

        //controls animation *** blend tree
        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        //only changess based on rotation of the camera around Y-axis
        //passing Y angle using eulerAngles, vector of rotation is y-axis == Vector3.up
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection; 
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        
        //checks if chracter is moving
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }    
    }

    private void OnAnimatorMove() 
    {
        //transform.Translate(movementDirection * magnitude * speed * Time.deltaTime, Space.World);
        // not Time.deltaTime bc SimpleMove() includes frame normalization
        Vector3 velocity = animator.deltaPosition;
        velocity.y = ySpeed * Time.deltaTime;

        characterController.Move(velocity);
    }

    
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;   //hide and lock the cursor to the center of the view
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;     //if app doesnt have focus
        }
    }

}
