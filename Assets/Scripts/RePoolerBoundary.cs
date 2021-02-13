using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePoolerBoundary : MonoBehaviour {
    
    private void OnTriggerExit2D(Collider2D other) {
        other.transform.root.gameObject.SetActive(false);
    }
}
