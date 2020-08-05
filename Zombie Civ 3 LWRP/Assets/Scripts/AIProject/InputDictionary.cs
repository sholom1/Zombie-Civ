using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class InputDictionary : ScriptableObject
{
    public KeyCode Interact;
    public KeyCode Use;
    public KeyCode Cancel = KeyCode.Escape;
    public KeyCode Mouse0 = KeyCode.Mouse0;
}
