using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Vector3 healthbarRotation;

    // Start is called before the first frame update
    void Start()
    {
        healthbarRotation = Camera.main.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(healthbarRotation);
    }
}
