using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public DiceStats diceStats;

    Camera main;

    [SerializeField] LayerMask diceColliderLayerMask, canPickupLayerMask;

    int forceAmount = 50;

    Rigidbody rigidBody;

    Vector3 diceUp;

    Ray ray;

    void Awake()
    {
        diceUp = diceStats.transform.TransformDirection(Vector3.up);

        rigidBody = diceStats.GetComponent<Rigidbody>();

        main = Camera.main;
    }

    void Update()
    {
        if(rigidBody.velocity == Vector3.zero)
        {
            OutputRoll();
        }

        ray = main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, canPickupLayerMask))
        {
            if (Input.GetMouseButton(0))
            {
                diceStats.transform.position = new Vector3(hit.point.x, 1, hit.point.z);

                rigidBody.AddForce(hit.point);
            }
        }
    }

    void OutputRoll()
    {
        if (Physics.Raycast(diceStats.transform.position, diceUp, out RaycastHit hit, Mathf.Infinity, diceColliderLayerMask))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
