﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public string interactText;

    public virtual void Interact()
    {

    }

    public string GetInteractionText()
    {
        return interactText;
    }
}
