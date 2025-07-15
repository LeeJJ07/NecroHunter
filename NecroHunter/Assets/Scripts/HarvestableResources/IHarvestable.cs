using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHarvestable
{
    ResourceData ResourceData { get; }

    void Harvested(int damage);
}
