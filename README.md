# CS4455GameDev
CS4455 Game Development Project

The Good Smith's War

Instructions:
Run "The Good Smith_s War.exe" located under the Assests/Build folder

Scenes to open in Unity:
l0 - this is the first scene to open
intro
l1
l1.5
l2
l3
l4_copy
l5final
Outro Scene
menuscene

Keys:
Movement: WASD or arrow keys
Sheathe/unsheathe weapon: K
Attack: J
Jump Attack: J while pressing W
Activate: Space

Genre: Dungeon Crawler RPG
Premise: You are a simple blacksmith of a small village. Your village has recently been invaded by goblins who have been demanding money from you and the other villagers. Anyone who refuses to pay is kidnaped and locked in the goblin Queen’s dungeon.
You spoke out against this unfair taxation and have been arrested. The game begins as you wake up in a small jail cell in the Goblin Queen’s dungeon. You trick one of the guards to release you and recover your trusted axe. Now, your war to dethrone the goblin Queen continues. Fight guard minions, solve puzzels, collect the coins to purchase buffs that will help you, collect the key to open the next level and defeat the goblin Queen.


Rubric Requirements & Walk through:

Game Feel: The game is a third person dungeon crawler. There are clear objectives provided with the story line. The player shoudl defeat enemies to get keys and advance through the levels so that they can defeat the Goblin Queen. The player is prompted to restart a level when they die(when their health runs out). They are also informed pf death through a red screen and plyer ragdoll. Observe this by dying in any non-vendor level. There is a start menu. See this by completing th tutorial on game start, it will bring you to the main menu/title screen.

Precursors to Fun: The game sets up goals for the character. In order to defeat the Queen they must make it to her by defeating guards on 3 other levels. Defeating guards gives you a key that allows you to advance. Some guards can only be reached by exploring and opening doors. This can be seen best on levels 2 and 4. Level two begins after the you leave the fist vendor room. The player faces choices in interacting with vendors and buying buffs. They collect gold over the course of the game and have to choose what they spend their money on as well as decided which buffs are most useful to them later in the game when they can afford more buffs but only carry two. See this in the vendor interaction levels. The first vendor interaction is immeadiaty after the first scene in which you can fight enemies. The game getsharder as you progress. See this by playing through the game. There is a tutorial level that helps teach fighting actions. It is the first thing to load even before the start menu. The first level with enemies is also extremely easy to give players a better chance to get used to controls. If a player dies on a level they have to restart the level keeping non of their progress. If they fail, it is implied that they will e in jail forever and the town will not be saved.

Character: The main character of the game is a character mdel with animation adn input control that we created. The use root motion and their animation blend very well. The player has direct control of the character at most times unless they begin a combo attack or other longer animation. You can play this character and test the intuitiveness of the controls anywhere in the game. The character ragdolls when they die and has audible footsteps that match their actual steps. They also have an audio for unsheathing and sheathing their weapon. The camera is mostly smooth. The first level with enemies is too small compared to the other levels to work well ith the camera follow confirguration. Test the camera on the second level with enemies after the first vendor interaction, or in the vendor scenes.

Physics and spatial Sym: The environments are created with a base kit and embellished with interactable objects and designed for our particular game play style. There are interactable doors and pressure plates on the second and third levels with enemies. These levels also have crates that react to the environment and character physically. Coins spin in the space to attract attention and most interactable objects are animated and audible. Open the doors betwen levels, loot chests, stand on pressure plates, put crates on pressure plates, and listen fo rth key pick up sound. The environment is interactive and generates alerts to the player. The spatial simulation is consistent with speed and appearance regardless of situation.

NPC AI: The enemy goblins in the game have an AI that we implemented. We used only th emodels and animations from the game kit. The scripts, blend trees, ect. are ours. The enemies go through certain states. In the seocnd level with enemies you can see some enemies start by patroling the space before they attack while other sit idle. When they "see" the player they react by jumping up and attacking. They attack head on and do not hide. They chase the player if they run away. If the player gets outside of a certain range the enemies stop pursuing and return to their original state. They have smooth steering and fluid animation. 


Polish: There is a cute in game start menu and the ability to  quit the game at any time. You can opena pause menu with esc and then quit the game by pressing exit. There is a loadig screen between each level to help with transitions and the tutorial level has a cool fade out of the world effect. The audio effects in the game cover every situation without being overwhelming. The player has multiple randomized attack sounds that play to keep the audio from being annoying. The crate when pushed plays a looped audio. There are effects that surround the player to show what buffs they have on them at any point. See this by buying a buff in any vendor level. There are still many cool and useful environment animations. The lighting in the levels is meant to match the asthetic. Especially look for the lighting deisgn in the main menu, the tutorial level, and the boss level. Also make a not of the consistent style through out the game and artwork that displays when you complete the game. The edge of the workd makes sense and the player can't leave the world. There are some minor glitches.



Known Bugs:



Citation
--------
AcquireChan - https://assetstore.unity.com/packages/3d/characters/acquire-chan-3d-model-110031
3D Character Model used

SapphiArt - https://assetstore.unity.com/packages/3d/characters/amane-kisora-chan-free-ver-70581
3D Character Model used

Unity-chan - https://assetstore.unity.com/packages/3d/characters/unity-chan-model-18705
3D Character Model used

Treasure Set - Free Chest - https://assetstore.unity.com/packages/3d/props/interior/treasure-set-free-chest-72345
Models and materials were used.

Coin Bag Clip Art - http://clipart-library.com/clipart/1114489.htm
2D Sprite

Mixamo-Melee Axe Pack - https://assetstore.unity.com/packages/3d/animations/melee-axe-pack-35320
We used the humanoid animaitons from this pack

Barbarian Warrior - https://assetstore.unity.com/packages/3d/characters/humanoids/barbarian-warrior-75519
We used this 3d character model for our main play character

3D Game Kit - Character Pack - https://assetstore.unity.com/packages/3d/3d-game-kit-character-pack-135217
We only used the chomper model and chomper animation from this pack. The animator and controller are ourselves.

Aura - Particle System - https://assetstore.unity.com/packages/tools/particles-effects/aura-volumetric-lighting-111664
We only use its pictures as our material mesh

Cartoon Building Temple Kit - Environment - https://assetstore.unity.com/packages/3d/environments/dungeons/cartoon-temple-building-kit-lite-110397
We use this kit to build our whole environment

Zelda: breath of the wild OST - We use as our OP background music

Castle Supply LITE - https://assetstore.unity.com/packages/3d/environments/fantasy/castle-supply-lite-23699 - Used blacksmith models from this kit

High Quality Realistic Wood Textures - Mega Pack https://assetstore.unity.com/packages/2d/textures-materials/wood/high-quality-realistic-wood-textures-mega-pack-75831 - Used Materials from this kit

Ben3d Crate - https://assetstore.unity.com/packages/3d/props/industrial/ben3d-crate-7548 - Used models form this kit

