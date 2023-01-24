using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PositioningUtilities
{
    public static IEnumerable<T> GetObjectsNearPosition<T>(Vector2 position, float sizeOfArea) where T : MonoBehaviour
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfArea);

        return colliders.Where(c => c.GetComponent<T>() != null).Select(c => c.GetComponent<T>());
    }
}
