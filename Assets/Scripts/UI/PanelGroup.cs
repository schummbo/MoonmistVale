using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour
{
    [SerializeField] private List<GameObject> Panels;

    public void Show(int idPanel)
    {
        for (int i = 0; i < Panels.Count; i++)
        {
            var panel = Panels[i];

            panel.SetActive(i == idPanel);
        }
    }
}
