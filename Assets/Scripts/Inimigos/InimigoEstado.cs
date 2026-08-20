using System;
using UnityEngine;

public abstract class InimigoEstado : MonoBehaviour
{
    public abstract void OnEnter();

    public abstract Type OnUpdate();

    public abstract void OnExit();
}
