using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class PoolSpawner : MonoBehaviour {

    [SerializeField] private float spawnXPos = 25f;
    [SerializeField] private float spawnYPosMin = -2f;
    [SerializeField] private float spawnYPosMax = 10.5f;
    [SerializeField] private float initSpawnTime = 1f;
    [SerializeField] private float spawnTimeRate = 1f;
    [SerializeField] private int[] pointsPos = {
        1, 3, 6, 10, 30, 60, 100, 200
    };

    private ObjectPooler objectPooler;
    private float lastSpawnTime;
    private int currentSpawnedIndex;
    private int spawnCounter;
    private int pointsPosIndex;

    private void Awake() {
        lastSpawnTime = initSpawnTime;
        currentSpawnedIndex = 0;
        spawnCounter = 0;
        pointsPosIndex = 0;
    }

    private void Start() {
        objectPooler = GetComponent<ObjectPooler>();
    }

    private void Update() {
        bool isGameOver = GameController.GetInstance().GetIsGameOver();

        lastSpawnTime += Time.deltaTime;

        // if (!isGameOver && lastSpawnTime >= spawnTimeRate) {
        if (lastSpawnTime >= spawnTimeRate) {
            lastSpawnTime = 0;

            float spawnYPos = Random.Range(spawnYPosMin, spawnYPosMax);
            Vector2 spawnPosition = new Vector2(spawnXPos, spawnYPos);
            Vector2 spawnRotation = Vector2.zero;
            Spawn(spawnPosition, spawnRotation);

            ++currentSpawnedIndex;
            ++spawnCounter;

            if (currentSpawnedIndex >= objectPooler.GetPoolSize()) {
                currentSpawnedIndex = 0;
            }
        }
    }

    private void Spawn(Vector2 position, Vector2 rotation) {
        GameObject obj = objectPooler.GetPooledObject();

        if (obj == null) {
            Debug.Log("Object pooled is null.");
            return;
        }
        obj.transform.position = position;
        obj.transform.eulerAngles = rotation;
        obj.SetActive(true);

        for (int i = 0; i < obj.transform.childCount; i++) {
            GameObject childObj = obj.transform.GetChild(i).gameObject;

            if (childObj.CompareTag("Points")) {
                if (spawnCounter + 1 == pointsPos[pointsPosIndex]
                    && pointsPosIndex < pointsPos.Length) {
                        childObj.SetActive(true);
                        ++pointsPosIndex;
                } else {
                    childObj.SetActive(false);
                }
            } else {
                childObj.SetActive(true);
            }
        }
    }
}
