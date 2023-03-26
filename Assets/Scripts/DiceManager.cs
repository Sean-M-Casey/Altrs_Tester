using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    Transform dice;

    //Camera main;

    [SerializeField] LayerMask diceColliderLayerMask/*, canPickupLayerMask*/;

    [Tooltip("Keep this at a length of 2! It will not work otherwise :)")]
    [SerializeField] int[] forceRange = new int[2];

    Rigidbody rigidBody;

    //Ray ray;

    void Awake()
    { 
        //main = Camera.main;
    }

    void Update()
    {
        if(rigidBody != null)
        {
            if (rigidBody.velocity == Vector3.zero)
            {
                OutputRoll();
            }
        }

        //ray = main.ScreenPointToRay(Input.mousePosition);

        //if(Physics.Raycast(ray, out RaycastHit hit, canPickupLayerMask))
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        diceStats.transform.position = new Vector3(hit.point.x, 1, hit.point.z);

        //        rigidBody.AddForce(hit.point);
        //    }
        //}
    }

    public void ThrowDice(Transform dicePrefab)
    {
        int random = Random.Range(0, spawnPoints.Length - 1);

        dice = Instantiate(dicePrefab, spawnPoints[random].position, Quaternion.identity);

        random = Random.Range(forceRange[0], forceRange[1]);

        Debug.Log("Dice was thrown with a force of: " + random);

        rigidBody = dice.GetComponent<Rigidbody>();

        rigidBody.AddForce(new Vector3(1, 0, 1) * random, ForceMode.Force);
    }

    void OutputRoll()
    {
        if (Physics.Raycast(dice.transform.position, transform.up, out RaycastHit hit, Mathf.Infinity, diceColliderLayerMask))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
