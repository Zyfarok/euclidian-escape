using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    Light l = null;
    const float delta = 0.5f;
    int[] code = { 4, 2, 1, 5 };

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartBlink()
    {
        BlinkCode();
    }

    void BlinkCode()
    {
        float time = 0;
        foreach (int digit in code)
        {
            for (int i = 0; i < digit; i++)
            {
                Invoke("TurnOn", time);
                time += delta;
                Invoke("TurnOff", time);
                time += delta;
            }
            time += 2 * delta;
        }
        time += 6 * delta;
        Invoke("BlinkCode", time);
    }

    void TurnOn()
    {
        l.intensity = 3;
    }

    void TurnOff()
    {
        l.intensity = 1;
    }
}

