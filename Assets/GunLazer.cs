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

    public MonoBehaviour gear1Script;
    public MonoBehaviour gear2Script;

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
                if(hit.collider.gameObject.name.Equals("Blocker")) {
                    hit.collider.gameObject.SetActive(false);
                    gear1Script.enabled = true;
                    gear2Script.enabled = true;
                }
                line.SetPosition(0, gunTip.position);
                line.SetPosition(1, hit.point);
            } else {
                line.SetPosition(0, gunTip.position);
                line.SetPosition(1, gunTip.position + 1000*gunTip.forward);
            }
        }
    }
}
