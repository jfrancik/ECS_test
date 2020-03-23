using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MoveComponent moveComponent) =>
        {
            translation.Value.z -= moveComponent.Value * Time.deltaTime;
            if (translation.Value.z < 0) translation.Value.z = 10f;
        });
    }
}
