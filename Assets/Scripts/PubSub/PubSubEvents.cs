using System;
using UnityEngine;

namespace Assets.Scripts.PubSub
{
    [CreateAssetMenu(menuName = "System/Pub Sub/Events")]
    public class PubSubEvents : ScriptableObject
    {
        public Action OnScenePreChange { get; set; }

        public Action OnScenePostChange { get; set; }

        public Action<int> OnPhaseStarting { get; set; }

        public Action OnInventoryChange { get; set; }
    }
}
