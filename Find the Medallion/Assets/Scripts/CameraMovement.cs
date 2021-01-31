using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    
    public float cameraHeight = 10;
    
    void Start(){
        cam = this.GetComponent<Camera>();
    }
    void Update(){
        transform.position = target.position + new Vector3(0, cameraHeight, -10);
        transform.LookAt(target);
    }
}
