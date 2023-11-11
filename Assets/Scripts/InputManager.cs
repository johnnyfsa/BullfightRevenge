using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event EventHandler OnActionButtonPressed;
    public event EventHandler OnPauseButtonPressed;
    PlayerInputActions playerInput;
    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();
        playerInput.Player.ActionButton.performed += ActionButton_performed;
        playerInput.Player.Pause.performed += Pause_performed;
        playerInput.UI.Unpause.performed += Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void ActionButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnActionButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = new Vector2(0, 0);

        inputVector = playerInput.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        // print(inputVector);
        return inputVector;
    }
    public void SwitchActiveActionMap()
    {
        if (playerInput.Player.enabled)
        {
            playerInput.Player.Disable();
            playerInput.UI.Enable();
        }
        else
        {
            playerInput.Player.Enable();
            playerInput.UI.Disable();
        }

    }
}
