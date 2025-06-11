using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float SpawnRadius = 10.0f;
    public GameObject MonsterPrefab;
    public Transform Player;
    public float SpawnInterval = 3.0f;
    private float Timer;

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= SpawnInterval)
        {
            Timer = 0.0f;
            SpawnMonsterAtEdge();
        }
    }

    void SpawnMonsterAtEdge() 
    {
        Vector3 spawnPos = GetRandomPointOnCircleEdge(Player.position, SpawnRadius);
        GameObject monster = Instantiate(MonsterPrefab, spawnPos, Quaternion.identity);
        monster.GetComponent<MonsterMovement>().Initialize(Player);
    }

    Vector3 GetRandomPointOnCircleEdge(Vector3 center, float radius)
    {
        float angle = Random.Range(0.0f, Mathf.PI * 2.0f);

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        return new Vector3(center.x + x, 0.0f, center.z + z);
    }
}
