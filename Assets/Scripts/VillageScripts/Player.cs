using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables
    [SerializeField] public int maxHealth = 10;
    [SerializeField] public int currentHealth = 10;
    [SerializeField] float speed = 2f;
    private Camera mainCamera;
    Animator animator;
    public healthslide Healthslide;
    public bool NoHealth = false;


    private void Start()
    {
        //sets health and components
        currentHealth = maxHealth;
        Healthslide.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();

    }

    private void Update()
    {
        //uses Input to get return of virtual axis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //sets new Vector3 using the x and z Vectors for movement
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        //checks is there is any length higher than 0
        if (moveDirection.magnitude > 0)
        {
            //if so Normalise to prevent angular speed increase and set and sets Vector as current * speed * deltatime.
            //then tanslates this into movement in the given world
            moveDirection.Normalize();
            moveDirection *= speed * Time.deltaTime;
            transform.Translate(moveDirection, Space.World);
        }

        //creation of new floats given by the dot product of the movedirection and forward and right transforms
        float velocityZ = Vector3.Dot(moveDirection.normalized, transform.forward);
        float velocityX = Vector3.Dot(moveDirection.normalized, transform.right);
        //sets the floats used by the blend tree to activate animations based on movement
        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", velocityX,.1f, Time.deltaTime);

        //creates a ray from the main camera to a point of the screen set by the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //creates a plane within the world using normal of up and set at  0,0,0. means that the ray will be able to cast on orthogonal view of up.
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float to hold length of ray
        float rayLength;
        //Casts ray from camera to the flat plane and produce a length, if it exits
        if(groundPlane.Raycast(ray, out rayLength))
        {
            //Create a vector3 of the distance point of the raylength
            Vector3 pointToLook = ray.GetPoint(rayLength);
            Debug.DrawLine(ray.origin, pointToLook, Color.red);
            //rotate the player character in the direction of the Vector point. 
            //Creates the ability to rotate and aim the character to fire in any rotation from the forward position
            transform.LookAt(pointToLook);
        }
    }
    public void Hit()
    {
        //reduce current health, set the health slider as new current. if health <= 0 then set player to false.
        currentHealth--;
        Healthslide.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Debug.Log("should die");
            FindObjectOfType<GameManager>().EndGame();

        }

    }

}
