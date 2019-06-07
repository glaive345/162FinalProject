using System;
using UnityEngine;

namespace DeepSpace

{
    public interface IPublisher
    {
        void Unregister(bool);

        void Register(bool);

        void Notify(bool);
    }
}