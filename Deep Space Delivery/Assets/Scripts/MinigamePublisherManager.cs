using System;
using UnityEngine;
using DeepSpace
// change name to Publisher Manager
public class MinigamePublisherManager : MonoBehaviour
{

    public int GroupCount { get; } = 5;
    private IPublisher MiniGame1Publisher = new Publisher();
    private IPublisher MiniGame2Publisher = new Publisher();
    private IPublisher Minigame3Publisher = new Publisher();
    private IPublisher MiniGame4Publisher = new Publisher();
    private IPublisher Minigame5Publisher = new Publisher();



    public void SendMessageWithPublisher(int group, bool activated)
    {

        switch (group)
        {
            case 1:
                MiniGame1Publisher.Notify(activated);
                break;
            case 2:
                MiniGame2Publisher.Notify(activated);
                break;
            case 3:
                Minigame3Publisher.Notify(activated);
                break;
            case 4:
                MiniGame4Publisher.Notify(activated);
                break;
            case 5:
                Minigame5Publisher.Notify(activated);
                break;

        }
    }
    public void Register(int group, bool callback)
    {
        switch (group)
        {
            case 1:
                MiniGame1Publisher.Register(callback);
                break;
            case 2:
                MiniGame2Publisher.Register(callback);
                break;
            case 3:
                Minigame3Publisher.Register(callback);
                break;
            case 4:
                MiniGame4Publisher.Register(callback);
                break;
            case 5:
                Minigame5Publisher.Register(callback);
                break;
        }
    }
    public void Unregister(int group, bool callback)
    {
        switch (group)
        {
            case 1:
                MiniGame1Publisher.Unregister(callback);
                break;
            case 2:
                MiniGame2Publisher.Unregister(callback);
                break;
            case 3:
                Minigame3Publisher.Unregister(callback);
                break;
            case 4:
                MiniGame4Publisher.Unregister(callback);
                break;
            case 5:
                Minigame5Publisher.Unregister(callback);
                break;
        }
    }
}
