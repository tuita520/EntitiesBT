using System;
using EntitiesBT.Components;
using EntitiesBT.Core;
using EntitiesBT.DebugView;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace EntitiesBT.Sample
{
    public class IsEntityPositionInBox : BTNode<IsEntityPositionInBoxNode, IsEntityPositionInBoxNode.Data>
    {
        public BoxCollider Box;
        
        protected override void Build(ref IsEntityPositionInBoxNode.Data data, ITreeNode<INodeDataBuilder>[] builders)
        {
            // rotation is not count into.
            data.Bounds = new Bounds(Box.center + transform.position, Box.size);
        }
    }
    
    [BehaviorNode("404DBF2F-A83B-4FF8-B755-F2A6D6836793")]
    public class IsEntityPositionInBoxNode
    {
        public static readonly ComponentType[] Types = { ComponentType.ReadOnly<Translation>() };
        
        [Serializable]
        public struct Data : INodeData
        {
            public Bounds Bounds;
        }

        public static NodeState Tick(int index, INodeBlob blob, IBlackboard bb)
        {
            ref var data = ref blob.GetNodeData<Data>(index);
            var translation = bb.GetData<Translation>();
            return data.Bounds.Contains(translation.Value) ? NodeState.Success : NodeState.Failure;
        }
    }
    
    public class IsEntityPositionInBoxNodeDebugView : BTDebugView<IsEntityPositionInBoxNode, IsEntityPositionInBoxNode.Data> {}
}
