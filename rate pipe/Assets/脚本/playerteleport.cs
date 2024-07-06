using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NYAN_dots
{
    public class TagDetector : MonoBehaviour
    {
        public Transform groundCheck;
        public float checkDistance = 1.0f;
        public LayerMask groundLayer;
        public GameObject player; // Reference to the player object

        private Vector3 lastGroundPosition;

        void Start()
        {
            lastGroundPosition = player.transform.position;
        }

        void Update()
        {
            // 射线投射从角色的groundCheck位置向下
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);

            // 如果射线碰撞到物体，显示物体的标签
            if (hit.collider != null)
            {
                Debug.Log("Below object tag: " + hit.collider.tag);

                // 检查是否碰到Ground标签
                if (hit.collider.CompareTag("ground"))
                {
                    lastGroundPosition = player.transform.position;
                }

                // 检查是否碰到Water标签
                if (hit.collider.CompareTag("water"))
                {
                    player.transform.position = lastGroundPosition;
                }
            }
            else
            {
                Debug.Log("No object below");
            }
        }

        void OnDrawGizmos()
        {
            // 在编辑器中绘制射线，便于调试
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
        }
    }
}
