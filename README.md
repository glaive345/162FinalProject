# 162FinalProject
## General Notes/Problems ##
Created on Unity 2019.1.3f1<br/>

Each minigame should hold: <br/>
-Whether it is currently active (bool) <br/>
-Whether it can be interacted with (bool)<br/>

Parameters required from each minigame: <br/>
-Name of minigame + start/ongoing(only for some)/end (string) <br/>
-Success/Failure (bool)<br/>
-Degree of success (float 0 to 1)<br/>
-End state of station (string)<br/>

## Visuals/UI (Derek) ##
-Player asset done<br/>
-basic ship outline<br/>
-basic locations marked<br/>
-numbers on screen for UI<br/>

### Documentation ###
-Found most assets on Unity Asset Store.<br/>
-Designed basic ship layout and shape to be longer than wide to fit well in a locked camera at regular widescreen resolutions. Segmented ship into three parts to add to ship complexity with doors.<br/>
-Made walls transparent to help with visibility of the various things within the ship.
-Interactable points of the ship added and decorated with objects. Interactable points mainly added to perimeter of ship to ensure that they are all fairly spaced apart from each other, increasing travel distance between them.<br/>
-Added various particle effects and lights to different parts of the ship.<br/>
-Changed out basic background image to incorporate a skybox for better depiction of background.<br/>
-Implemented animator for idle, walking, and running animations on the two player characters.<br/>
-Started designing basic UI setup.<br/>
-Implemented door animations when stepping on pressure plate. Jumps to specific frame if exiting or getting on while in previous animation.<br/>


## Game Logic (Jason) ##
-Classes<br/>
	-PlayerController(movement of the player)<br/>
	-GameSystemManager:<br/>
		-Time algorithms to publish to different RepairObjects - set the status to Pending<br/>
		-Timer<br/>
		-FailureCounter<br/>
		-Collision<br/>
	-RepairObjects:<br/>
		-Status: Inactive, Pending, Broken<br/>
		-Timer: while Pending, to see if fixed or broken<br/>
		-Publishes to GameSystemManager fixed or broken<br/>
		-if RepairObject.status == Pending && RepairObject.isTriggered activate Minigames<br/>
		-isTriggered if the repairObjectTile collides with the player<br/>
	-Minigames:<br/>
		-Status: Inactive, Active<br/>
		-Completion Status: Perfect, pass, critical, fail<br/>
		-Publishes return status to repair object<br/>
		-Timer to decide completion status<br/>
-Structuring of code<br/>
-Event generation<br/>

### Documentation ###
-WRITE DOCUMENTATION HERE
## Movement (Aaron) ##
-Movement in four directions<br/>
-Movement restrictions<br/>
-Setting fixed camera<br/>
-make a minigame (trgeter minigame)<br/>

### Documentation ###
-WRITE DOCUMENTATION HERE
## Input (Yang) ##
-Binding keys to command pattern (for 2 players)<br/>
-Creating first basic minigame (virus minigame)<br/>

### Documentation ###
-WRITE DOCUMENTATION HERE


## Audio ##
-WRITE DOCUMENTATION HERE
## Gameplay Testing ##
-WRITE DOCUMENTATION HERE
## Narrative Design ##
-WRITE DOCUMENTATION HERE
## Press Kit and Trailer ##
-WRITE DOCUMENTATION HERE
## Game Feel ##
-WRITE DOCUMENTATION HERE
