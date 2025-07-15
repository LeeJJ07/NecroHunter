using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HarvestableObject : MonoBehaviour, IHarvestable
{
    [SerializeField] private ResourceData data;
    private int currentDurability;

    private void Start()
    {
        currentDurability = data.initialDurability;
    }

    public void Harvest(int harvestPower)
    {
        currentDurability -= harvestPower;

        if (currentDurability <= 0)
        {
            // Destroy Resource
        }
    }

    protected abstract void OnDepleted();
}
