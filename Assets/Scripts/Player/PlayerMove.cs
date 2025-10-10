using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private InputSystem_Actions playerActions;

    private void Awake()
    {
        playerActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerActions.Player.Move.WasPressedThisFrame())
        {
            Move();
        }
    }

    private void Move()
    {
        // assign values to a Vector2 based on the last received input from WASD
        Vector2 moveInput = playerActions.Player.Move.ReadValue<Vector2>();

        //check if there was any input
        if(moveInput != Vector2.zero)
        {
            //if yes: change the vector2 to a vector3
            Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y).normalized;

            // move character in the direction being pressed
            transform.position += moveVector;
            TurnToFaceDirection(moveInput);

        }
    }

    private void TurnToFaceDirection(Vector3 direction)
    {
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if(direction.x > 0) 
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(direction.y < 0) 
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (direction.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
