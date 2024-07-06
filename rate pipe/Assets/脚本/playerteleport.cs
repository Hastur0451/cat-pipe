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
            // ����Ͷ��ӽ�ɫ��groundCheckλ������
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);

            // ���������ײ�����壬��ʾ����ı�ǩ
            if (hit.collider != null)
            {
                Debug.Log("Below object tag: " + hit.collider.tag);

                // ����Ƿ�����Ground��ǩ
                if (hit.collider.CompareTag("ground"))
                {
                    lastGroundPosition = player.transform.position;
                }

                // ����Ƿ�����Water��ǩ
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
            // �ڱ༭���л������ߣ����ڵ���
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
        }
    }
}
