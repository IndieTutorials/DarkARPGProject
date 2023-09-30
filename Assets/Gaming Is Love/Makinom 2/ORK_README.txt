
ORK Framework 3
http://orkframework.com


-------------------------------------------------------------------------------------------------------------------------------------------------------
Content
-------------------------------------------------------------------------------------------------------------------------------------------------------

- Licence and Version Information
- General Information
- Dependencies
- Documentation and Tutorials
- Scripting and Extensions
- Support
- Folder Description
- iOS Hints



-------------------------------------------------------------------------------------------------------------------------------------------------------
Licence and Version Information
-------------------------------------------------------------------------------------------------------------------------------------------------------

There are 2 versions of ORK Framework available - the 'Limited' and 'Full' version.
ORK Framework 3 includes Makinom 2.

Limited:
- Free and available to everyone.
- Limited in terms of creating game data (e.g. you can only create a certain amount of combatants).
- The limited version displays a watermark at one of the corners of the screen.
- No source code access.
- Doesn't allow any distribution (i.e. releasing or publishing) of your project.
- Includes Makinom 2 Free (no source code).

Full:
- No limitations.
- No watermark.
- You can sell your projects (or distribute them for free).
- Access to the full source code.
- Includes Makinom 2 Pro (full source code)

Please see the details in the version comparison found here: http://orkframework.com/download/


There are 2 versions of Makinom available - the 'Free' and 'Pro' version.
Both offer the same, unlimited access to Makinom's features, but only the 'Pro' version contains the full C# source code.

Please see the details in the version comparison found here: http://makinom.com/download/



-------------------------------------------------------------------------------------------------------------------------------------------------------
General Information
-------------------------------------------------------------------------------------------------------------------------------------------------------

ORK Framework 3 is an extension for Makinom 2 - as such, you'll access ORK's editor via the Makinom editor.
Makinom 2 Pro is included in ORK Framework 3.

- Opening ORK/Makinom editor.
You can open the Makinom editor to view or change settings and create/edit schematics using the Unity menu: 'Window > Gaming Is Love > Makinom'
Alternative (both open the same editor): 'Window > Gaming Is Love > ORK Framework'

- Makinom Project asset (also contains ORK data)
When you first open the Makinom editor, a new Makinom Project asset will be created.
This asset will contain all general ORK/Makinom settings made in the editor.
It's located in: 'Assets/Gaming Is Love/_Data/'
This folder will also contain additional sub-folders with individual data assets when saving the project.
It's recommended to leave the project/data assets at this location to make use of Makinom's backup functionality.

- Schematic assets
When you create schematics, each schematic is stored in a separate asset file.
You can save the schematic assets anywhere in your Unity project.
To keep things organized, it's recommended to create a folder structure mirroring the purpose of the schematics or scenes they're used in.

- DLL information
ORK Framework and Makinom are included in your Unity project using DLLs.
This will decrease your project's compile time if you change any of your own scripts.
The full C# source code is only included in the 'Full' version of ORK Framework (for both ORK and Makinom).



-------------------------------------------------------------------------------------------------------------------------------------------------------
Dependencies
-------------------------------------------------------------------------------------------------------------------------------------------------------

The optional 'Unity UI' UI module requires using the following packages:
- Unity UI
- TextMesh Pro

ORK Framework 3 also requires Makinom 2, a compatible version is automatically included in the package.



-------------------------------------------------------------------------------------------------------------------------------------------------------
Documentation and Tutorials
-------------------------------------------------------------------------------------------------------------------------------------------------------

You can find the documentation and different types of tutorials here:
ORK Framework: http://orkframework.com/guide/

As a first read, I'd recommend to check out the introduction, first steps and game starters documentations:
http://orkframework.com/guide/documentation/getting-started/introduction/
http://orkframework.com/guide/documentation/getting-started/first-steps/
http://orkframework.com/guide/documentation/getting-started/game-starters/

To learn how to set up a status system, follow this tutorial series:
http://orkframework.com/guide/tutorials/status-system-setup/start-status-system-setup/

To learn how to build a game based on a status system, follow this tutorial series:
http://orkframework.com/guide/tutorials/3d-rpg-playground/start-3d-rpg-playground/

If you're seeking information/help with a specific feature, take a look at the documentation:
http://orkframework.com/guide/documentation/

If you have any tutorial requests or need help, don't hesitate and contact me:
contact@orkframework.com

Please note that ORK Framework 2 projects are not compatible with ORK Framework 3.
A project conversion tool will be available at a later time.

The ORK/Makinom editor includes a built-in documentation.
All settings are explained in the help window at the lower left corner of the editor.

Information and help texts will be displayed if you hover with the mouse over a setting or foldout.
You can change this behaviour in the editor settings (Editor > Editor Settings).

There currently is no separate documentation of the individual settings available, other than the documentation and tutorials mentioned above.



-------------------------------------------------------------------------------------------------------------------------------------------------------
Scripting and Extensions
-------------------------------------------------------------------------------------------------------------------------------------------------------

You can extend ORK Framework's and Makinom's functionality using plugins, custom nodes and code extensions (via custom scripts).

Learn more about scripting with Makinom here:
http://makinom.com/guide/documentation/scripting/scripting-overview/

Check out available Makinom plugins, custom nodes and other scripts here:
http://makinom.com/guide/extensions/

You can also use the Makinom Extension Manager in Unity to download and import available extensions into your project.
Open the extension manager using the Unity menu: 'Window > Gaming Is Love > Makinom Extension Manager'



-------------------------------------------------------------------------------------------------------------------------------------------------------
Support
-------------------------------------------------------------------------------------------------------------------------------------------------------

Need help or found a bug?

Please search for a solution or report the bug in the 'Support & Community' forum:
http://forum.orkframework.com/

Didn't find anything or don't want to post in the forum? Contact me:
contact@orkframework.com



-------------------------------------------------------------------------------------------------------------------------------------------------------
Folder Description
-------------------------------------------------------------------------------------------------------------------------------------------------------

Upon import you'll get the following folders:

- 'Assets/Gaming Is Love/Makinom 2/'
This folder and sub-folders contains ORK Framework 3, Makinom 2 and UI modules ('DLL' folder).
You'll also find the source code (VS projects), zipped-up in the 'ORK_Source_Code.zip' and 'Makinom_Source_Code.zip' files.

- 'Assets/Gizmos/GamingIsLove/'
This folder and sub-folders contain the gizmo icons for ORK Framework and Makinom components and assets (in 128x128 pixels).

After opening and saving in the Makinom editor you'll get additional folders:

- 'Assets/Gaming Is Love/_Data/'
This folder and sub folders contain your Makinom Project and data assets.
It's recommended to keep them at this place.

- 'Assets/Gaming Is Love/_Backups/'
This folder contains backups created when saving changes in the Makinom editor.
You can restore backups via the Makinom editor in: 'Editor > Backups'
Backups will bundle all assets in the Makinom data folder as a unitypackage-file.

- 'Assets/Gaming Is Love/_Downloads/'
Packages you've downloaded via the extension manager will be stored in this folder.



-------------------------------------------------------------------------------------------------------------------------------------------------------
iOS Hints
-------------------------------------------------------------------------------------------------------------------------------------------------------

If you're building your project for iOS and run into issues, check out the Unity trouble shooting page:
http://docs.unity3d.com/Manual/TroubleShootingIPhone.html


