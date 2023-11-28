using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        MovePlayer();
    }

    public void MovePlayer()
    {
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(movement.normalized * Time.deltaTime * movementSpeed);
    }
}
