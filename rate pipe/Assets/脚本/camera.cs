using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float transitionSpeed = 2.0f; // 镜头切换速度
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

        // 计算相机的半宽和半高
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
            // 玩家离开了屏幕右侧，切换到右侧的镜头
            mainCamera.transform.position += new Vector3(cameraHalfWidth * 2, 0, 0);
        }
        else if (viewPos.x < 0)
        {
            // 玩家离开了屏幕左侧，切换到左侧的镜头
            mainCamera.transform.position -= new Vector3(cameraHalfWidth * 2, 0, 0);
        }
        else if (viewPos.y > 1)
        {
            // 玩家离开了屏幕上侧，切换到上侧的镜头
            mainCamera.transform.position += new Vector3(0, cameraHalfHeight * 2, 0);
        }
        else if (viewPos.y < 0)
        {
            // 玩家离开了屏幕下侧，切换到下侧的镜头
            mainCamera.transform.position -= new Vector3(0, cameraHalfHeight * 2, 0);
        }
    }
}
