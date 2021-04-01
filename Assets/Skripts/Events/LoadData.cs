﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private readonly List<Action> _callbacks = new List<Action>();
    public void Subscribe(Action callback)
    {
        _callbacks.Add(callback);
    }

    public void Delete(Action action)
    {
        _callbacks.Remove(action);
    }

    public void Publish()
    {
        foreach (var callback in _callbacks)
            callback();
    }
}
