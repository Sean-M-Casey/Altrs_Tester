using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackableObject : MonoBehaviour, IHackable
{
    [SerializeField] private HackableTypes hackableType;
    private GameObject hackableAsGameObject;

    public HackableTypes HackableType { get { return hackableType; } }

    public GameObject TargetAsGameObject { get { return hackableAsGameObject; } }

    private void Awake()
    {
        hackableAsGameObject = gameObject;
    }
}
