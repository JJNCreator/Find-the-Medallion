using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public delegate void OnInteractionButtonPressed();
    public static event OnInteractionButtonPressed onInteraction;

    public float moveSpeed = 5;
    public float runSpeed = 15;
    public float rotateSpeed = 15;
    public bool canSeeInvisible = false;
    public bool isRunning = false;
    
    private float originalMoveSpeed;

    private Animator aniCom;
    private Rigidbody rb;
    private Transform transformCache;
    
    private Vector3 moveDirection;
    
    private Vector3 velocity;

    private void Awake()
    {
        aniCom = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        transformCache = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalMoveSpeed = moveSpeed;
    }
    private void Update()
    {
        if (Input.GetButtonUp("Run"))
        {
            isRunning = !isRunning;
        }

        if(Input.GetButtonUp("Interact"))
        {
            Debug.Log("Interact");
            onInteraction();
        }
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }
        
        velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;

        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);
       
        transformCache.LookAt(transformCache.position + new Vector3(moveDirection.x, 0f, moveDirection.z));
        rb.velocity = moveDirection;

        aniCom.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
        aniCom.SetBool("isRunning", isRunning);
    }
}
