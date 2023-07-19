using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private Transform cameraTransform;

    private void Start()
    {
        ch = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        groundedPlayer = ch.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Movement();

        if (input.OnJump() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        ch.Move(playerVelocity * Time.deltaTime);

        OnChangeSpeed();
    }

    public void Movement()
    {
        //if (OptionManager.Instance == null)
        //    return;

        Vector2 movement = input.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;

        move = move.normalized;
        ch.Move(move * Time.deltaTime * playerSpeed);
    }

    public void OnChangeSpeed()
    {
        if (InventoryManager.Instance.isUse)
        {
            playerSpeed = 2f;
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
            playerSpeed = 3f;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            playerSpeed = 2f;
    }
}
