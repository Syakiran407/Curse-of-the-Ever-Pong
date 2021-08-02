using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerController controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;

    bool crouch = false;

    //private properties
    private float startingRunSpeed;
    private bool startingjump;
    private float startingJumpForce;
    private bool startingcrouch;

    private void Start()
    {
        startingcrouch = crouch;
        startingjump = jump;
        startingJumpForce = GetComponent<PlayerController>().m_JumpForce;
        startingRunSpeed = runSpeed;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            crouch = true;
        } else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            crouch = false;
        }
    }

    public void ReinitialiseStats()
    {
        runSpeed = startingRunSpeed;
        jump = startingjump;
        GetComponent<PlayerController>().m_JumpForce = startingJumpForce;
        crouch = startingcrouch;
    }

    public void StopMovement()
    {
        runSpeed = 0;
        jump = false;
        crouch = false;
        GetComponent<PlayerController>().m_JumpForce = 0;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;


    }
}
