using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private readonly List<Action<string>> _callbacks = new List<Action<string>>();
    public void Subscribe(Action<string> callback)
    {
        _callbacks.Add(callback);
    }

    public void Delete(Action<string> action)
    {
        _callbacks.Remove(action);
    }

    public void Publish(string data, char separator)
    {
        var strs = data.Split(new char[] { separator });
        var index = 0;
        foreach (var callback in _callbacks)
            try
            {
                callback(strs[index++]);
            }
            catch
            {

            }
    }
}
