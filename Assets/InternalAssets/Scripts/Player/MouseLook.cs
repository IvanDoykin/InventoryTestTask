using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum RotationAxes
{
    MouseX,
    MouseY
}

public class MouseLook : MonoBehaviour
{
    private const float minimumVert = -75.0f;
    private const float maximumVert = 90.0f;

    [Range(6f, 15f)]
    [SerializeField] private float sensitivityHor = 9.0f;

    [Range(6f, 15f)]
    [SerializeField] private float sensitivityVert = 9.0f;

    private RotationAxes axes;
    private float rotationX = 0;

    private new Camera camera;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (GetComponent<Camera>() != null)
        {
            axes = RotationAxes.MouseY;
            camera = GetComponent<Camera>();
        }

        else if (GetComponent<CharacterController>() != null)
        {
            axes = RotationAxes.MouseX;
        }

        else
        {
            Destroy(this, 1f);
            throw new UnityException("MouseLook uses only at CharacterController or Camera.");
        }
    }

    private void Update()
    {
        MoveAround();
    }

    private void MoveAround()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }

        if (axes == RotationAxes.MouseY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }
}
