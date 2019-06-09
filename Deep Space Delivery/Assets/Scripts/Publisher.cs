using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace
{
    public class Publisher : IPublisher
    {
        private List<System.Action<bool>> RegListSuccess;
        private List<System.Action<float>> RegListDegree;
        private List<System.Action<string>> RegListState;

        //Constructor for Publisher makes a new list
        public Publisher()
        {
            RegListSuccess = new List<System.Action<bool>>();
            RegListDegree = new List<System.Action<float>>();
            RegListState = new List<System.Action<string>>();
        }

        void IPublisher.Notify(bool success, float degreeOfSuccess, string endState)
        {
            //Notifies each observer of the new destination in the RegList using the stored action
            foreach (System.Action<bool> element in RegListSuccess)
            {
                element(success);
            }
            foreach(System.Action<float> element in RegListDegree)
            {
                element(degreeOfSuccess);
            }
            foreach(System.Action<string> element in RegListState)
            {
                element(endState);
            }
        }

        //Interface to add to the RegList
        void IPublisher.Register(System.Action<bool> success, System.Action<float> degreeOfSuccess, System.Action<string> endState)
        {
            RegListSuccess.Add(success);
            RegListDegree.Add(degreeOfSuccess);
            RegListState.Add(endState);
        }
        //Interface to remove from the RegList
        void IPublisher.Unregister(System.Action<bool> success, System.Action<float> degreeOfSuccess, System.Action<string> endState)
        {
            RegListSuccess.Remove(success);
            RegListDegree.Remove(degreeOfSuccess);
            RegListState.Remove(endState);
        }
    }
}