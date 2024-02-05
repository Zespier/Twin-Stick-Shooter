using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 9f;
    public Transform body;
    public PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    /// <summary>
    /// Reads the player inputs on the left joystick to move
    /// </summary>
    private void Movement()
    {
        Vector2 moveValue = playerInputs.Player.Move.ReadValue<Vector2>();
        transform.position += Time.deltaTime * speed * new Vector3(moveValue.x, moveValue.y, 0);
    }

    /// <summary>
    /// Reads the player inputs on the right joystick to rotate
    /// </summary>
    private void Rotation()
    {
        Vector2 lookValue = playerInputs.Player.Look.ReadValue<Vector2>();

        if (lookValue == Vector2.zero) { return; }

        body.up = Vector3.Lerp(body.up, lookValue, 1);
    }
}
