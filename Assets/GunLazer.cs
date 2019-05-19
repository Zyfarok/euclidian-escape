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
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.GetComponent<Renderer>().material = lineMaterial;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.enabled = false;
        GameObject gun = GameObject.Find("BitGun");
        Debug.Log(gun);
        gun.GetComponent<VRTK.VRTK_InteractableObject>().InteractableObjectUsed += (sender, e) => {line.enabled = true;Update();};
        gun.GetComponent<VRTK.VRTK_InteractableObject>().InteractableObjectUnused += (sender, e) => line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (line.enabled)
        {
            Ray ray = new Ray(gunTip.position, gunTip.forward);
            if (Physics.Raycast(ray, out hit, range))
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
            }
        }
    }
}
