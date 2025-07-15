using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HarvestableObject : MonoBehaviour, IHarvestable
{
    [SerializeField] private ResourceData data;
    public ResourceData ResourceData { get => data; }
    private int currentDurability;


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
    protected abstract void EmitHarvestFragments();
    protected abstract void OnDepleted();
}
