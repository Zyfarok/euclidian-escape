using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumpadState : MonoBehaviour
{
    string code = "4215";
    string current = "0000";
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 9; i++)
        {
            int number = i;
            GameObject key = GameObject.Find("Key" + i);
            key.GetComponent<VRTK.VRTK_InteractableObject>().InteractableObjectTouched +=
                new VRTK.InteractableObjectEventHandler((sender, args) => KeyPress(number));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void checkCode()
    {
        if (current.Equals(code))
        {
            // trigger non-euclidian room and print message
            GameObject scripts = GameObject.Find("Scripts");
            NonEuclidianGeometryChange negc = scripts.GetComponent<NonEuclidianGeometryChange>();
            negc.SetEnabled(true);
            GameObject console = GameObject.Find("ConsoleText");
            TMPro.TextMeshPro tm = console.GetComponent<TMPro.TextMeshPro>();
            tm.text += "\nCorrect password! Maintenance room enabled.";
        }
    }
    public void KeyPress(int number)
    {
        current = current.Substring(1, 3) + number;
        checkCode();
    }
}
