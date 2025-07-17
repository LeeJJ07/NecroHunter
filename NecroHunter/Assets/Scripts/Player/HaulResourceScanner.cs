using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaulResourceScanner : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("ResourceFragment"))
            return;
        if (!player.CanGetHaulResource())
            return;
        Debug.Log("Current Speed = " + player.StatHandler.GetStat(EStatType.MOVE_SPEED));
        player.GetHaulResource();
    }
}
