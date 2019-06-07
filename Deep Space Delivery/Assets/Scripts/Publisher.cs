using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace
{
    public class Publisher : IPublisher
    {
        private List<System.Action<bool>> RegList;

        //Constructor for Publisher makes a new list
        public Publisher()
        {
            RegList = new List<System.Action<bool>>();
        }

        void IPublisher.Notify(bool callback)
        {
            //Notifies each observer of the new destination in the RegList using the stored action
            foreach (System.Action<bool> element in RegList)
            {
                element(callback);
            }
        }

        //Interface to add to the RegList
        void IPublisher.Register(System.Action<bool> callback)
        {
            RegList.Add(callback);
        }
        //Interface to remove from the RegList
        void IPublisher.Unregister(System.Action<bool> callback)
        {
            RegList.Remove(callback);
        }
    }
}