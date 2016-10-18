using UnityEngine;
using System.Collections;

public class SceneOneCombineScript : MonoBehaviour {

    public GameObject elementContainer;

    void OnTriggerEnter2D(Collider2D other)
    {
        Combine(other.gameObject);
    }

    void Combine(GameObject element) {
        elementContainer.transform.localPosition = gameObject.transform.localPosition;
        gameObject.transform.SetParent(element.transform);
        element.transform.SetParent(elementContainer.transform);

        //IList<Transform> transforms = new IList<Transform>();
        
        //foreach(Transform child in transform) {
        //    transforms.Add(child);
        //}

        //foreach (Transform child in transforms) {
        //    child = element;
        //}
    }
}
