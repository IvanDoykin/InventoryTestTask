using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Tool,
    Gun,
    None
}

[CreateAssetMenu(fileName = "newItem", menuName = "Items/NewItem", order = 1)]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private new string name;
    public string Name => name;

    [SerializeField] private int id;
    public int Id => id;

    [SerializeField] private float weight;
    public float Weight => weight;

    [SerializeField] private ItemType type;
    public ItemType Type => type;

    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
}
