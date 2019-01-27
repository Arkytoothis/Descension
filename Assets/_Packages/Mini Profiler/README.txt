===============================================================================
KLEBER.SWF MINI PROFILER PRO README FILE
===============================================================================

Thank you for using Mini Profiler tool. A simple tool that will help you in
your journey to create a great game. With this tool you can watch any numeric
variable and follow its value over time. Examples of the use can be: enemies
spawned, lights into the scene, enabled GameObjects, draw calls, etc.

This is made through a class called AbstractValueProvider or simply "Value
Provider". Mini Profiler already come with two Value Providers to the most
popular use of the tool: FramerateValueProvider to watch the framerate and
MemoryValueProvider to watch - you guessed it - the memory usage.

You can get more help clicking on the "?" icon on any Mini Profiler component
inside Unity Editor inspector (Unity 5.1+) or going to complete documentation
at: http://kleber-swf.com/docs/mini-profiler/

-------------------------------------------------------------------------------
PACKAGE CONTENT
-------------------------------------------------------------------------------
* Base behaviour scripts and editor
* 1 bitmap font
* 1 Example scene to view the Mini Profiler in action at no time
* 2 Prefabs containing ready to use Framerate and Memory watchers to help you
  to start right away
* 20 Color Preset Libraries to use as color schemes

-------------------------------------------------------------------------------
FEATURES
-------------------------------------------------------------------------------
* Watch any numeric variable over time in your game at runtime. This is very
helpful to debug the behavior of a variable through time. This is for replacing
the Debug.Log made each frame.

* Watch framerate and memory. As a bonus, the package already have the FPS
(framerate) and memory watcher prefabs for you. You can start to watch these
variables in no time.

* Add panels even easily with a menu. Even more useful then prefabs, right
click on the scene or go to Game Object > UI > Mini Profiler and add a ready
to use Framerate and Memory Watcher panels.

* Minimum impact. The performance impact of the panels is minimal: only 2 draw
calls and a small 256x64 texture into the memory for each panel. Of course the
final performance depends on what is being watched and how often the variable is
being read. So keep this in mind when you create your custom Value Provider.

* Works on any device. PC, OSX, Android, iOS, Web Player, WebGL and all the
platforms Unity can build.

* Customize the interval which the variable is read. Some variables need to be
watched every frame, some every second, some every minute. Mini Profiler let you
configure how often the variable.

* Collapse/expand the panel. If the panel is covering something on your game,
just collapse it to make it as small as possible (just the text is shown).

* Option to collapse/expand the panel on touch. You can touch the panel to
toggle it's collapsed state. This option is enabled by default. If you want to
disable it, you just need, on the Game Object that contains the Mini Profiler,
to remove/disable the button component, unset its "Interactable" property or
unset the "Raycast Target" property from the Image component.

* Option to collapse/expand the panel with a keyboard shortcut. You can assign
a keyboard shortcut to toggle the panel's collapsed state.

* Customize colors. You can customize the background, title, minimum, maximum
and average colors to easily differentiate between panels.

* Color Preset Libraries (Unity 5.1+). To help you to create different panels
quickly, the package already contains 4 good-to-see Color Preset Libraries
ready to use.

* Drag and drop Color Preset Libraries to easy customization (Unity 5.1+).
Instead of choosing color by color, you can drag and drop any Color Preset
Library you made (with at least 5 colors) or any that comes with the package
and BAM! all the configurable colors change immediately.

* Position, scale e resize as you want. The panel is covering some important
part of your game even when collapsed? Move it in another place. The panel is
too small or too large? Resize it or scale it as you wish.

* Collapse/expand, create/destroy, enable/disable panels programatically. You
can collapse, expand, instantiate, destroy, enable, disable, scale, resize and
position panels as you wish programatically throught the MiniProfiler classes.

* Collapse/expand, create/destroy, enable/disable panels dinamically without
making them overlap optimizing the space used. This is an option if you have
more than one panel or have them dinamically created/destroyed and want them to
nicely behave with each other. This is done by putting panels into UI Layout
Groups.

-------------------------------------------------------------------------------
WATCHING THE FRAMERATE AND MEMORY USAGE
-------------------------------------------------------------------------------
To watch the framerate (fps) and the memory usage, open the context menu on the
scene and point to "UI > Mini Profiler" to see the options. You can do it going
to "GameObject > UI > Mini Profiler" menu too having the same effect.

-------------------------------------------------------------------------------
WATCHING A CUSTOM VARIABLE
-------------------------------------------------------------------------------
You are interested in watching your custom variables? It is very simple.
	1. Create a new class (C#) extending the class AbstractValueProvider and
	implement the properties needed.
	2. Create an empty GameObject into the scene
	3. Add the "Mini Profiler" component
	4. Add the component you created on	step 1
	5. Optionaly set a title
	6. Play

-------------------------------------------------------------------------------
EXTENDING THE ABSTRACT VALUE PROVIDER
-------------------------------------------------------------------------------
Extending the AbstractValueProvider class is really simple. Here is a example
code:

	public class SimpleFramerateValueProvider : AbstractValueProvider {
		public override float Value {
			get {
				return 1f / Time.deltaTime;
			}
		}
	}

See? All the boring UI work is made for you. Alternativelly you can override
the method Refresh(float readInterval) which is called every frame.

-------------------------------------------------------------------------------
CREATING PANELS PROGRAMATICALLY
-------------------------------------------------------------------------------
You can either instantiate one of the given prefabs or you can create a new
panel like this:

	GameObject go = new GameObject("Framerate Watcher");
	go.AddComponent<FramerateValueProvider>();
	go.AddComponent<MiniProfiler>();

Note that the order is important since the MiniProfiler behaviour depends on a
value provider to work properly.

-------------------------------------------------------------------------------
AUTOMATICALLY ARRANGING MULTIPLE PROFILE PANELS
-------------------------------------------------------------------------------
If you have more than one panel or you create/destroy, collapse/expand,
enable/disable multiple panels at runtime and wants that they arrange themselves
automatically, you can use any Unity's Layout Group component.

	1. Create an empty GameObject and add the a Vertical Layout Group
	component (for example) to it.
	2. IMPORTANT: Uncheck the Child Force Expand Height to make the collapsing
	work properly.
	3. Put all your Mini Profiler panels that you want to manage as children
	of the this GameObject.

	With those actions the panels are arranged automatically when changed.

-------------------------------------------------------------------------------
ADD A KEYBOARD SHORTCUT TO TOGGLE COLLAPSED STATE
-------------------------------------------------------------------------------
This is a helpful feature when you have your mouse locked in your game for some
reasons and want to toogle the collapsed state of Mini Profiler panels.

	1. Add a Keyboard Shortcut component to the Mini Profiler panel
	2. Choose the modifiers and the main key

And that's it. Note that if you assign the same shortcut to several panels,
they will all respond to the toggle at the same time.

-------------------------------------------------------------------------------
FURTHER WORDS
-------------------------------------------------------------------------------
The intention of this extension is to watch any numeric variable you want. I
use it daily in my professional and personal projects. It was created trying be
simple, flexible, easy to use and have as less impact as possible in the game.

More information: http://kleber-swf.com/app/mini-profiler/
Complete documentation: http://kleber-swf.com/docs/mini-profiler/
Bugs and requests: https://bitbucket.org/kleber/mini-profiler/issues

THANK YOU FOR USING MINI PROFILE!
