using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : HarvestableObject
{
    protected override void OnDepleted()
    {
        Debug.Log("Depleted All Woods");
        Destroy(gameObject);
    }
}
