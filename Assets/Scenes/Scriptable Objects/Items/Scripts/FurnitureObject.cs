using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Furniture Object", menuName = "Inventory System/Items/Furniture")]

public class FurnitureObject : ItemObject

{
    public void Awake()
    {
        type = ItemType.Furniture;
    }
}
