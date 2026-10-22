using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestureCapture : MonoBehaviour
{
    public float baseSampleRate = 1f/30f;
    public Camera camera;
    public GameObject player;
    public Sprite debugDot;
    public SpellActions spellActions;
    public GestureParticles gestureParticles;
    float lastSampleTime;
    GestureRecognizer gestureRecognizer = new();
    GestureClassifier gestureClassifier = new();
    Vector2 lastMousePosition = new();
    bool gestureHappening = false;
    List<GameObject> debug = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSampleTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time-lastSampleTime > baseSampleRate)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            if ((mousePosition-lastMousePosition).magnitude < 1.0)
            {
                if (gestureHappening && Time.time-lastSampleTime > baseSampleRate*5.0)
                {
                    foreach (GameObject o in debug)
                    {
                        Destroy(o);
                    }
                    debug = new();
                    List<GestureSegment> characterizedGesture = gestureRecognizer.CharacterizeSamples(p => {
                        AddDot(p);
                    });
                    // DebugDrawGesture(characterizedGesture);
                    if (characterizedGesture.Count > 1) {
                        ClassifiedGesture shape = gestureClassifier.ClassifyGesture(characterizedGesture, new(player.transform.position, camera));
                        if (shape != null) {
                            gestureParticles.AddParticles(characterizedGesture);
                            player.transform.rotation = Quaternion.LookRotation(transform.forward, shape.direction);
                            if (shape.gestureType == ClassifiedGestureType.Carrot)
                            {
                                spellActions.Dash();
                                
                            }
                            if (shape.gestureType == ClassifiedGestureType.Triangle)
                            {
                                spellActions.FireBallSpell();
                            }
                            if (shape.gestureType == ClassifiedGestureType.Square)
                            {
                                spellActions.AOEBlast();
                            }
                        }
                    }
                    gestureRecognizer = new();
                    gestureHappening = false;
                    lastSampleTime = Time.time;
                    lastMousePosition = mousePosition;
                } else
                {
                    // Debug.Log("no movement, no current gesture");
                }
            } else
            {
                gestureRecognizer.AddSample(mousePosition);
                gestureHappening = true;
                lastSampleTime += baseSampleRate;
                lastMousePosition = mousePosition;
                // Debug.Log("added sample");

            }
        }
    }

    void DebugDrawGesture(List<GestureSegment> characterizedGesture)
    {
        // Debug.Log("begin gesture");
        for (int i = 0; i < characterizedGesture.Count; i++)
        {
            Vector2 start =  characterizedGesture[i].start;
            Vector2 end = start+new Vector2(Mathf.Cos(characterizedGesture[i].angle), Mathf.Sin(characterizedGesture[i].angle))*characterizedGesture[i].length;
            // Debug.Log("start " +start + " end " + end);
            AddLine(ToVec3(camera.ScreenToWorldPoint(start), 0f), ToVec3(camera.ScreenToWorldPoint(end), 0f));
        }
    }

    Vector3 ToVec3(Vector2 xy, float z)
    {
        return new Vector3(xy.x, xy.y, z);
    }

    void AddLine(Vector3 start, Vector3 end)
    {
        if (start.x == float.NaN || start.y == float.NaN || end.x == float.NaN || end.y == float.NaN)
        {
            return;
        }
        // Add a LineRenderer component
        GameObject gameObject = new GameObject();
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Set the material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the color
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.green;

        // Set the width
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        // Set the number of vertices
        lineRenderer.positionCount = 2;

        // Set the positions of the vertices
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        debug.Add(gameObject);
    }

    void AddDot(DebugDot dot)
    {
        Vector2 start = ToVec3(camera.ScreenToWorldPoint(dot.start), 0f);
        if (start.x == float.NaN || start.y == float.NaN)
        {
            return;
        }
        // Add a LineRenderer component
        GameObject gameObject = new GameObject();
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();

        sr.sprite = debugDot;
        sr.color = dot.color;

        // Optional: set sorting layer/order
        sr.sortingLayerName = "Default";
        sr.sortingOrder = 0;
        gameObject.transform.position = start;
        gameObject.transform.localScale = new(dot.scale, dot.scale, dot.scale);

        // AddLine(start, start + new Vector2(Mathf.Cos(dot.angle), Mathf.Sin(dot.angle)));
        // if (dot.angle != float.NaN)
        // {
        //     GameObject angleObject = new GameObject();
        //     SpriteRenderer angleSr = angleObject.AddComponent<SpriteRenderer>();

        //     angleSr.sprite = debugDot;
        //     angleSr.color = Color.aliceBlue;

        //     // Optional: set sorting layer/order
        //     angleSr.sortingLayerName = "Default";
        //     angleSr.sortingOrder = 0;
        //     angleObject.transform.position = start + new Vector2(Mathf.Cos(dot.angle), Mathf.Sin(dot.angle))*2f;
        //     angleObject.transform.localScale = new(0.5f, 0.5f, 0.5f);
        //     debug.Add(angleObject);
        // }

        debug.Add(gameObject);
    }
}
