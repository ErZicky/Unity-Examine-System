# Unity-Examine-System
A simple First Person Item Examine system for unity 

here's how it looks (it's more smooth than it looks):

![](https://github.com/ErZicky/Unity-Examine-System/blob/main/images./GIF.png)

It has a few customizable fields that I found useful, here's how the script inspector looks:

![](https://github.com/ErZicky/Unity-Examine-System/blob/main/images./Editor.PNG)


## Controls:
Left mouse to pick up and examine an object with the right tag.
Right mouse to exit examine mode and put back the object.

## Note:
You need to give the object that you want to be examinable a tag and specify it in the script inspector.

You need a FPS controller (which is not provided) and specify it in the code (see comments)

You need an "Inspect Area" object which has to be a child of your camera, here's my structure:

![](https://github.com/ErZicky/Unity-Examine-System/blob/main/images./Hierarchy.PNG)

The crosshair part it's just a canvas with the image of a white circle to mark the center of the screen, it's not needed but it's useful to "aim" the object.
