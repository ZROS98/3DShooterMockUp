using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [field: SerializeField]
    private Transform CameraTransform { get; set; }
    [field: SerializeField]
    private Transform PlayerCameraHolderTransform { get; set; }

    protected virtual void Update ()
    {
        CameraTransform.position = PlayerCameraHolderTransform.position;
        CameraTransform.rotation = PlayerCameraHolderTransform.rotation;
    }
}