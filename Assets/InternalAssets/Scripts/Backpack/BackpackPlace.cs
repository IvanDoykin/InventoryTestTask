using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackpackPlace : MonoBehaviour
{
    public static UnityEvent<ItemInfo> BackpackPlaceSet { get; private set; } = new UnityEvent<ItemInfo>();
    public static UnityEvent<ItemInfo> BackpackPlaceRemove { get; private set; } = new UnityEvent<ItemInfo>();

    [SerializeField] private Transform place;

    [SerializeField] private ItemType keepType;
    public ItemType KeepType => keepType;

    private Item keepItem;
    public Item KeepItem => keepItem;

    public bool IsEmpty => keepItem == null;

    public void SetItem(Item newItem)
    {
        if (newItem.Info.Type == keepType && IsEmpty)
        {
            keepItem = newItem;
            keepItem.ResetHandler();
            keepItem.SetToBackpack(this);

            TransformTools.Instance.SmoothMoveTo(keepItem.transform, place.position, 0.5f);
            TransformTools.Instance.SmoothRotationTo(keepItem.transform, Vector3.zero, 0.5f);
            keepItem.DisableGravity();
            BackpackPlaceSet.Invoke(keepItem.Info);
        }
    }

    public void RemoveKeepItem()
    {
        BackpackPlaceRemove.Invoke(keepItem.Info);
        keepItem = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponentInParent<Item>();
        if (item != null)
        {
            if (item.Info.Type == keepType && Vector3.Distance(item.transform.position, place.position) >= 0.15f)
            {
                SetItem(item);
            }
        }
    }
}
