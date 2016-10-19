using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SceneOneCombineScript : mouseDrag {

    public GameObject elementContainer;
    private bool _isLerping = false;
    private float _timeStartedLerping;
    public float timeTakenDuringLerp = 3.0f;
    private Vector2 _startPosition;
    private Vector2 _endPosition;


    void FixedUpdate() {
        if (_isLerping) {

            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;


            transform.localPosition = Vector2.Lerp(_startPosition, _endPosition, 3.0f);

            if (percentageComplete >= 3.0f) {
                _isLerping = false;
            }
        }
    }

    void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerping = Time.time;

        _startPosition = transform.position;
        //  _endPosition = transform.position + Vector3.forward * 5;
        _endPosition = new Vector2(transform.position.x, transform.position.y - 10);
    }


        void OnTriggerEnter2D(Collider2D other)
    {
        //  Combine(other.gameObject);
        Debug.Log("Damn ittt");
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

    public override void OnEndDrag(PointerEventData eventData) {
        if (gameObject.name == "Hydrogen2")
        {

        }
        else if (gameObject.name == "Oxygen") {
            StartLerping();
        }
    }
}
