using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private StatData statData;
    private Dictionary<EStatType, float> currentStats = new Dictionary<EStatType, float>();

    private void Awake()
    {
        InitializeStats();
    }
    private void InitializeStats()
    {
        foreach(StatEntry entry in statData.stats)
        {
            currentStats[entry.statType] = entry.baseValue;
        }
    }

    public float GetStat(EStatType statType)
    {
        return currentStats.ContainsKey(statType) ? currentStats[statType] : 0.0f;
    }

    public void ModifyStat(EStatType statType, float amount, bool isPermanent = true, float duration = 0.0f)
    {
        if (!currentStats.ContainsKey(statType))
            return;

        currentStats[statType] += amount;

        if(!isPermanent)
            StartCoroutine(RemoveStatAfterDuration(statType, amount, duration));
    }

    private IEnumerator RemoveStatAfterDuration(EStatType statType, float amount, float duration)
    {
        yield return new WaitForSeconds(duration);
        currentStats[statType] -= amount;
    }
}
