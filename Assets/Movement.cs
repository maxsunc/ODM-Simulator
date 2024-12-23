using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform cam;
    public CharacterController controller;
    public float moveSpeed;
    [HideInInspector]
    public float copySpeed;
    public bool enabledMovement;
    public float turnSmoothTime = 0.1f;
    float smoothVelocity;
    public GameObject ODM;
    [HideInInspector]
    public Vector3 move;
    public Transform groundCheck;
    public LayerMask targetLayer;
    private Animator playerAnimator;
    private Animator swordAnimator;

    private Vector3 _velocity;
    private bool gravity = true;
    private bool cursorLock = true;
    // reset x rotation whenever we hit the ground (trigger)
    private bool resetXRot = false;
    

    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        swordAnimator = this.gameObject.transform.GetChild(0).GetChild(3).GetComponent<Animator>();

        copySpeed = moveSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        if (enabledMovement)
        {
            Vector3 dir = new Vector3(move.x, 0f, move.z).normalized;

            if(dir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);

                playerAnimator.SetBool("isRunning", true);
                swordAnimator.SetBool("isRunning", true);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                playerAnimator.SetBool("isRunning", false);
                swordAnimator.SetBool("isRunning", false);
            }
        }
        if (gravity)
        {
            Gravity();
        }

      

    }

    void Gravity()
    {
        _velocity.y += 9.8f * Time.fixedDeltaTime;
        _velocity.y = Mathf.Clamp(_velocity.y, 0f,78.4f);
        controller.Move(-_velocity * Time.fixedDeltaTime);

    }

    public void SlowDownGravity(float value)
    {
        _velocity.y -= value;
    }


    public void ResetGravity(bool option)
    {

        _velocity.y = 0;
        //reset gravity
        // stop doing gravity
        gravity = option;
    }

    public bool CheckGround()
    {
        Collider[] coli = Physics.OverlapBox(groundCheck.position, new Vector3(0.1f, 0.2f, 0.1f), Quaternion.identity, targetLayer);
        if(coli.Length >= 1) 
        {
            if (!resetXRot)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                resetXRot = true;
            }
        return true;
        }
        resetXRot = false;
        return false;

    
   }


    public void ChangeCursorState(bool state)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

}
