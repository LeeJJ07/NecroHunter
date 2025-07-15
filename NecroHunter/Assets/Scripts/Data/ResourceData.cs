using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResourceData", menuName = "Resources/Resource Data")]
public class ResourceData : ScriptableObject
{
    public EResourceType resourceType;
    public int initialDurability;

    [Header("Fragment Info")]
    public GameObject fragment;
    public int fragemntAmount;

    [Header("Fragment Generation Info")]
    public float fragmentSpreadRadius;
    public float fragmentVerticalBoost;
    public float fragmentMinMoveTime;
    public float fragmentMaxMoveTime;
}
