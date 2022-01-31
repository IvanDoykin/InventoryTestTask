using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackCell : MonoBehaviour
{
    public ItemType Type { get; private set; } = ItemType.None;

    [SerializeField] private Image cellIcon;
    public bool IsActive => cellIcon.sprite != null;

    public void SetType(ItemType type)
    {
        if (Type == ItemType.None)
        {
            Type = type;
        }
    }

    public void SetItem(ItemInfo newItemInfo)
    {
        cellIcon.sprite = newItemInfo.Icon;
    }

    public void RemoveItem()
    {
        cellIcon.sprite = null;
    }
}
