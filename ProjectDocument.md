# Game Basic Information #

## Summary ##

Life is tough for a Deep Space Delivery Man. Try to reach your destination as your ship gets bombarded by asteroids, your computer is flooded with viruses, and worst of all, you are running low on soda. This is a two player co-op game where cooperation is essential for dealing with all of the emergencies that come your way. 

## Gameplay explanation ##
**In this section, explain how the game should be played. Treat this like a manual within a game. It is encouraged to explain the button mappings and the most optimal gameplay strategy.**<br/>
#### Basic controls: 
Player 1 moves with the WASD keys, and uses the F button for commands. 
Player 2 moves with the IJKL keys, and uses the H button for commands. 
Minigames are activated and played using only the Command button. 

The amount of minigames and what they do might be a little overwhelming at first, but hopefully this explanation will make everything a little bit more intuitive. 

#### Minigame 1: Refueling Game
This minigame is found at the leftmost part of the map. The goal of this minigame is to refuel your ship. There are three engines and thus three fuel gauges. If you let some run out, your ship speed (as shown in the upper left corner), will decrease. Refueling your ship will keep the ship going. 

The three bars represent your ships three fuel tanks. If some are empty your ship will move slower. Hold down the Command button to refuel and watch your fuel gauge increase. Let go of the command button to change which fuel gauge you are targeting. The main engine is more powerful than the auxilary ones.

#### Minigame 2: Missile Game
This minigame is found in the left-bottom corner of the map. Occasionally, an asteroid will appear, indicated by the message, "Asteroid approaching. Use missiles to shoot it down!". The goal of this minigame is to destroy the asteroids and keep your ship healthy.

Keep the yellow cursor on top of the red target. Once the bar to the right fills up, the asteroid will be destroyed. Be mindful of your Missile Count, which is shown on the bottom-left UI panel. This Missile Count will regenerate over time, and you will not be able to shoot asteroids if the count is 0. 

#### Minigame 3: Shields Game
This minigame is found in the middle-bottom corner of the map. The goal of this minigame is to replenish your Shields meter, which is shown on the bottom-left UI panel. Over time, this meter will decrease gradually, making your ship take more damage from asteroids if it is empty.  

Hold the Command Button to fill the middle bar. The more full the middle bar is before you release it the more your Shields will be replenished. If you time your release correctly and align the moving red bar with the green diamond when you release, you will gain 5 times the amount of Shields. 

#### Minigame 4: Laser Game
This minigame is found in the middle-top corner of the map. The goal of this minigame is to relieve boredom, which occasionally appears as an emergency. 

Press the Command Button to shoot lasers, and try to shoot down the asteroids on the screen. You cannot continue firing if the red bar on the bottom fills up, indicating that your lasers are currently overheated. 

#### Minigame 5: Virus Game
This minigame is found in the rightmost side of the map. Occasionally, the ship will be infested with viruses, which will slow the ship down. The goal of this minigame is to take care of this emergency. 

Press the Command Button to delete a pop-up window. Mash until all the windows are closed. Windows will gradually reappear over time.

#### Minigame 6: Refueling Oil Barrel Count
This action can be done in front of the two green barrels on the map. In order the refuel your ship, you must bring oil barrels to the Refueling game.

Press the Command Button to let your player carry an oil barrel. Now, navigate your character to the Refueling game. Hold the command button to tilt the barrel upwards. If you hold the command button after it has reached the top it will instead push it down, so you should release the button when you get the barrel upright. Be careful, as if the black barrel reaches 90 degrees, the barrel will spill, and you will have to start all over again. 

#### Minigame 7: Soda
This minigame is found in the upper-right corner of the map. Soda increases your players speed, which can be extremely valuable. 
To play, wait for the bar in the middle to fill up. The more it is filled up, the more speed boost your player will get. Drink the can by pressing the command button again

#### Minigame 8: Radar
this minigame is found at the top of the ship. Every 10 seconds a new scan will require review. Unreviewed scans will slow down your ship. At the station you have to count the number of blips there are in the scan by pressing the command button the right amount of times before the time bar on top of it finishes. If you are correct it will reduce the number of scans required to review by 1. Else you will have to try again.


# Main Roles #
Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content.

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based off the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your own relevant information. Liberally use the template when necessary and appropriate.

## User Interface
The UI in this game gives the user a clear overview of the status of the ship. We have a Time panel which displays the ship's progress towards its destination, an Alerts panel that displays any incoming emergencies, and more. 

*Observer Pattern* - Most of the UI in this game plays off of an observer pattern. For example, the Shields minigame publishes data to   the ShieldBarManager. The ShieldBarManager is watched by the UI, and updates to the Shield meter in this way. You can see how the ShieldBarManager informs the UI here: [ShieldBarManager](https://github.com/glaive345/162FinalProject/blob/7aedb5356aa1619ed8ca9e6dd5ec28e5be099e25/Deep%20Space%20Delivery/Assets/Scripts/UI%20Scripts/ShieldBarManager.cs#L7) (shieldBar is the UI element displaying the shield meter). The Shields minigame publishes information through the changeBar() function. 



## Movement/Physics
There are number of different movement and physics systems in this game, implemented in the different minigames and the players. 

*Lerp* - A number of objects make use of lerp. For example, the asteroid uses lerp to head towards the ship. Or the barrel in Minigame 6 uses lerp to accelerate to 90 degrees, as to imitate real life and gravity. [Lerp](https://github.com/glaive345/162FinalProject/blob/b9bdf6aef0ac79e108247b580c18e960e27de9d4/Deep%20Space%20Delivery/Assets/Scripts/Interactable%20Objects/MissileZone.cs#L112).

Some minigames use eccentric movement systems, like in the Shields game. There is a red bar which must move around a circular meter. We do this using the function RotateAround(). [Circular Motion](https://github.com/glaive345/162FinalProject/blob/b9bdf6aef0ac79e108247b580c18e960e27de9d4/Deep%20Space%20Delivery/Assets/Scripts/Interactable%20Objects/ShieldZone.cs#L55). 

## Animation and Visuals
**List your assets including their sources, and licenses.**<br/>

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**
The visuals obviously play off a space theme. 

## Input
Player1 uses  WASD to navigate and X to interact with stations. Player 2 uses IJKL to navigate and M to interact with stations.<br/>

*Command Pattern* - The players' movement scripts are encapsulated using the command pattern interface. During update of the PlayerController script, the binded key strokes would invoke the execute function in the movement script and trigger the players' gameobjects to move accordingly. Using the Command Pattern makes it easier to call the desired function without needing the information of exact class names. It also makes it so that the controller script doesn't have to fix the specific keys, and that the players of the game can select their own key bindings. [The command interface](https://github.com/glaive345/162FinalProject/blob/master/Deep%20Space%20Delivery/Assets/Scripts/Movement/IPlayerCommand.cs).

## Game Logic
**Document what game states and game data you managed and what design patterns you used to complete your task.**<br/>
*Observer Pattern* - The game is centered around a Observer / Watcher design pattern. The UI watches and receives updates from the minigames. And in turn, the minigames watch the EventManager, who keeps track of which minigames are active and their states (such as GameActive, which indicates if a player is currently playing a minigame and EventActive, which indicates if an emergency has been initiated), and generates events accordingly. 

More specifically, in EventManager, we have a list of tuples containing the name of a minigame and how long they have been active. Then, it generates a random minigame that is not active. Minigames signal when they are complete through the function ReturnFunction(stringMinigameName), which tells the manager to pop it out of the list. [This](https://github.com/glaive345/162FinalProject/blob/b9bdf6aef0ac79e108247b580c18e960e27de9d4/Deep%20Space%20Delivery/Assets/Scripts/EventManager.cs#L32) is a link to the EventManager's eventList, a list of 'tuples', which the EventManager frequency modifies. 


# Sub-Roles
## Audio
**List your assets including their sources, and licenses.** <br/>

**Describe the implementation of your audio system.**

**Document the sound style.**

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**



## Narrative Design
**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.**<br/>
The story is that these guys are lowly space delivery men. They are not paid well, and so have to do with this bumbling, disorganized ship. We try to instill this narrative throughout the game. For example, you can see that the popups in the minigame are from Windows XP, the oil barrels are behind doors, and the door can only be opened with the help of another person. We are hoping it implies that this is a poorly managed and cheap ship.



## Press Kit and Trailer
[Press Kit](https://github.com/aakim-git/PDFs/blob/master/Press%20Kit.pdf)<br/>
[Trailer](https://youtu.be/24c4bITD9aY)

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**<br/>
Our Press Kit and Trailer highlights exciting gameplay, and iconic moments, like different minigames and even starting the game and viewing the main menu. 


## Game Feel
**Document what you added to and how you tweaked your game to improve its game feel.**<br/>
For the computer virus minigame, the original idea was to mash the interaction button enough times to kill some fixed number of empty white squares in the game canvas. First changed the plain white square into windows xp style warning window to make the games seems more computer virusy. Then changed the mechanism of the minigame a bit so that instead of restarting the minigame a lot of times during the entire game play, random number of warning windows will be spawned real time as the game progresses. Everytime the player returns to the statione to play the minigame the display will be something completely different. The player might also see warning windows spawned real time as they're playing the minigame and deleting windows which might add some urgency to the player's game play.<br/>
For the laser game, the original idea was to have one target moving vertically up and down, and player hit the fire button when the target pass the the barrel. Later made the target ball shape and bouncing toward the laser gun end. Then made it so that random number of targets will be generate at random initial velocity and toward different angels. It made the minigame a lot more shootingy.
