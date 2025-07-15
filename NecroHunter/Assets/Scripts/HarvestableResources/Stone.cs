using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Stone : HarvestableObject
{
    protected override void EmitHarvestFragments()
    {
        for (int i = 0; i < ResourceData.fragemntAmount; i++)
        {
            Vector3 origin = transform.position;

            Vector3 offset = Random.insideUnitSphere * 2f;
            offset.y = Mathf.Abs(offset.y) + 1f;
            Vector3 targetPos = origin + offset;

            GameObject fragment = Instantiate(ResourceData.fragment, origin, Quaternion.identity);

            float duration = Random.Range(0.3f, 0.6f);
            fragment.transform.DOMove(targetPos, duration).SetEase(Ease.OutBack);
        }
    }
    protected override void OnDepleted()
    {
        EmitHarvestFragments();
        Destroy(gameObject);
    }
}
