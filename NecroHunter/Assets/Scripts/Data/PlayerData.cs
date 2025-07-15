using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Idle State")]
    public float detectionRadius = 5.0f;

    [Header("Move State")]
    public float moveSpeed = 3.0f;

    [Header("Harvest State")]
    public string[] harvestAnimNames;

}
