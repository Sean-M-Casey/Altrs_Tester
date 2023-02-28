using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinking : MonoBehaviour
{
    public GameObject eyelidBlink;
    public GameObject iris;
    private float blinkTime = 0.05f;
    private float eyeOpenTime;
    private float minTime = 0.5f;
    private float maxTime = 5.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("EyesOpen");
    }

    IEnumerator EyesOpen()
    {
        eyeOpenTime = Random.Range(minTime, maxTime);
        eyelidBlink.SetActive(false);
        iris.SetActive(true);
        yield return new WaitForSeconds(eyeOpenTime);
        StartCoroutine("Blink");
    }
    IEnumerator Blink()
    {
        eyelidBlink.SetActive(true);
        iris.SetActive(false);
        yield return new WaitForSeconds(blinkTime);
        StartCoroutine("EyesOpen");
    }
}
