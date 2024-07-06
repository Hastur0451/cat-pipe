using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NYAN_dots
{
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float runSpeed = 10f;
        public float maxJumpHeight = 7f;
        public float minJumpHeight = 2f;
        public float jumpTime = 0.35f;
        private float jumpTimeCounter;
        private bool isJumping;
        private bool isRunning;
        private Rigidbody2D rb2d;
        private Animator anim;

        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            HandleMovement();
            HandleJump();
            UpdateAnimations();
        }

        void HandleMovement()
        {
            float moveInput = Input.GetAxis("Horizontal");
            isRunning = Input.GetKey(KeyCode.LeftShift);
            float currentSpeed = isRunning ? runSpeed : walkSpeed;

            rb2d.velocity = new Vector2(moveInput * currentSpeed, rb2d.velocity.y);

            if (moveInput != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1); // Flip character based on direction
            }
        }

        void HandleJump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb2d.velocity = new Vector2(rb2d.velocity.x, minJumpHeight);
            }

            if (Input.GetButton("Jump") && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, maxJumpHeight);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
        }

        void UpdateAnimations()
        {
            float moveInput = Input.GetAxis("Horizontal");

            anim.SetBool("isWalking", moveInput != 0 && !isRunning);
            anim.SetBool("isRunning", moveInput != 0 && isRunning);
            anim.SetBool("isJumping", !IsGrounded());

            if (moveInput != 0)
            {
                anim.SetFloat("dirX", Mathf.Sign(moveInput));
            }
        }

        bool IsGrounded()
        {
            // Implement your grounded check here, e.g. using raycasts or collision detection
            // This is a placeholder implementation
            return rb2d.velocity.y == 0;
        }
    }
}
