# 162FinalProject
## General Notes/Problems ##
Created on Unity 2019.1.3f1
## Visuals/UI (Derek) ##
-Player asset done<br/>
-basic ship outline<br/>
-basic locations marked<br/>
-numbers on screen for UI<br/>

### Documentation ###
-WRITE DOCUMENTATION HERE
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
