using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePoolOnContactWithTag : MonoBehaviour {

    [SerializeField] private string gameObjectTag;

    private bool tagExists;

    private void Awake() {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++) {
            if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(gameObjectTag)) {
                Debug.Log (gameObjectTag + " found at index + " + i);
                tagExists = true;
                break;
            }
        }

        if (!tagExists) {
            GetComponent<RePoolOnContactWithTag>().enabled = false;
            Debug.Log("Tag does not exist. Script disabled.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (gameObjectTag != null && other.gameObject.CompareTag(gameObjectTag)) {
            gameObject.SetActive(false);
        }
    }
}
