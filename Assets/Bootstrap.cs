using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;

public class Bootstrap : MonoBehaviour
{
    public Mesh mesh;
    public Material material;

    [SerializeField] private int N = 10000;
    [SerializeField] private float rotation = 0f;
    [SerializeField] private float scale = 1f;

    // Start is called before the first frame update
    private void Start()
    {
        EntityManager entityManager = World.Active.EntityManager;
        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(Scale),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(MoveComponent)
            );
        NativeArray<Entity> entityArray = new NativeArray<Entity>(N, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);
        foreach (Entity entity in entityArray)
        {
            entityManager.SetComponentData(entity, new Translation 
            {
                Value = new float3(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(0f, 10f)) 
            });
            entityManager.SetComponentData(entity, new Rotation
            {
                Value = quaternion.EulerXYZ(0, rotation * Mathf.Deg2Rad, 0)
            });
            entityManager.SetComponentData(entity, new Scale
            {
                Value = scale
            });
            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material,
            });
            entityManager.SetComponentData(entity, new MoveComponent
            {
                Value = UnityEngine.Random.Range(1f, 4f),
            });
        }

        entityArray.Dispose();
    }

}
