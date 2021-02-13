using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float flapForce = 1200f; 
    [SerializeField] private float flapRotation = 25f;
    [SerializeField] private float fallRotation = 0.75f;
    [SerializeField] private float gravityScale = 7f;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool isDead = false;

    // For logging
    float minVelocity; 
    float maxVelocity;

    private void Awake() {
        minVelocity = float.MaxValue; 
        maxVelocity = float.MinValue;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb2d.gravityScale = gravityScale;

        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX 
                | RigidbodyConstraints2D.FreezeRotation;
    }

    private void Start() {
        
    }

    private void Update() {
        if (isDead) {
            Death();
        } else {
            if (Input.GetMouseButtonDown(0)) {
                Flap();
            }
            Fall();
        }

        // LogVelocity();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!isDead && other.gameObject.CompareTag("Obstacle")) {
            isDead = true;
            GameController.GetInstance().GameOver();
        }
    }

    private void Flap() {
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(0f, flapForce));
        transform.rotation = Quaternion.Euler(0f, 0f, flapRotation);
        anim.SetTrigger("Flap");
    }

    private void Fall() {
        if (transform.eulerAngles.z <= 25 || transform.eulerAngles.z > 335) {
            transform.Rotate(Vector3.back * fallRotation);
        }
    }

    private void Death() {
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        GameController.GetInstance().PauseGame();
    }

    private void LogVelocity() {
        if (rb2d.velocity.y < minVelocity) {
            minVelocity = rb2d.velocity.y;
        }
        if (rb2d.velocity.y > maxVelocity) {
            maxVelocity = rb2d.velocity.y;
        }
        Debug.Log("Current velocity: " + rb2d.velocity 
                + " Min y velocity: " + minVelocity 
                + " Max y velocity: " + maxVelocity);
    }
}
