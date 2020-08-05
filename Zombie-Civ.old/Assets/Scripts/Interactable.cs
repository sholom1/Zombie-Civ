using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieCiv.Tools
{
    public abstract class Interactable : MonoBehaviour
    {
        public string actionInfo;
        public abstract void Use(Interact player);
    }
}
