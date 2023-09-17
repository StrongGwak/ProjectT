using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject objectPanel;
    public Camera mainCamera;
    #region Rotator
    // 회전 속력 (민감도)
    public float mouseSensitivity = 500f;
    // 사용자의 입력에 따른 좌-우 회전 각도를 저장
    private float horizontalAngle;
    // 사용자의 입력에 따른 상-하 회전 각도를 저장
    private float verticalAngle;
    // 상-하 회전 제한 각도 (초기값=90도)  -90 ~ +90
    public float limitAngle = 90f;

    public float scrollSpeed = 10000.0f;

    // 카메라 드래그 속도를 조절하는 변수
    public float dragSpeed = 2f;

    // 마우스 드래그 중인지 여부를 확인하는 변수
    private bool isDragging = false;

    // 마우스 드래그 시작 지점을 저장하는 변수
    private Vector3 dragOrigin;

    private float scroollWheel;
    private float newDistance;
    private float zoomSpeed = 100.0f;
    private float minDistance = 2.0f;
    private float maxDistance = 10.0f;
    private Vector3 cameraDirection;
    #endregion

    void Start()
    {

    }

    void Update()
    {
        if (!objectPanel.activeInHierarchy)
        {
            // 마우스 휠로 줌 인/줌 아웃
            Zoom();
        }
        
    }
    

    void LateUpdate()
    {
        // 마우스가 눌린 상태에서 드래그하면 카메라 이동
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        // 마우스 버튼을 떼면 드래그 종료
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 드래그 중일 때 카메라 이동
        if (isDragging)
        {
            RotateHorizontal();
            RotateVertical();
        }
    }

    //  시점을 좌/우로 회전시키는 함수
    private void RotateHorizontal()
    {
        // 1. 사용자의 입력값 (마우스)
        //  a. 좌/우 이동값
        float horizontal = Input.GetAxis("Mouse X");
        // 2. 마우스 민감도에 따라 입력 값이 변동
        float turnPlayer = horizontal * mouseSensitivity * Time.deltaTime;
        // 3. * 마우스 이동에 따른 좌-우 회전값 누적
        horizontalAngle -= turnPlayer;
        // 4. 누적된 회전 값을 Player의 회전에 반영
        transform.localEulerAngles = new Vector3(0, horizontalAngle, 0);
    }

    // 시점을 상/하로 회전시키는 함수
    private void RotateVertical()
    {
        // 1. 사용자의 입력값을 가져온다. (상-하) ( -1 ~ +1 )
        float vertical = Input.GetAxis("Mouse Y");
        // 2. 마우스 민감도에 따라 입력 값이 변동
        float turnFace = vertical * mouseSensitivity * Time.deltaTime;
        // 3. * 마우스 이동에 따른 상-하 회전값 누적
        verticalAngle -= turnFace;
        // * 상하 회전 각도 제한을 통해 해결한다.
        verticalAngle = Mathf.Clamp(verticalAngle, -limitAngle, limitAngle);
        // 4. 누적된 회전 값을 Face(=Camera)의 회전에 반영
        mainCamera.transform.localEulerAngles = new Vector3(-verticalAngle, 0, 0);
    }

    // 마우스 스크롤 휠 업/다운 시 줌 인/줌 아웃
    private void Zoom()
    {
        // 마우스 휠 입력
        scroollWheel = Input.GetAxis("Mouse ScrollWheel");
        // 거리 값 제한
        cameraDirection = mainCamera.transform.localRotation * Vector3.forward;
        
        // 카메라 위치 변경
        mainCamera.transform.position += cameraDirection * Time.deltaTime * scroollWheel * zoomSpeed;
        
        /*// 카메라와 목표 사이의 거리 계산
        float distance = Vector3.Distance(potentialNewPosition, mainCamera.transform.parent.position);

        // 거리가 최소 및 최대 거리 범위 내에 있는지 확인
        if (distance >= minDistance && distance <= maxDistance)
        {
            // 카메라 위치 변경
            mainCamera.transform.position = potentialNewPosition;
        }*/
    }
}
