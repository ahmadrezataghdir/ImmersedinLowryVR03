using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform target; 

    void Update()
    {
        if (target != null)
        {
            
            transform.position = target.position;
            transform.rotation = target.rotation; 
        }
    }
}

