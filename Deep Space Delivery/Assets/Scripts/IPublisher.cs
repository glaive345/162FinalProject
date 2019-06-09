using System;
using UnityEngine;

namespace DeepSpace

{
    public interface IPublisher
    {
        void Unregister(System.Action<bool> success, System.Action<float> degreeOfSuccess, System.Action<string> endState);

        void Register(System.Action<bool> success, System.Action<float> degreeOfSuccess, System.Action<string> endState);

        void Notify(bool success, float degreeOfSuccess, string endState);
    }
}