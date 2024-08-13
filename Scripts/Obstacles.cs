using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacles : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject cannon;
    public GameObject rotor;
    public GameObject chain;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public float spawnInterval = 2f;
    public float sphereLifetime = 10f;
    public float sphereForce = 25f;
    public float cannonRotationSpeed = 20f;
    public float rotationAngle = 30f;
    public float rotorRotationSpeed = 300f;

    public float moveDistance = 2.5f; 
    public float moveSpeed = 2f; 
    public float waitTime = 1f; 

    private Vector3 startPos;
    private bool movingRight = true;
    void Start()
    {
        if (chain != null)
        {
            startPos = chain.transform.position;
            StartCoroutine(MoveChain());
        }
        else
        {
            Debug.LogWarning("Chain object is not assigned, skipping chain movement.");
        }

        spherePrefab = Resources.Load<GameObject>("Prefabs/SpherePrefab");
        cannon = GameObject.Find("Cannon");
        rotor = GameObject.Find("Rotor");
        chain = GameObject.Find("Chain");

        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SphereSpawn");
        foreach (GameObject spawnPointObject in spawnPointObjects)
        {
            spawnPoints.Add(spawnPointObject);
        }

        StartCoroutine(SpawnSpheres());
        StartCoroutine(RotateCannon());
    }

    private void Update()
    {
        RotateRotor();
       
    }

    IEnumerator MoveChain()
    {
        if (chain == null)
        {
            Debug.LogWarning("Chain object is not assigned.");
            yield break;
        }

        while (true)
        {
            Vector3 targetPos = movingRight ? startPos + Vector3.right * moveDistance : startPos - Vector3.right * moveDistance;

            while (Vector3.Distance(chain.transform.position, targetPos) > 0.1f)
            {
                if (chain == null)
                {
                    Debug.LogWarning("Chain object was destroyed or is not assigned.");
                    yield break;
                }

                chain.transform.position = Vector3.MoveTowards(chain.transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
      
            yield return new WaitForSeconds(waitTime);   
            movingRight = !movingRight;
        }
    }

    IEnumerator SpawnSpheres()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (spherePrefab != null && spawnPoints.Count > 0)
            {
                foreach (GameObject spawnPoint in spawnPoints)
                {
                    Vector3 spawnPosition = spawnPoint.transform.position;
                    GameObject sphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
                    Rigidbody rb = sphere.GetComponent<Rigidbody>();

                    if (rb == null)
                    {
                        rb = sphere.AddComponent<Rigidbody>();
                    }
             
                    Vector3 spawnDirection = spawnPoint.transform.forward;

                    rb.AddForce(spawnDirection * sphereForce, ForceMode.Impulse);
                    Destroy(sphere, sphereLifetime);
                }
            }
            else
            {
                Debug.LogWarning("spherePrefab or spawnPoints are not assigned.");
            }
        }
    }

    IEnumerator RotateCannon()
    {
        while (true)
        {
            yield return RotateCannonCoroutine(rotationAngle);
            yield return RotateCannonCoroutine(-rotationAngle);
        }
    }

    IEnumerator RotateCannonCoroutine(float targetAngle)
    {
        Quaternion startRotation = cannon.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f) * startRotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * cannonRotationSpeed / Mathf.Abs(targetAngle);
            cannon.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }
    }

    void RotateRotor()
    {
        if (rotor != null)
        {
            rotor.transform.Rotate(Vector3.up, rotorRotationSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Rotor object is not assigned.");
        }
    }
}
