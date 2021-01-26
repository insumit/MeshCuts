using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleSpawner;
    public GameObject particles, targetObject;

    public Camera cam;
    private Vector3 camPos, targetPos;

    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        targetPos = targetObject.transform.position;

        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            particleSpawner.Emit(1);

            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPos, Time.deltaTime);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleSpawner, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
            SpawnAtLocation(collisionEvents[i]);
    }

    private void SpawnAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        particles.transform.position = particleCollisionEvent.intersection;
        particles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal)
;
        Instantiate(particles, particles.transform.position, particles.transform.rotation);   
    }
}
