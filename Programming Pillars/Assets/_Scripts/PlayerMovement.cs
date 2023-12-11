using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxMovementSpeed;


    [SerializeField] private float boundX;
    [SerializeField] private float boundZ;


    public bool canMove = true;
    public bool canLook = true;
    private Camera main;

    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Transform bodyT;

    [SerializeField] private TextMeshProUGUI speedText;


    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameMan.gameOver || GameManager.gameMan.paused) return;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(canLook) LookAtMouse();
        if(canMove) MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(movement.normalized * Time.deltaTime * movementSpeed);
        if (transform.position.x > boundX)
        {
            transform.position = new Vector3(boundX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -boundX)
        {
            transform.position = new Vector3(-boundX, transform.position.y, transform.position.z);
        }
        if (transform.position.z > boundZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundZ);
        }
        if (transform.position.z < -boundZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundZ);
        }
    }


    private void LookAtMouse()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, floorMask))
        {
            Vector3 mousePos = hit.point;
            mousePos.y = bodyT.position.y;
            bodyT.LookAt(mousePos, Vector3.up);
        }


    }

    public void ChangeMoveSpeed(float speed)
    {
        movementSpeed += speed;
        if (movementSpeed > maxMovementSpeed) movementSpeed = maxMovementSpeed;
        speedText.text = $"{movementSpeed}";
    }


}
