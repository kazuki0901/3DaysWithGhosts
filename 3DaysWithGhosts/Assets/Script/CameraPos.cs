using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public Transform cameraPos;


    void Update()
    {
        transform.position = cameraPos.position;
    }
}
