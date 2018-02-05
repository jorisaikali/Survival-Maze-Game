using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerMovement : MonoBehaviour {

    // ---------- Public variables ---------- //
    public float moveSpeed = 5f;
    public float mouseSensitivity = 5f;
    public float verticalRange = 60f;
    // -------------------------------------- //

    // ---------- Private variables ---------- //
    private float verticalRotation = 0f;
    private GameObject gameObjectHit = null;
    private float camRayLength = 100f;
    private bool lockMovement = false, lockRotation = false;
    // private CharacterController cc;
    private PlayerMotor motor;
    // --------------------------------------- //
    private void Start()
    {

        // --------- Configuring the cursor --------- //
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // ------------------------------------------ //

        motor = GetComponent<PlayerMotor>();
    }

    public void UpdateMe()
    {
        if (!lockMovement) // Check if movement is locked, if not locked, allow movement
            Move();

        if (!lockRotation) // Check if rotatio is locked, if not locked, allow rotating
            Look();


        
    }

    // ------------------------ Moving the player ------------------------ //
    private void Move()
    {
        float forwardSpeed = Input.GetAxisRaw("Vertical");
        float sideSpeed = Input.GetAxisRaw("Horizontal");

        Vector3 horizontalMove = transform.right * sideSpeed;
        Vector3 verticalMove = transform.forward * forwardSpeed;

        Vector3 velocity = (horizontalMove + verticalMove).normalized * moveSpeed;
        
        //Vector3 speed = new Vector3(sideSpeed, 0f, forwardSpeed).normalized * moveSpeed;
        //speed = transform.rotation * speed;
        motor.Move(velocity);
         
        //cc.SimpleMove(speed);
    }
    // ------------------------------------------------------------------- //

    // ------------------------ Controls the players rotation ------------------------ //
    private void Look()
    {
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, rotLeftRight, 0f);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRange, verticalRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
    // -------------------------------------------------------------------------------- //

    // -------------------------- Getting what the player is looking at -------------------------- //
    /* Checks what the player is looking at on the mask layer. Returns the game object being looked at,
       null if looking at nothing on that mask */
    public GameObject GetRayCast(LayerMask mask)
    {
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
    
        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, camRayLength, mask))
        {
            gameObjectHit = hit.transform.gameObject;
        }
        else
        {
            gameObjectHit = null;
        }

        return gameObjectHit;
    }
    // -------------------------------------------------------------------------------------------- //

    // ---------- Locking/Unlocking player movement/rotate ---------- //
    public void LockMovement() { lockMovement = true; }
    public void LockRotation() { lockRotation = true; }
    public void UnlockMovement() { lockMovement = false; }
    public void UnlockRotation() { lockRotation = false; }
    // -------------------------------------------------------------- //
}
