using System;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public float percent = 1f;
    public float width = 15f;
    public float height = 1.5f;
    public float maxHealth = 100f;
    public float damage = 0f;
    public float originalHealth = 0f;
    public float health;
    public MeshFilter primaryHealthRenderer;
    public MeshFilter secondaryHealthRenderer;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.originalHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        if (health != this.health)
        {
            float healthDifference = health-this.health;
            damage += healthDifference;
        }
        this.health = health;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        percent += 0.1f*damage/maxHealth;
        damage *= 0.9f;
        if (Math.Abs(damage) < 0.001)
        {
            originalHealth = health + (originalHealth-health)*0.99f;
        }
        float actualWidth = width*percent;
        primaryHealthRenderer.mesh = MeshFor(actualWidth, height, 0f, percent);
        secondaryHealthRenderer.mesh = MeshFor(width*originalHealth/maxHealth, height, 0f, 0f);
    }

    Mesh MeshFor(float width, float height, float uStart, float uEnd)
    {
        Mesh mesh = new();
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
        };
        mesh.vertices = vertices;

        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(uStart, 0),
            new Vector2(uEnd, 0),
            new Vector2(uStart, 1),
            new Vector2(uEnd, 1)
        };
        mesh.uv = uv;
        return mesh;
    }
}
