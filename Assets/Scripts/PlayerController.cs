using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private float sensitivity;

    [SerializeField] private float zBounds = -25f; // Prevent the player from walking too far into the lanes


    //Player Main Camera
    public GameObject _camera;
    private float yRotation = 0;
    private float xRotation = 0;

    //jump
    private CharacterController cc;
    private bool isJump;
    private bool isMove;

    private float y;

    public float jumpSpeed = 5;             //跳跃的速度
    public float gravity = 9.81f;               //重力

    private Vector3 moveDirection;
    private CollisionFlags flags;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cc = GetComponent<CharacterController>();
    }
    /// <summary>
    /// Character movement
    /// </summary>
    void Update()
    {
        yRotation += Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        xRotation -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -60, 80);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        _camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        y -= gravity * Time.deltaTime;
        if (transform.position.y <= 1.5) y = 0;

        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            isMove = true;
        }
        //
        if (Input.GetButton("Jump") && !isJump)
        {
            isJump = true;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.y = jumpSpeed;
        }
        // Fall if in midair or if in mid jump
        if (isJump || cc.Move(-transform.up * Time.deltaTime) != CollisionFlags.Below)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            flags = cc.Move(moveDirection * Time.deltaTime);

            if (flags == CollisionFlags.Below)
            {
                isJump = false;
                moveDirection.y = 0;
            }
        }
        if (isMove)
        {
            cc.Move(transform.forward * z * moveSpeed * Time.deltaTime);
            cc.Move(transform.right * x * moveSpeed * Time.deltaTime);
            isMove = false;
        }
        // Clamp movement
        float newZ = Mathf.Clamp(transform.position.z, -100, zBounds);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        //Return to Main Menu using 'ESC'
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    
}
