using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkyBoxRotation : MonoBehaviour
{
    //public Vector3 editRotation;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // transform.Rotate(new Vector3(transform.position.x, transform.position.y + y, transform.position.z) * Time.deltaTime);
        transform.Rotate(0, y * Time.deltaTime, 0);

        
        Debug.Log("Update");

    }
}
