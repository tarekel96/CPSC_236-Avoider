# CPSC_236-Avoider

## Student Info
- Student ID: 1902662
- Student Name: Tarek El-Hajjaoui
- Course Number: CPSC 236 - 03
- Assignment: Avoider

## Description of Program:
- A C#/Unity 2D Top-Down POV Game. The game starts with the Main Menu with 2 buttons - Play and Quit.
- If press Play, User is re-directed to the game scene.
- The game is 2D and Top-Down POV
- The player is to get the golden coin at the top right of the tilemap
- The player has a dash ability that is trigged by a double click. The player is 1.5x faster for 1.5 seconds.
- There are enemies that are randomly spawned in the map
- The enemies will not chase the player until they are within a certain radius distance from the player
- The enemies have projectiles that they can throw at the player
- If the enemies or projectiles touches the player, the player dies and the game restarts
- If player reaches the gold coin, the player wins and the game restarts
- There is a Quit UI button that follows the player's camera and is positioned at the top right 

## Instructions to run the program:
- Clone the repo
- Open the repo with Unity Hub / Unity
- *IMPORTANT*: Open the Sample Scene (otherwise it will look like the project is empty and has no game objects)
- Press Play on Unity and interact with the program

## Source Files:
- Scripts:
  - Menu.cs
  - Game.cs	
  - Player.cs
  - CameraScript.cs
  - ButtonQuitIngame.cs	
  - EnemyManager.cs
  - EnemyController.cs
  - BulletEnemyController.cs		
  - Coin.cs					

## Sources referred to:
- Tiny RPG Forest Tilemap: https://assetstore.unity.com/packages/2d/characters/tiny-rpg-forest-114685
- Main menu background image: https://www.pinterest.com/pin/191473421646268760/
- Brackey's Top Down Movement in Unity: https://www.youtube.com/watch?v=whzomFgjT50&t=425s&ab_channel=Brackeys
- 2D mouse point click movement system: https://forum.unity.com/threads/2d-mouse-point-click-movement-system-quick-tutorial.217886/
- Detect a Double Click: https://www.youtube.com/watch?v=GR0XJX1phiw&ab_channel=AlexanderZotov
- Main menu Unity: https://www.youtube.com/watch?v=-GWjA6dixV4&ab_channel=BMo
- Async Load Scene Unity: https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html
- Professor Christopher Byod's Unity/C# Videos
