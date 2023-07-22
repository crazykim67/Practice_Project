using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public CharacterController ch;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -10f;

    private InputManager input;
    private Transform cameraTransform;

    [SerializeField]
    private Transform groundRayTr;
    [SerializeField]
    private float rayDistance;

    private void Start()
    {
        ch = GetComponent<CharacterController>();
        input = InputManager.Instance;
        cameraTransform = Camera.main.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //groundedPlayer = ch.isGrounded;
        if (isGround() && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        Movement();

        if (input.OnJump() && isGround())
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        ch.Move(playerVelocity * Time.deltaTime);

        OnChangeSpeed();
    }

    public bool isGround()
    {
        int layer = (-1) - (1 << LayerMask.NameToLayer("Player"));

        RaycastHit hit;
        bool isGrounded = false;

        //Debug.DrawRay(groundRayTr.position, Vector3.down * rayDistance, Color.red);
        if(Physics.Raycast(groundRayTr.position, Vector3.down, out hit, rayDistance, layer))
        {
            if (hit.transform != null)
                isGrounded = true;
            else
                isGrounded = false;
        }

        return isGrounded;
    }

    public void Movement()
    {
        Vector2 movement = input.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;

        move = move.normalized;
        ch.Move(move * Time.deltaTime * playerSpeed);
    }

    public void OnChangeSpeed()
    {
        if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
            playerSpeed = 3f;
        else if (UnityEngine.Input.GetKeyUp(KeyCode.LeftShift))
            playerSpeed = 2f;
    }
}
