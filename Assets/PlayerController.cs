using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private float sensitivity;

    [SerializeField] private float moveBounds;

    [Header("References")]
    [SerializeField] private GameController gameController;

    private float yRotation = 0;
    private float xRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    bool lockCamera = false;

    void Update()
    {
        if (!gameController.IsLocked())
        {
            // Camera
            float mouseX = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;
            float mouseY = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;

            xRotation = Mathf.Clamp(xRotation, -60, 60);
            yRotation = Mathf.Clamp(yRotation, -45, 45);

            yRotation += mouseY;
            xRotation -= mouseX;

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            // Movement
            int x = 0, z = 0;

            if (Input.GetKey(KeyCode.W)) z += 1;
            else if (Input.GetKey(KeyCode.S)) z -= 1;
            if (Input.GetKey(KeyCode.A)) x -= 1;
            else if (Input.GetKey(KeyCode.D)) x += 1;

            Vector3 velocity = new Vector3(x, 0, 0);

            float newXTransform = Mathf.Clamp(transform.position.x + (velocity.x * Time.deltaTime * moveSpeed), -moveBounds, moveBounds);

            transform.position = new Vector3(newXTransform, transform.position.y, transform.position.z);
        }
        
        // Lock the movement controls when begining a shot
        if (Input.GetKeyDown(KeyCode.Mouse1)) gameController.SwitchLocked();
    }
}
