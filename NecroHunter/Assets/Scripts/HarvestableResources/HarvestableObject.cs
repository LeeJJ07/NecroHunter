using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public abstract class HarvestableObject : MonoBehaviour, IHarvestable
{
    [SerializeField] private ResourceData data;
    public ResourceData ResourceData { get => data; }
    private int currentDurability;

    protected NavMeshObstacle navMeshObstacle;

    private void Awake()
    {
        navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    private void Start()
    {
        currentDurability = data.initialDurability;
    }

    public void Harvested(int harvestPower)
    {
        currentDurability -= harvestPower;

        if (currentDurability <= 0)
        {
            OnDepleted();
        }
    }
    protected void EmitHarvestFragments()
    {
        for (int i = 0; i < ResourceData.fragemntAmount; i++)
        {
            Vector3 targetPos = FragmentTargetPos();

            GameObject fragment = ObjectPoolManager.SpawnObject(data.fragment, transform.position, Quaternion.identity);

            float duration = Random.Range(data.fragmentMinMoveTime, data.fragmentMaxMoveTime);
            fragment.transform.DOMove(targetPos, duration).SetEase(Ease.OutBack);
        }
    }
    protected abstract void OnDepleted();

    private Vector3 FragmentTargetPos()
    {
        Vector3 origin = transform.position;

        Vector3 offset = Random.insideUnitSphere * data.fragmentSpreadRadius;
        offset.y = Mathf.Abs(offset.y) + data.fragmentVerticalBoost;
        return origin + offset;
    }
}
