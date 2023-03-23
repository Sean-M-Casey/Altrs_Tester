using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public DiceStats diceStats;

    public Camera main;

    public LayerMask layer;

    Vector3 looking;

    float number;

    void Awake()
    {
        looking = diceStats.transform.forward;

        number = looking.z;

        number += 0.9106792f;
    }

    void Update()
    {
        //Vector3 point = diceStats.GetComponent<MeshCollider>().ClosestPointOnBounds(main.transform.position);

        //Vector3 diff = diceStats.GetComponent<MeshCollider>().;

        //Debug.Log(point);


        

        Debug.DrawRay(diceStats.transform.position, looking * 10, Color.yellow);

        if (Physics.Raycast(diceStats.transform.position, diceStats.transform.forward, out RaycastHit hit, Mathf.Infinity, layer))
        {
            float distance = Vector3.Distance(new Vector3(0, 0, hit.point.z), new Vector3(0, 0, main.transform.position.z));

            
        }
    }
}
