using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class SpriteStacker : MonoBehaviour
{
    [SerializeField] Transform player;

    public List<Transform> characters;

    private Transform nearestCharacter;



    void Update()
    {
        float minimumDistance = Mathf.Infinity;
        if (nearestCharacter != null)
        {
            nearestCharacter.GetComponent<SortingGroup>().sortingOrder = 12;
        }
        nearestCharacter = null;
        foreach (Transform character in characters)
        {
            float distance = Vector3.Distance(player.position, character.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                nearestCharacter = character;
            }
        }
        nearestCharacter.GetComponent<SortingGroup>().sortingOrder = 120;
        Debug.Log("Nearest Character: " + nearestCharacter + "; Distance: " + minimumDistance + "Sorting Group " + nearestCharacter.GetComponent<SortingGroup>());
    }
}
