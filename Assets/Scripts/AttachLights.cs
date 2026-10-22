using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class AttachLights : MonoBehaviour {

    public Light lightPrefab;

    void Start() {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var lights = ps.lights;
        lights.enabled = true;
        lights.ratio = 0.5f;
        lights.light = lightPrefab;
    }
}