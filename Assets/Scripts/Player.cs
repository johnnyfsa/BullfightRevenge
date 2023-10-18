using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event EventHandler OnLivesChanged;
    [SerializeField] InputManager inputManager;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] int numLives;

    [SerializeField] float speedMultiplyer = 1f;
    [SerializeField] float powerUpDuration = 5f;

    [SerializeField] bool stompActive = false;
    private ParticleSystem stompShockWave;

    private float playerRadius = 1.0f;
    private float playerHeight = 2.0f;

    public bool isMoving = false;
    private bool canMove;

    public int NumLives { get => numLives; set => numLives = value; }
    public float Speed { get => speed; set => speed = value; }
    public float SpeedMultiplyer { get => speedMultiplyer; set => speedMultiplyer = value; }
    public bool StompActive { get => stompActive; set => stompActive = value; }

    private void Awake()
    {
        inputManager.OnActionButtonPressed += HandleActionButtonPressed;
    }

    private void HandleActionButtonPressed(object sender, EventArgs e)
    {
        if (stompActive)
        {
            stompShockWave.gameObject.SetActive(true);
            StartCoroutine(ConludeStomping());
            CheckForEnemiesInArea();
            stompActive = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stompShockWave = GetComponentInChildren<ParticleSystem>();
        if (stompShockWave != null)
        {
            stompShockWave.Stop();
        }

    }

    // Update is called once per frame
    void Update()
    {
        HandlePowerUpInteraction();
        HandleMovement();
    }

    private void HandlePowerUpInteraction()
    {
        float moveDistance = Time.deltaTime * Speed * SpeedMultiplyer;
        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, transform.forward, out RaycastHit hit, moveDistance))
        {
            if (hit.collider.TryGetComponent<SpeedPowerUp>(out SpeedPowerUp spdpowerup))
            {

                spdpowerup.Activate(this);
                StartCoroutine(FadeSpeed());
                spdpowerup.DestroySelf();
            }
            else if (hit.collider.TryGetComponent<PowerUp>(out PowerUp powerup))
            {
                powerup.Activate(this);
                powerup.DestroySelf();
            }
        }





    }

    private void HandleMovement()
    {
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = Time.deltaTime * Speed * SpeedMultiplyer;
        isMoving = moveDir != Vector3.zero;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //if the player is making a diagonal movement and hits a wall, it should try to decompose the movement so it cam move sideways whilst hugging the wall
        //if the player hit a wall
        if (!canMove)
        {
            //check if the player cam move on the x axis
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized; //normalize, so it does not go slower when hugging the wall
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //check if the player can move on the Z axis
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);

    }

    public bool IsWalking()
    {
        return isMoving;
    }

    public void AddLives(int livesToAdd)
    {
        NumLives += livesToAdd;
        if (NumLives >= 0)
        {
            OnLivesChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private IEnumerator FadeSpeed()
    {
        yield return new WaitForSeconds(powerUpDuration);
        SpeedMultiplyer /= 1.5f;
    }

    private IEnumerator ConludeStomping()
    {
        yield return new WaitForSeconds(stompShockWave.main.duration);
        stompShockWave.gameObject.SetActive(false);
    }

    private void CheckForEnemiesInArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3.0f);
        foreach (Collider c in colliders)
        {
            if (c.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Explode();
            }
        }
    }
}
