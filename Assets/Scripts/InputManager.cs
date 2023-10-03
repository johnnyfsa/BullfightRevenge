using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputActions playerInput;
    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();
        playerInput.Player.ActionButton.performed += ActionButton_performed;
    }

    private void ActionButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Action button pressed");
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = new Vector2(0, 0);

        inputVector = playerInput.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        // print(inputVector);
        return inputVector;
    }



}
