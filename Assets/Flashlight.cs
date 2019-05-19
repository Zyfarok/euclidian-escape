using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VRTK.VRTK_InteractableObject>().InteractableObjectUsed += new VRTK.InteractableObjectEventHandler((sender, args) => {
            bool newState = !light1.activeSelf; light1.SetActive(newState); light2.SetActive(newState); light3.SetActive(newState);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
