using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class GunLazer : MonoBehaviour
{
    public Transform gunTip;

    RaycastHit hit;
    public float range = 100.0f;
    LineRenderer line;
    public Material lineMaterial;
    
    // Start is called before the first frame update
    void Start() {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.GetComponent<Renderer>().material = lineMaterial;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
    }

    // Update is called once per frame
    void Update() {
        Ray ray =  new Ray(gunTip.position, gunTip.forward);
        if(Physics.Raycast(ray, out hit, range)) {
            if(true) {
                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point + hit.normal);
            }
            else {
                line.enabled = false;
            }
        } 
    }
}
