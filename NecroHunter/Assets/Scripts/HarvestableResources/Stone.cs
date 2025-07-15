using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Stone : HarvestableObject
{
    protected override void OnDepleted()
    {
        EmitHarvestFragments();
        Destroy(gameObject);
    }
}
