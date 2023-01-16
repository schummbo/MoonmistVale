using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    [SerializeField] public GameObject Highlighter;

    private GameObject currentTarget;

    public void Highlight(GameObject target)
    {
        if (target == currentTarget)
        {
            return;
        }

        currentTarget = target;

        Vector2 position = target.transform.position;

        Highlight(position);
    }

    private void Highlight(Vector2 position)
    {
         Highlighter.SetActive(true);
         Highlighter.transform.position = position;
    }

    public void Hide()
    {
        currentTarget = null;
        Highlighter.SetActive(false);
    }
}
