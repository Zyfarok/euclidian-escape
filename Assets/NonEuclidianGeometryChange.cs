using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonEuclidianGeometryChange : MonoBehaviour
{
    private enum Orientation
    {
        None = -1, NE, NW, SW, SE
    }
    private enum Direction
    {
        None = -1, Front, Right, Back, Left
    }

    public bool isEnabled = false;

    public bool exitEnabled = false;

    public bool almostEnd = false;
    public bool end = false;

    public GameObject engineRoom;
    //GameObject frontWall = null;
    public TMPro.TextMeshPro panel;
    public TMPro.TextMeshPro console;

    public GameObject exitDoor;

    public GameObject env;
    public MonoBehaviour EndScript;
    
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
        if(end) {
            if(VRTK.VRTK_DeviceFinder.HeadsetTransform().position.z < -1.0f) {
                env.transform.Translate(new Vector3(0,0,0.03f));
                EndScript.enabled = true;
            }
            return;
        }
        
        float angle = VRTK.VRTK_DeviceFinder.HeadsetTransform().rotation.eulerAngles.y;

        if(almostEnd) {
            Direction direction = getDirection(angle);
            if(direction == Direction.Front) {
                engineRoom.SetActive(false);
                end = true;
            }
            return;
        }
        
        if(lastOrientation == Orientation.None) {
            lastOrientation = GetOrientation(angle);
        }
        
        if(exitEnabled) {
            Direction direction = getDirection(angle);
            if(isEnabled) {
                if(!engineRoom.activeSelf && direction == Direction.Front) {
                    console.text += "\nBackward exit enabled.";
                    panel.text = "BACKWARD EXIT";
                    isEnabled = false;
                }
            }
            if(!isEnabled) {
                if(VRTK.VRTK_DeviceFinder.HeadsetTransform().position.z > -0.8) {
                    if(direction == Direction.Front) {
                        exitDoor.SetActive(false);
                    } else {
                        exitDoor.SetActive(true);
                    }
                } else if (exitDoor.activeSelf == false) {
                    exitEnabled = false;
                    almostEnd = true;
                }
                return;
            }
        }
        
        if (isEnabled) {
            Orientation current = GetOrientation(angle);

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

            lastOrientation = current;
        }
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

    private Direction getDirection(float angle)
    {
        int i = (int)Mathf.Floor((angle + 45) / 90) % 4;
        if (i < 0)
        {
            i += 4;
        }

        switch (i)
        {
            case 0:
                return Direction.Front;
            case 1:
                return Direction.Right;
            case 2:
                return Direction.Back;
            default:
                return Direction.Left;
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

    public void EnableExit(){
        exitEnabled = true;
    }
}
