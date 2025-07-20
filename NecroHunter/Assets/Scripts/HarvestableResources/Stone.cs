using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Stone : HarvestableObject
{
    protected override void OnDepleted()
    {
        EmitHarvestFragments();

        navMeshObstacle.carving = true;

        Destroy(gameObject);
    }
}
