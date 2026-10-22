using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class GestureParticles : MonoBehaviour
{
    ParticleSystem particleSystem;
    public Camera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddParticles(List<GestureSegment> gesture)
    {
        foreach (GestureSegment segment in gesture)
        {
            for (float i = 0f; i < segment.length; i += 2.5f)
            {
                Vector2 position = camera.ScreenToWorldPoint(segment.start+new Vector2(Mathf.Cos(segment.angle), Mathf.Sin(segment.angle))*i);
                var emitParams = new ParticleSystem.EmitParams();
                emitParams.position = position+UnityEngine.Random.onUnitCircle*0.1f;
                emitParams.velocity = UnityEngine.Random.onUnitSphere*0.5f;
                emitParams.startLifetime = UnityEngine.Random.Range(0.35f, 0.65f);
                emitParams.startSize = UnityEngine.Random.Range(0.15f, 0.25f);
                particleSystem.Emit(emitParams, 1);
            }
        }
    }
}
