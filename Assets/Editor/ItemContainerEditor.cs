using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemContainer))]
public class ItemContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemContainer container = target as ItemContainer;

        if (GUILayout.Button("Clear Container"))
        {
            foreach (var containerItemSlot in container.ItemSlots)
            {
                containerItemSlot.Item = null;
                containerItemSlot.Count = 0;
            }
        }


        DrawDefaultInspector();
    }
}
