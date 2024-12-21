using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float runningSpeed;
    public float walkingSpeed;
    public float jumpHeight; // Jump Force
    public float gravity; // Gravity Force
    public Transform cameraTransform;
    public bool isRunning;
    public int health;
    public int maxHealth;
    public int killCount;
    public GunController myGun;

    CharacterController myCharacterController;
    float moveX;
    float moveZ;
    float playerRotationY;
    float cameraRotationX;
    Vector3 moveVector;
    float verticalVelocity; // Vertical movement due to jump or gravity
    Vector3 cameraRotationVector;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        health = maxHealth;
        gravity = -9.8f;
        jumpHeight = 1.2f;
        moveSpeed = walkingSpeed;
        myCharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        RotateCamera();
    }

    public void DoDamageToPlayer(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Debug.Log("Dead!");
        }
    }

    void MovePlayer()
    {
        if ( Input.GetKey(KeyCode.LeftShift) )
        {
            isRunning = true;
        }

        if ( Input.GetKeyUp(KeyCode.LeftShift) )
        {
            isRunning = false;
        }

        moveSpeed = isRunning ? runningSpeed : walkingSpeed;

        // Horizontal Movement
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        // Change our moveVector direction from world space to local space
        moveVector = transform.TransformDirection(new Vector3(moveX, 0, moveZ) * moveSpeed);
        
        // Gravity and Jumping
        if (myCharacterController.isGrounded)
        {
            verticalVelocity = 0; // Reset vertical velocity when grounded

            if (Input.GetKey(KeyCode.Space))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // Apply gravity over time
        }

        // Add vertical velocity to movement
        moveVector.y = verticalVelocity;

        // Move the character
        myCharacterController.Move(moveVector * Time.deltaTime);
    }

    void RotatePlayer()
    {
        playerRotationY = Input.GetAxis("Mouse X");
        transform.Rotate(0, playerRotationY, 0);
    }

    void RotateCamera()
    {
        cameraRotationX -= Input.GetAxis("Mouse Y");

        cameraRotationVector = cameraTransform.eulerAngles;
        cameraRotationVector.x = Mathf.Clamp(cameraRotationX, -40, 40);

        cameraTransform.eulerAngles = cameraRotationVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmmoBox"))
        {
            myGun.bulletCount += 30;
            GameObject.Destroy(other.gameObject);
        }

        if (other.CompareTag("MedicalBox"))
        {
            health += 100;
            health = Mathf.Clamp(health, 0, maxHealth);

            GameObject.Destroy(other.gameObject);
        }
    }
}
