using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonEuclidianGeometryChange : MonoBehaviour
{
    private enum Orientation
    {
        None = -1, NE, NW, SW, SE
    }


    GameObject engineRoom = null;
    GameObject frontWall = null;

    bool isEnabled = false;
    Orientation lastOrientation = Orientation.None;

    // Start is called before the first frame update
    void Start()
    {
        engineRoom = GameObject.Find("EngineRoom");
        frontWall = GameObject.Find("CockpitFrontWall");
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
                return Orientation.NW;
            case 1:
                return Orientation.SW;
            case 2:
                return Orientation.SE;
            default:
                return Orientation.NE;
        }
    }

    private void SwapFront()
    {
        if (engineRoom.activeSelf)
        {
            engineRoom.SetActive(false);
            frontWall.SetActive(true);
        }
        else
        {
            engineRoom.SetActive(true);
            frontWall.SetActive(false);
        }
    }

    public void SetEnabled(bool value)
    {
        isEnabled = value;
    }
}
