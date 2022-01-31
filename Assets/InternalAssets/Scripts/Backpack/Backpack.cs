using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    [SerializeField] private List<BackpackPlace> backpackPlaces = new List<BackpackPlace>();
    private readonly List<BackpackCell> cells = new List<BackpackCell>();

    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform cellPlace;

    [SerializeField] private Canvas ui;
    public Canvas UI => ui;

    private Transform lookPoint;

    private void Start()
    {
        BackpackPlace.BackpackPlaceSet.AddListener(SyncPlaceSet);
        BackpackPlace.BackpackPlaceRemove.AddListener(SyncPlaceRemove);

        for (int i = 0; i < backpackPlaces.Count; i++)
        {
            GameObject cell = Instantiate(cellPrefab, cellPlace);
            BackpackCell backpackCell = cell.GetComponent<BackpackCell>();
            backpackCell.SetType(backpackPlaces[i].KeepType);
            cells.Add(backpackCell);
        }
    }

    private void Update()
    {
        if (lookPoint != null)
        {
            ui.transform.LookAt(lookPoint);
            ui.transform.eulerAngles = new Vector3(0f, ui.transform.eulerAngles.y + 180f, 0f);
            OpenUIUpdate();
        }
        else
        {
            CloseUIUpdate();
        }

        lookPoint = null;
    }

    public void SetLookToPlayer(Transform player)
    {
        lookPoint = player;
    }

    private void OnDestroy()
    {
        BackpackPlace.BackpackPlaceSet.RemoveListener(SyncPlaceSet);
        BackpackPlace.BackpackPlaceRemove.RemoveListener(SyncPlaceRemove);
    }

    private void SyncPlaceSet(ItemInfo info)
    {
        foreach (var cell in cells)
        {
            if (cell.Type == info.Type)
            {
                cell.SetItem(info);
            }
        }
    }

    private void SyncPlaceRemove(ItemInfo info)
    {
        foreach (var cell in cells)
        {
            if (cell.Type == info.Type)
            {
                cell.RemoveItem();
            }
        }
    }

    private void OpenUIUpdate()
    {
        ui.gameObject.SetActive(true);
        foreach (var cell in cells)
        {
            cell.gameObject.SetActive(cell.IsActive);
        }
    }

    private void CloseUIUpdate()
    {
        ui.gameObject.SetActive(false);
    }
}
