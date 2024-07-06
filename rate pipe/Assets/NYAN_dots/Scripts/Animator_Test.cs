using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NYAN_dots
{
    public class Animator_Test : MonoBehaviour
    {
        public int walkSpd = 5;
        public int runSpd = 10;
        public float jumpForce = 10f;
        public float spd = 0f;
        private bool isGrounded = false;
        private bool isJumping = false;
        private Rigidbody2D rb2;
        private Animator amt;
        public Transform groundCheck;
        public float checkRadius;
        public LayerMask groundLayer;

        private Vector3 originalScale;

        void Start()
        {
            rb2 = GetComponent<Rigidbody2D>();
            amt = GetComponent<Animator>();
            originalScale = transform.localScale;
        }

        void Update()
        {
            // 检测是否接地 (Check if grounded)
            bool wasGrounded = isGrounded;
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

            // 移動処理 (Movement processing)
            float inputX = Input.GetAxisRaw("Horizontal");
            bool inputSpc = Input.GetButton("Fire3");
            bool inputJump = Input.GetButtonDown("Jump");

            // 速度を追加
            rb2.velocity = new Vector2(spd * inputX, rb2.velocity.y);

            // アニメ処理 (Animation processing)
            if (inputX == 0 && rb2.velocity.y == 0)
            {
                amt.SetBool("walk", false);
                amt.SetBool("run", false);
            }
            else
            {
                // 输入方向传递给动画
                amt.SetFloat("dirX", inputX);

                // 步行
                amt.SetBool("walk", true);
                amt.SetBool("run", false);
                spd = walkSpd;

                // 奔跑
                if (inputSpc)
                {
                    amt.SetBool("run", true);
                    amt.SetBool("walk", false);
                    spd = runSpd;
                }
            }

            // 跳跃处理 (Jump processing)
            if (inputJump && isGrounded)
            {
                Debug.Log("Jump triggered");
                rb2.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                amt.SetBool("jump", true);
                isJumping = true;
                isGrounded = false;
            }

            // 水平翻转 (Flip character based on direction while jumping)
            if (isJumping)
            {
                if (inputX > 0)
                {
                    transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
                }
                else if (inputX < 0)
                {
                    transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
                }
            }

            // 重置翻转 (Reset flip when landing)
            if (!isJumping)
            {
                transform.localScale = originalScale;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // 着地判定 (Ground detection)
            if (collision.gameObject.CompareTag("ground"))
            {
                isGrounded = true;
                isJumping = false;
                amt.SetBool("jump", false);
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            // 离开地面 (Leave ground)
            if (collision.gameObject.CompareTag("ground"))
            {
                isGrounded = false;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            amt.SetTrigger("blink");
        }
    }
}
