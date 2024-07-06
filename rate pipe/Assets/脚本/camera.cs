using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float transitionSpeed = 2.0f; // ��ͷ�л��ٶ�
    private Camera mainCamera;
    private Transform playerTransform;
    private Vector3 offset;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    void Start()
    {
        mainCamera = Camera.main;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = mainCamera.transform.position - playerTransform.position;

        // ��������İ��Ͱ��
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = mainCamera.aspect * cameraHalfHeight;
    }

    void Update()
    {
        CheckPlayerPosition();
    }

    void CheckPlayerPosition()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(playerTransform.position);

        if (viewPos.x > 1)
        {
            // ����뿪����Ļ�Ҳ࣬�л����Ҳ�ľ�ͷ
            mainCamera.transform.position += new Vector3(cameraHalfWidth * 2, 0, 0);
        }
        else if (viewPos.x < 0)
        {
            // ����뿪����Ļ��࣬�л������ľ�ͷ
            mainCamera.transform.position -= new Vector3(cameraHalfWidth * 2, 0, 0);
        }
        else if (viewPos.y > 1)
        {
            // ����뿪����Ļ�ϲ࣬�л����ϲ�ľ�ͷ
            mainCamera.transform.position += new Vector3(0, cameraHalfHeight * 2, 0);
        }
        else if (viewPos.y < 0)
        {
            // ����뿪����Ļ�²࣬�л����²�ľ�ͷ
            mainCamera.transform.position -= new Vector3(0, cameraHalfHeight * 2, 0);
        }
    }
}
