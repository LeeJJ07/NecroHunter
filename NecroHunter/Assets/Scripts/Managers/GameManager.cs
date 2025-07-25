using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool isNight;
    public bool IsNight { get { return isNight; } }
}
