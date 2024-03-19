using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using UnityEngine.EventSystems;

public class PlayerController_ : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private float sensitivity;
    //Player Main Camera
    public GameObject _camera;
    private float yRotation = 0;
    private float xRotation = 0;

    //jum
    private CharacterController cc;
    private bool isJump;
    private bool isMove;
    

    public float jumpSpeed = 4;             //跳跃的速度
    public float gravity = 1;               //重力

    private Vector3 moveDirection;
    private float h = 0;
    private float v = 0;
    private CollisionFlags flags;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cc = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        yRotation += Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        xRotation -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -60, 80);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        _camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            isMove = true;
        }

        if (Input.GetButton("Jump") && !isJump)
        {
            isJump = true;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.y = jumpSpeed;
        }

        if (isJump)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            flags = cc.Move(moveDirection * Time.deltaTime);

            if (flags == CollisionFlags.Below)
            {
                isJump = false;
            }
        }
        if (isMove)
        {
            cc.Move(transform.forward * moveSpeed * Time.deltaTime * Vertical);
            cc.Move(transform.right * moveSpeed * Time.deltaTime * Horizontal);
            
            
            isMove = false;
        }
    }
}
