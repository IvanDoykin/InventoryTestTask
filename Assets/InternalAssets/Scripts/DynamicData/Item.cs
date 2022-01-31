using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Item : MonoBehaviour
{
    public BackpackPlace BackpackPlace { get; private set; }

    [SerializeField] private ItemInfo info;
    public ItemInfo Info => info;

    private Rigidbody physicsComponent;

    private void Start()
    {
        physicsComponent = GetComponent<Rigidbody>();
    }

    // Place where I would ask colleagues how to do better
    #region WeakPartCode

    public void PickUpByPlayer(Camera playerCamera)
    {
        DisableGravity();
        physicsComponent.detectCollisions = true;

        physicsComponent.velocity = Vector3.zero;
        physicsComponent.angularVelocity = Vector3.zero;
        transform.SetParent(playerCamera.transform);
    }

    public void ResetHandler()
    {
        Vector3 objectPos = transform.position;
        transform.SetParent(null);
        transform.position = objectPos;
    }

    public void DisableGravity()
    {
        physicsComponent.useGravity = false;
    }

    public void SetKinematicState(bool state)
    {
        physicsComponent.isKinematic = state;
        if (state)
        {
            DisableGravity();
        }
    }

    #endregion

    public void EnableGravity()
    {
        physicsComponent.useGravity = true;
    }

    public void MoveTo(Vector3 destination)
    {
        physicsComponent.MovePosition(destination);
    }

    public void RemoveFromBackpack()
    {
        BackpackPlace?.RemoveKeepItem();
        BackpackPlace = null;
        EnableGravity();
        SetKinematicState(false);
    }

    public void SetToBackpack(BackpackPlace place)
    {
        BackpackPlace = place;
        SetKinematicState(true);
    }
}
