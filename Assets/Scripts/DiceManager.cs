using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform centrePoint;
    Transform dice;

    [SerializeField] float secondsBeforeDestroying;

    [SerializeField] LayerMask diceColliderLayerMask;

    [Tooltip("Keep this at a length of 2! It will not work otherwise :)")]
    [SerializeField] int[] forceRange = new int[2];

    Rigidbody rigidBody;

    bool diceRolled;

    void Awake()
    { 
        foreach(var t in spawnPoints) //getting the same rotation for each spawnpoint to throw the dice in the same direction
        {
            t.LookAt(centrePoint);
        }
    }

    void Update()
    {
        if(rigidBody != null && diceRolled)
        {
            if (rigidBody.velocity == Vector3.zero)
            {
                OutputRoll();
            }
        }
    }

    public void ThrowDice(Transform dicePrefab) //this method takes in the dice prefab from the button, picks a random spawnpoint, then a random force amount, and then throws the instantiated dice prefab with that force.
    {
        int random = Random.Range(0, spawnPoints.Length);

        Transform pickedSpawnPoint = spawnPoints[random];

        dice = Instantiate(dicePrefab, pickedSpawnPoint.position, Quaternion.identity);

        random = Random.Range(forceRange[0], forceRange[1]);

        Debug.Log("Dice was thrown from: " + pickedSpawnPoint.name + ", with a force of: " + random);

        diceRolled = false;

        StartCoroutine(WaitForThrow());

        rigidBody = dice.GetComponent<Rigidbody>();
        rigidBody.AddForce(pickedSpawnPoint.forward * random, ForceMode.Force);

        StartCoroutine(DestroyAfterSeconds(dice.gameObject));
    }

    void OutputRoll() //this is the outcome of the roll
    {
        if (Physics.Raycast(dice.transform.position, transform.up, out RaycastHit hit, Mathf.Infinity, diceColliderLayerMask))
        {
            Debug.Log(hit.collider.name); //need to figure out a way too ensure the dice isn't cocked.
        }
    }

    IEnumerator DestroyAfterSeconds(GameObject diceToDestroy) //destroys dice after a certain time to reduce performance issues.
    {
        yield return new WaitForSeconds(secondsBeforeDestroying);

        Destroy(diceToDestroy);
    }

    IEnumerator WaitForThrow() //without this, the dice returns 20 before it has a chance to apply force, leading to potential inaccuracies in the future.
    {
        yield return new WaitForSeconds(0.25f); //makes the update function wait a quarter of a second before outputting the result, i.e, waiting for the force to be applied.

        diceRolled = true;
    }
}
