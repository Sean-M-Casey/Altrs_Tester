using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - target.position);
        //transform.LookAt(target);
        var rot = transform.eulerAngles;
        rot.x = 0;
        transform.eulerAngles = rot;
    }
}

