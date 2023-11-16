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
    private static InputManager instance;

    public static InputManager Instance { get => instance; private set => instance = value; }

    private void Awake()
    {
        // Ensure that only one instance of the singleton exists.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();
        playerInput.Player.ActionButton.performed += ActionButton_performed;
        playerInput.Player.Pause.performed += Pause_performed;
        playerInput.UI.Unpause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInput.Player.ActionButton.performed -= ActionButton_performed;
        playerInput.Player.Pause.performed -= Pause_performed;
        playerInput.UI.Unpause.performed -= Pause_performed;
        playerInput.Dispose();
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
