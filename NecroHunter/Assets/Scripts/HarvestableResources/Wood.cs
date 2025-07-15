using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wood : HarvestableObject
{
    protected override void OnDepleted()
    {
        EmitHarvestFragments();
        Destroy(gameObject);
    }
}
