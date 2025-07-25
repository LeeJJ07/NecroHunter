using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Idle State")]
    public float detectionRadius = 1.0f;

    [Header("Harvest State")]
    public string[] harvestAnimNames;

    [Header("Hauling State")]
    public int maxCountHaulResource = 5;
    public float decreaseSpeedPerHaulResource = -0.2f;

    [Header("Attack")]
    public float attackDistance = 5.0f;
}
