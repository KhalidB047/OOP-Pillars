using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerT;
    private Camera main;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        main = Camera.main;
        playerT = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        main.transform.position = playerT.position + offset;
    }
}
