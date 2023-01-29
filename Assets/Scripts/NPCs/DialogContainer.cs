using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialog/Dialog")]
public class DialogContainer : ScriptableObject
{
    public List<string> Lines;
    public Actor Actor;
}
