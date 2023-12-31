using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject objectPanel;
    public TextMeshProUGUI objectText;
    void Start()
    {
        objectText.text = "";
    }

    void Update()
    {
        getObject();
        inventoryOpen();
    }

    public void getObject()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            
            // Raycast 마우스 포인트의 위치를 알려준다.
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // Ray를 쐈을 때 맞는 변수
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Clue"))
            {
                objectPanel.SetActive(true);
                // Raycast 맞은 오브젝트의 이름을 String 변수에 저장
                string objectName = hit.transform.name;
                objectText.text = "오브젝트의 이름 : " + objectName;
            }
            else
            {
                // 맞는게 없다면 초기화
                objectText.text = "";
                objectPanel.SetActive(false);
            }
        }
    }

    public void inventoryOpen()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (objectPanel.activeSelf)
            {
                objectPanel.SetActive(false);
            } else
            {
                objectPanel.SetActive(true);
            }
                
        }
    }

}
