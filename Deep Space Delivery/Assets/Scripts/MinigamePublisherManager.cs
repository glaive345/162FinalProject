using System;
using UnityEngine;
using DeepSpace;
// change name to Publisher Manager
public class MinigamePublisherManager : MonoBehaviour
{

    public int GroupCount { get; } = 5;
    private IPublisher MiniGame1Publisher = new Publisher();
    private IPublisher MiniGame2Publisher = new Publisher();
    private IPublisher Minigame3Publisher = new Publisher();
    private IPublisher MiniGame4Publisher = new Publisher();
    private IPublisher Minigame5Publisher = new Publisher();



    public void SendMessageWithPublisher(string minigameName, bool success, float degreeOfSuccesss, string endState)
    {

        switch (minigameName)
        {
            case "Shield":
                MiniGame1Publisher.Notify(success, degreeOfSuccesss, endState);
                break;
            case "Laser":
                MiniGame2Publisher.Notify(success, degreeOfSuccesss, endState);
                break;
            case "Bridge":
                Minigame3Publisher.Notify(success, degreeOfSuccesss, endState);
                break;
            case "Missile":
                MiniGame4Publisher.Notify(success, degreeOfSuccesss, endState);
                break;
            case "Soda":
                Minigame5Publisher.Notify(success, degreeOfSuccesss, endState);
                break;

        }
    }
    public void Register(string minigameName, bool success, float degreeOfSuccess, string endState)
    {
        switch (minigameName)
        {
            case "Shield":
                MiniGame1Publisher.Register(success, degreeOfSuccess, endState);
                break;
            case "Laser":
                MiniGame2Publisher.Register(success, degreeOfSuccess, endState);
                break;
            case "Bridge":
                Minigame3Publisher.Register(success, degreeOfSuccess, endState);
                break;
            case "Missile":
                MiniGame4Publisher.Register(success, degreeOfSuccess, endState);
                break;
            case "Soda":
                Minigame5Publisher.Register(success, degreeOfSuccess, endState);
                break;
        }
    }
    public void Unregister(string minigameName, bool success, float degreeOfSuccess, string endState)
    {
        switch (minigameName)
        {
            case "Shield":
                MiniGame1Publisher.Unregister(success, degreeOfSuccess, endState);
                break;
            case "Laser":
                MiniGame2Publisher.Unregister(success, degreeOfSuccess, endState);
                break;
            case "Bridge":
                Minigame3Publisher.Unregister(success, degreeOfSuccess, endState);
                break;
            case "Missile":
                MiniGame4Publisher.Unregister(success, degreeOfSuccess, endState);
                break;
            case "Soda":
                Minigame5Publisher.Unregister(success, degreeOfSuccess, endState);
                break;
        }
    }
}
