using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform centrePoint;

    List<int> selectedSpawnPoints = new();
    List<Transform> pickedDice = new(), thrownDice = new();

    [SerializeField] GameObject throwButton, diceButtons, cancelButton;

    [SerializeField] LayerMask diceColliderLayerMask;

    [SerializeField] float secondsBeforeDestroying;

    [Tooltip("Keep this at a length of 2! It will not work otherwise :)")]
    [SerializeField] int[] forceRange = new int[2];
    [SerializeField] int maximumDiceToThrow; //maximum number of dice that can be rolled at once
    int diceThrownCount, totalRoll;

    bool diceRolled = false;

    void Awake()
    { 
        foreach(var t in spawnPoints) //getting the same rotation for each spawnpoint to throw the dice in the same direction
        {
            t.LookAt(centrePoint);
        }
    }

    void Update()
    {
        if (!diceRolled)
        {
            return;
        }

        int counting = 0;

        foreach(var t in thrownDice)
        {
            if (t.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                counting++;
            }
        }

        if (counting == thrownDice.Count) //if the number of die no longer moving is the same as the list count, then all of the dice have stopped moving
        {
            OutputRoll();
        }
    }

    public void PickingDice(Transform diePrefab)
    {
        thrownDice.Clear();

        if(pickedDice.Count == maximumDiceToThrow) //maximum number of dice that can be rolled at once
        {
            return;
        }

        pickedDice.Add(diePrefab);

        diceRolled = false;

        cancelButton.SetActive(true);
        throwButton.SetActive(true);
    }

    public void ThrowButtonClicked()
    {
        cancelButton.SetActive(false);
        throwButton.SetActive(false);
        diceButtons.SetActive(false);

        diceThrownCount = 0;

        foreach (var t in pickedDice)
        {
            SelectSpawnPoint(t);
        }

        pickedDice.Clear();
    }

    public void CancelPicking()
    {
        pickedDice.Clear();

        cancelButton.SetActive(false);
        throwButton.SetActive(false);
    }

    void SelectSpawnPoint(Transform dieToThrow)
    {
        int random = Random.Range(0, spawnPoints.Length);

        if (!CheckSelectedSpawnPoint(random))
        {
            SelectSpawnPoint(dieToThrow); //trys again for a new spawn point
            return;
        }
        else
        {
            ThrowDie(dieToThrow, random);
        }
    }

    void ThrowDie(Transform dieToThrow, int selectedSpawnPoint)
    {
        selectedSpawnPoints.Add(selectedSpawnPoint); //ensures each die gets thrown from a different spawn point in the array for randomness

        Transform pickedSpawnPoint = spawnPoints[selectedSpawnPoint];

        Transform dice = Instantiate(dieToThrow, pickedSpawnPoint.position, Quaternion.identity);

        dice.gameObject.AddComponent<DiceStats>(); //making the new die unique from the prefab - prefabs don't have dice stat components

        thrownDice.Add(dice);

        diceThrownCount++;

        dice.name = "Dice Number " + diceThrownCount; //making sure each die has a unique name for counting later

        selectedSpawnPoint = Random.Range(forceRange[0], forceRange[1]);

        StartCoroutine(WaitForThrow());

        Rigidbody rigidBody = dice.GetComponent<Rigidbody>();
        rigidBody.AddForce(pickedSpawnPoint.forward * selectedSpawnPoint, ForceMode.Force);

        StartCoroutine(DestroyAfterSeconds(dice.gameObject));
    }

    bool CheckSelectedSpawnPoint(int random) //checks to make sure the spawn point is a new one and hasn't been selected by another die in this throw.
    {
        foreach(var i in selectedSpawnPoints)
        {
            if(i == random)
            {
                return false;
            }
        }

        return true;
    }

    void OutputRoll() //this is the outcome of the roll
    {
        totalRoll = 0;

        foreach(var t in thrownDice)
        {
            int individualRoll;

            if (Physics.Raycast(t.position, transform.up, out RaycastHit hit, Mathf.Infinity, diceColliderLayerMask))
            {
                Debug.Log(t.name + " rolled: " + hit.collider.name); //need to figure out a way too ensure the dice isn't cocked.

                individualRoll = int.Parse(hit.collider.name); //this is the individual die's roll, can be pushed to UI seperate from the total as well if needed.

                totalRoll += individualRoll;
            }
        }

        Debug.Log("Total roll: " + totalRoll); //this is where the total is output, can be swapped out for UI in the future.

        diceRolled = false;

        thrownDice.Clear();
    }

    IEnumerator DestroyAfterSeconds(GameObject diceToDestroy) //destroys dice after a certain time to reduce performance issues.
    {
        yield return new WaitForSeconds(secondsBeforeDestroying);

        Destroy(diceToDestroy);

        diceRolled = false;

        diceButtons.SetActive(true);

        selectedSpawnPoints.Clear();
        thrownDice.Clear();
        pickedDice.Clear();
    }

    IEnumerator WaitForThrow() //without this, the dice returns 20 before it has a chance to apply force, leading to potential inaccuracies in the future.
    {
        yield return new WaitForSeconds(0.25f); //makes the update function wait a quarter of a second before outputting the result, i.e, waiting for the force to be applied.

        diceRolled = true;
    }
}
