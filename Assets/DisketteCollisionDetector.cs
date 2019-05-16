using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisketteCollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Diskette")
        {
            Destroy(other.gameObject);
            GameObject console = GameObject.Find("ConsoleText");
            Debug.Log(console);
            TMPro.TextMeshPro tm = console.GetComponent<TMPro.TextMeshPro>();
            tm.text = "Booting system...\nuser@host:~$";
            GameObject blinker = GameObject.Find("Blinker");
            blinker.GetComponent<Blink>().StartBlink();
        }
    }
}
