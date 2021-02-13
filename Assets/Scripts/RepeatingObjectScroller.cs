using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingObjectScroller : MonoBehaviour {

    [SerializeField] private float scrollSpeed = 5f;    

    private Vector3 startPosition;
    private float horizontalLength;

    private void Start() {
        horizontalLength = GetComponent<BoxCollider2D>().size.x;
        startPosition = transform.position;
    }

    private void Update() {
        // if (!GameController.GetInstance().GetIsGameOver()) {
        //     float newPosition = Mathf.Repeat(Time.time * scrollSpeed, horizontalLength);
        //     transform.position = startPosition + Vector3.right * newPosition;
        // }
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, horizontalLength);
        transform.position = startPosition + Vector3.right * newPosition;
    }
}
