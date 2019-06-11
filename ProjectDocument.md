# Game Basic Information #

## Summary ##

**A paragraph-length pitch for your game.**

## Gameplay explanation ##

**In this section, explain how the game should be played. Treat this like a manual within a game. It is encouraged to explain the button mappings and the most optimal gameplay strategy.**




# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content.

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based off the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your own relevant information. Liberally use the template when necessary and appropriate.

## User Interface

**Describe your user interface and how it relates to gameplay. This can be done via the template.**

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your own movement scripts that do not use the phyics system?**

## Animation and Visuals

**List your assets including their sources, and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Input

**Describe the default input configuration.**
Player1 uses  WASD to navigate and X to interact with stations. Player 2 uses IJKL to navigate and M to interact with stations.

*Command Pattern* - The players' movement scripts are encapsulated using the command pattern interface. During update of the PlayerController script, the binded key strokes would invoke the execute function in the movement script and trigger the players' gameobjects to move accordingly. Using the Command Pattern makes it easier to call the desired function without needing the information of exact class names. It also makes it so that the controller script doesn't have to fix the specific keys, and that the players of the game can select their own key bindings. [The command interface](https://github.com/glaive345/162FinalProject/blob/master/Deep%20Space%20Delivery/Assets/Scripts/Movement/IPlayerCommand.cs).



## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**

# Sub-Roles

## Audio

**List your assets including their sources, and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.**

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design

**oDocument how the narrative is present in the game via assets, gameplay systems, and gameplay.**

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**



## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**
For the computer virus minigame, the original idea was to mash the interaction button enough times to kill some fixed number of empty white squares in the game canvas. First changed the plain white square into windows xp style warning window to make the games seems more computer virusy. Then changed the mechanism of the minigame a bit so that instead of restarting the minigame a lot of times during the entire game play, random number of warning windows will be spawned real time as the game progresses. Everytime the player returns to the statione to play the minigame the display will be something completely different. The player might also see warning windows spawned real time as they're playing the minigame and deleting windows which might add some urgency to the player's game play.
For the laser game, the original idea was to have one target moving vertically up and down, and player hit the fire button when the target pass the the barrel. Later made the target ball shape and bouncing toward the laser gun end. Then made it so that random number of targets will be generate at random initial velocity and toward different angels. It made the minigame a lot more shootingy.
