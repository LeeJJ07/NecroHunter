using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResourceData", menuName = "Resources/Resource Data")]
public class ResourceData : ScriptableObject
{
    public EResourceType resourceType;
    public int initialDurability;
}
