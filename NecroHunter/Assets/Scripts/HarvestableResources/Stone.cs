using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : HarvestableObject
{
    protected override void OnDepleted()
    {
        Debug.Log("Depleted All Stones");
        Destroy(gameObject);
    }
}
