using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SharkScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public GameObject SharkTimer;
    public GameObject player;

    public float startAngle = 0f;
    public float fov = 90f;
    public int rayCount = 50;
    public float viewDistance = 50f;
    public float attackTime = 3f;

    private bool isTimerOn = false;
    private DateTime startTime;

    private Mesh mesh;

    void Start()
    {
        startTime = DateTime.Now;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Update()
    {
        float angle = startAngle;
        Vector3 origin = transform.position;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++) {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);

            if (raycastHit2D.collider == null) {
                // No hit
                vertex = GetVectorFromAngle(angle) * viewDistance;
            } else {
                // Hit object
                vertex = raycastHit2D.point - (Vector2)origin;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;   
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        if (isPlayerVisible() && isPlayerEmptyPath()) {
            TimeSpan timeDifference = DateTime.Now - startTime;

            if (isTimerOn && timeDifference.TotalSeconds > attackTime) {
                // Kill
                Debug.Log("Kill");
            }

            // Debug.Log("Player visible");
            if (!isTimerOn) {
                startTime = DateTime.Now;
            }

            SharkTimer.SetActive(true);
            SharkTimerScript.SetSeconds(attackTime - timeDifference.TotalSeconds);

            isTimerOn = true;
        } else {
            if (isTimerOn) {
                // Disable shark timer
                SharkTimer.SetActive(false);
            }

            isTimerOn = false;
        }
    }

    // Checks if there is nothing between current object and player
    bool isPlayerEmptyPath()
    {
        Vector2 direction = player.transform.position - transform.position;
        Ray2D ray = new Ray2D(transform.position, direction);
        float maxDistance = direction.magnitude;
        
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDistance, layerMask);
        
        if (hit.collider == null) {
            return true;
        }

        if (hit.collider.name == "Ground") {
            return false;
        }

        Debug.Log(hit.collider.name);
        return false;
    }

    // Checks if player is in angle range 
    bool isPlayerVisible()
    {
        float target = getAngle(player) % 180;
        float diff = Mathf.Abs(startAngle - (fov / 2) - target);
        if (diff >= 180) {
            diff = 360 - diff;
        }
        
        return diff <= fov / 2;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    float getAngle(GameObject gameObject)
    {
        return Mathf.Atan2(
            (gameObject.transform.position.y - transform.position.y),
            (gameObject.transform.position.x - transform.position.x)
        ) * (180 / Mathf.PI);
    }
}
