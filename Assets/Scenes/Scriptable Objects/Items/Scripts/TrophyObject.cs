using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trophy Object", menuName = "Inventory System/Items/Trophy")]
public class TrophyObject : ItemObject
{
    public int addPoint;
    public void Awake()
    {
        type = ItemType.Trophy;
    }
}
