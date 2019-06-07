using System;
using UnityEngine;

namespace DeepSpace

{
    public interface IPublisher
    {
        void Unregister(System.Action<bool> callback);

        void Register(System.Action<bool> callback);

        void Notify(bool callback);
    }
}