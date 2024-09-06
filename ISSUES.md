# Issues

## Sub list is adding line break at end

```
* Some mods instruct the user to overwrite the game's original files.  This mod may not be compatible with those mods.
    * This mod does not affect the original game files.
* Safe to add and remove from existing saves.
```

Is Rendered as:
```
* Some mods instruct the user to overwrite the game's original files.  This mod may not be compatible with those mods.
    * This mod does not affect the original game files.
    
* Safe to add and remove from existing saves.
```

## Removed images keeping new lines

When removing an image, the pre link break and the post line break are retained,
so the translated doc will have two lines, which looks odd.

example:
```
# Shortest Trip To Earth - Preset Highlight

![thumbnail icon](media/thumbnail.png)

Makes the currently loaded preset easier to see by using a more visible highlight color.
```

Is rendered as:
```
# Shortest Trip To Earth - Preset Highlight


Makes the currently loaded preset easier to see by using a more visible highlight color.
```

## Table ends double new line
Tables are adding a new line at the end in addition to normal new line.
May be related to font being on a new line.

```
The configuration file will be created on the first game run and can be found at [font=Courier New]<Steam Directory>\steamapps\common\Shortest Trip to Earth\BepInEx\config\HighlightPreset.cfg[/font].  The changes will take effect the next time the game is run.
[font=Courier New]+----------------+------------------+--------------------------------------------------------------+
| Name           | Default          | Description                                                  |
|================+==================+==============================================================|
| HighlightColor | 00FF00FF (Green) | The highlight color for the current preset.  The Value is in |
|                |                  | RGBA format                                                  |
+----------------+------------------+--------------------------------------------------------------+
[/font]
 
[b][size=5]Support[/size][/b] 
```

## List retains new lines
Workaround:  Remove extra lines in Markdown or manually fix output.

If a md list has new lines between each ```* foo`` line, it is rendered on two lines:

This includes if *any* of the list items have a new line.  

```
* Extract the contents of the zip file into the ```<Game Dir>/BepInEx/plugins``` folder.

* Run the Game.  The mod will now be enabled.

```

**or**

```
* Extract the contents of the BepInEx zip file into the game's directory:
```<Steam Directory>\steamapps\common\Shortest Trip to Earth```
    
    __Important__:  The .zip file *must* be ...
* Run the game.  Once the main menu is shown, exit the game.  
```

```
[b]Important[/b]:  The .zip file [i]must[/i] be extracted to the root folder of the game.  If BepInEx was extracted correctly, the following directory will exist: [font=Courier New]<Steam Directory>\steamapps\common\Shortest Trip to Earth\BepInEx[/font].  This is a common install issue.
[*]
Run the game.  Once the main menu is shown, exit the game.
[*]
If the install was successful, there will now be a [font=Courier New]<Game Dir>/BepInEx/plugins[/font] directory.
```