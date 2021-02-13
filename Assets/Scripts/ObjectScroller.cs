using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScroller : MonoBehaviour {

    [SerializeField] private float scrollSpeed = -13f;    

    private Vector3 startPosition;
    private float _time;

    private void OnEnable() {
        _time = 0;
        startPosition = transform.position;
    }

    private void Update() {
        _time += Time.deltaTime;
        float newPosition = _time * scrollSpeed;
        transform.position = startPosition + Vector3.right * newPosition;
    }
}
