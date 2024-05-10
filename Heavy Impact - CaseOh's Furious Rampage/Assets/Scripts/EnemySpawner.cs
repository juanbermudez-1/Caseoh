using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject alien;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnInclineRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }
    private void Update()
    {
        spawnTime = spawnTime * (1 - (Time.deltaTime * spawnInclineRate));
    }
    IEnumerator EnemySpawn()
    {
        float rand1 = Random.Range(0, 10);
        yield return new WaitForSeconds(spawnTime);
        
        GameObject alien1 = Instantiate(alien, GenerateVectorOutsideCamera(),Quaternion.identity);
        if(rand1>=1)alien1.GetComponent<AIDestinationSetter>().target = GameObject.Find("Caseoh").transform;
        else alien1.GetComponent<AIDestinationSetter>().target = GameObject.Find("Vault").transform;
        StartCoroutine(EnemySpawn());
    }
    Vector3 GenerateVectorOutsideCamera()
    {
        float camLimit1 = 13;
        float camLimit2 = 7;
        float randX = Random.Range(-23, 23);
        float randY = Random.Range(-23, 23);
        Vector3 randPos = new Vector3(randX, randY, 0);
        if (randPos.x > camLimit1+ Camera.main.transform.position.x || randPos.x < -camLimit1 + Camera.main.transform.position.x || randPos.y > camLimit2+ Camera.main.transform.position.y || randPos.y < -camLimit2 + Camera.main.transform.position.y)
        {
            return randPos;
        }
        return GenerateVectorOutsideCamera();
    }
}
