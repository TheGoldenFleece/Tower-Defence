using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject cameraFixed;
    [SerializeField] PauseMenuUI pauseMenuUI;

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public Vector2 scrollBorder;

    bool movementEnabled;

    private void Start()
    {
        movementEnabled = false;
    }
    private void Update()
    {
        if (GameManager.GameIsOver) this.enabled = false;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseMenuUI.TogglePause();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (pauseMenuUI.pauseMenuUI.activeSelf) return;

            movementEnabled = !movementEnabled;
            cameraFixed.SetActive(!cameraFixed.activeSelf);
        }

        if (!movementEnabled)
        {
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, scrollBorder.x, scrollBorder.y);
        transform.position = pos;
    }

}
