using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2TutorialStart : MonoBehaviour
{

    public GameObject atom1;
    public GameObject atom2;
    public GameObject Hand;
    public GameObject TutorialOverlay;
    public GameObject SplashOverlay;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitAndMove(3.0f));
        StartCoroutine(WaitAndMove(8.0f));
        StartCoroutine(DestroyTutorial(12.0f));
    }

    IEnumerator WaitAndMove(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // start at time X
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= 3)
        { // until one second passed
            Hand.transform.localPosition = Vector3.Lerp(atom1.transform.localPosition, atom2.transform.localPosition, Time.time - startTime); // lerp from A to B in three second
            yield return 1; // wait for next frame
        }
    }

    IEnumerator DestroyTutorial(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        Hand.SetActive(false);
        TutorialOverlay.SetActive(false);
        SplashOverlay.SetActive(false);
    }
}
