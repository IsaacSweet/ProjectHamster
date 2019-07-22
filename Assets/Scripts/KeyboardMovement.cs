using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour {
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    public bool roundedMovement;

    public Vector2 mouseAxisModifier;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame

	void Update () {
        if (GameSettings.Instance.Paused == false)
        {
            MoveCharacter();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameSettings.Instance.Paused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                GameSettings.Instance.Paused = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                GameSettings.Instance.Paused = true;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            CharacterSettings.Instance.mainAction.Invoke();
        }
    }

    //Move Order
    //Rotation
    //XZ Movement
    //Y Movement
    void MoveCharacter()
    {
        CharacterSettings.Instance.Rotate(Input.GetAxis("Mouse X") * mouseAxisModifier.x, 
            Input.GetAxis("Mouse Y") * mouseAxisModifier.y);
        /******
         * Movement space considers the following
         * Via the vector FORWARD, object is moving in the Positive Z axis
         * Thusly,  Positive Z is NORTH
         *          Negative Z is SOUTH
         *          Positive X is EAST
         *          Negative X is WEST
         *          
         *          Y is used for depth, positive Y going UP
        ******/
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetKey(forward))
        {
            movementDirection += Vector3.forward;
        }
        if (Input.GetKey(backward))
        {
            movementDirection += Vector3.back;
        }
        if (Input.GetKey(left))
        {
            movementDirection += Vector3.left;
        }
        if (Input.GetKey(right))
        {
            movementDirection += Vector3.right;
        }

        if (roundedMovement)
        {
            movementDirection = movementDirection.normalized;
        }

        if (movementDirection != Vector3.zero)
        {
            CharacterSettings.Instance.Move(movementDirection);
        }

        if(Input.GetKeyDown(jump))
        {
            
        }
    }
}
