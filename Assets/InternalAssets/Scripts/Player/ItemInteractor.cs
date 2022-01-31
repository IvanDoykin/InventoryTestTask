using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractor : MonoBehaviour
{
    [SerializeField] private new Camera camera;

    [Range(1f, 10f)]
    [SerializeField] private float interactDistance = 5f;

    private Item pickedItem;

    private void Update()
    {
        PickUpCheck();
        HoldCheck();
        DropCheck();

        CheckOnBackpackUI();
    }

    private void PickUpCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0);
            Ray ray = camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                Item newPickedItem = hit.transform.GetComponent<Item>();

                if (newPickedItem != null)
                {
                    newPickedItem.RemoveFromBackpack();
                    newPickedItem.PickUpByPlayer(camera);
                    pickedItem = newPickedItem;
                }
            }
        }
    }

    private void HoldCheck()
    {
        if (pickedItem != null)
        {
            if (Vector3.Distance(pickedItem.transform.position, camera.transform.position) >= interactDistance)
            {
                pickedItem.ResetHandler();
                pickedItem.EnableGravity();
            }

            else
            {
                Vector3 point = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0);
                Ray ray = camera.ScreenPointToRay(point);
                RaycastHit[] hits = Physics.RaycastAll(ray, interactDistance - 1);

                foreach (var hit in hits)
                {
                    if (hit.transform.GetComponentInParent<Item>() == null)
                    {
                        pickedItem.MoveTo(hit.point - ray.direction * 0.1f);
                        break;
                    }
                }
            }
        }
    }

    private void DropCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            pickedItem?.ResetHandler();
            pickedItem?.EnableGravity();
            pickedItem = null;
        }
    }

    private void CheckOnBackpackUI()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 point = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0);
            Ray ray = camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                Backpack backpackUI = hit.transform.GetComponent<Backpack>();
                if (backpackUI != null)
                {
                    backpackUI.SetLookToPlayer(transform);
                }
            }
        }
    }
}
