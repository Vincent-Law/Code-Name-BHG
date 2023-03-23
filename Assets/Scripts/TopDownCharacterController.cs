using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public const float speed = 10f;
        private new Rigidbody2D rigidbody2D;
        private Vector2 moveDir;
        private Vector2 lastMoveDir;
        private Animator animator;

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
          HandleMovement();
        }

        private void HandleMovement()
        {
            float moveX = 0f;
            float moveY = 0f;

            //keyboard inputs
            if (Input.GetKey(KeyCode.W))
            {
                moveY = +1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveY = -1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveX = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveX = +1f;
            }

            //calculating move direction vector
            Vector2 moveDir = new Vector2(moveX, moveY).normalized;

            //test if idle or moving
            bool isIdle = moveX == 0 && moveY == 0;
            if (isIdle) {
                //Idle
                rigidbody2D.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
            else {
                //Moving
                lastMoveDir = moveDir;
                rigidbody2D.velocity = moveDir * speed;
                animator.SetFloat("Horizontal", moveDir.x);
                animator.SetFloat("Vertical", moveDir.y);
                animator.SetBool("isMoving", true);
            }
        }
    }
}
