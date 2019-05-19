using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonEuclidianGeometryChange : MonoBehaviour
{
    private enum Orientation
    {
        None = -1, NE, NW, SW, SE
    }

    public bool isEnabled = false;

    public GameObject engineRoom;
    //GameObject frontWall = null;
    public TMPro.TextMeshPro panel;
    
    Orientation lastOrientation = Orientation.None;

    // Start is called before the first frame update
    void Start()
    {
        //frontWall = GameObject.Find("CockpitFrontWall");
        engineRoom.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = VRTK.VRTK_DeviceFinder.HeadsetTransform().rotation.eulerAngles.y;
        Orientation current = GetOrientation(angle);
        if (!isEnabled || lastOrientation == Orientation.None)
        {
            lastOrientation = current;
            return;
        }

        if ((current == Orientation.SW && lastOrientation == Orientation.SE) ||
            (current == Orientation.SE && lastOrientation == Orientation.SW)
        )
        {
            SwapFront();
        } else if((current == Orientation.NW && lastOrientation == Orientation.NE) ||
                  (current == Orientation.NE && lastOrientation == Orientation.NW)
        ) {
            if((current == Orientation.NE) != engineRoom.activeSelf) {
                panel.text = "EngineRoom >\n< Cockpit";
            } else {
                panel.text = "< EngineRoom\nCockpit >";
            }

        }
        // Other swap can come later

        lastOrientation = current;
    }

    private Orientation GetOrientation(float angle)
    {
        int i = (int)Mathf.Floor(angle / 90) % 4;
        if (i < 0)
        {
            i += 4;
        }

        switch (i)
        {
            case 0:
                return Orientation.NE;
            case 1:
                return Orientation.SE;
            case 2:
                return Orientation.SW;
            default:
                return Orientation.NW;
        }
    }

    private void SwapFront()
    {
        if (engineRoom.activeSelf)
        {
            engineRoom.SetActive(false);
            //frontWall.SetActive(true);
        }
        else
        {
            engineRoom.SetActive(true);
            //frontWall.SetActive(false);
        }
    }

    public void SetEnabled(bool value)
    {
        isEnabled = value;
        float angle = VRTK.VRTK_DeviceFinder.HeadsetTransform().rotation.eulerAngles.y;
        Orientation current = GetOrientation(angle);
        if(current == Orientation.NE || current == Orientation.SE) {
            panel.text = "EngineRoom >\n< Cockpit";
        } else {
            panel.text = "< EngineRoom\nCockpit >";
        }
    }
}
