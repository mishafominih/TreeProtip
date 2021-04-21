using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private readonly List<Action<StringBuilder>> _callbacks = new List<Action<StringBuilder>>();
    public void Subscribe(Action<StringBuilder> callback)
    {
        _callbacks.Add(callback);
    }

    public void Delete(Action<StringBuilder> action)
    {
        _callbacks.Remove(action);
    }

    public string Publish(char separator)
    {
        var stream = new StringBuilder();
        foreach (var callback in _callbacks)
        {
            callback(stream);
            stream.Append(separator);
        }
        return stream.ToString();
    }
}
