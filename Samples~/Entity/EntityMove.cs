using System;
using EntitiesBT.Core;
using EntitiesBT.Components;
using EntitiesBT.DebugView;
using EntitiesBT.Entities;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace EntitiesBT.Sample
{
    public class EntityMove : BTNode<EntityMoveNode, EntityMoveNode.Data>
    {
        public Vector3 Velocity;

        protected override void Build(ref EntityMoveNode.Data data, ITreeNode<INodeDataBuilder>[] builders)
        {
            data.Velocity = Velocity;
        }
    }
    
    [BehaviorNode("F5C2EE7E-690A-4B5C-9489-FB362C949192")]
    public class EntityMoveNode
    {
        public static readonly ComponentType[] Types = {
            ComponentType.ReadWrite<Translation>()
          , ComponentType.ReadOnly<BehaviorTreeTickDeltaTime>()
        };
        
        [Serializable]
        public struct Data : INodeData
        {
            public float3 Velocity;
        }

        public static NodeState Tick(int index, INodeBlob blob, IBlackboard bb)
        {
            ref var data = ref blob.GetNodeData<Data>(index);
            ref var translation = ref bb.GetDataRef<Translation>();
            var deltaTime = bb.GetData<BehaviorTreeTickDeltaTime>();
            translation.Value += data.Velocity * deltaTime.Value;
            return NodeState.Running;
        }
    }

    public class EntityMoveDebugView : BTDebugView<EntityMoveNode, EntityMoveNode.Data> {}
}
