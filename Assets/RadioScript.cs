using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VRTK.VRTK_InteractableObject>().InteractableObjectTouched +=
            new VRTK.InteractableObjectEventHandler((sender, arg) => audio.enabled = !audio.enabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
