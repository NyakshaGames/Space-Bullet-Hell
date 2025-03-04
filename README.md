**Space Bullet Hell**

Space Bullet Hell is a fast-paced, automatic-firing bullet hell game built in Unity. Guide your lone astronaut through relentless waves of enemy ships and projectiles in deep space. With a wide arsenal of fully integrated weapons and challenging enemy encounters, this game delivers a complete and polished arcade experience.

Table of Contents
Features
Game Overview
Installation
Gameplay
Technical Details
Credits
License
Features
Automatic Firing:
Your weapon fires automatically with distinct, finely tuned cooldowns for each type.

Diverse Weapon Arsenal:
Switch seamlessly between eight weapon types, including:

Laser: A fast, straight-firing projectile.
Spread Shot: Fires a cone of bullets to cover a wide area.
Homing Missile: Seeks out and destroys enemy targets.
Charged Beam: A powerful beam that damages enemies over time.
Ricochet Bullet: Bullets that bounce off surfaces to hit hidden foes.
Multi-Directional Burst: Fires projectiles radially in every direction.
Cluster Bomb: Splits into multiple projectiles on impact.
Auxiliary Turret: Deploys a turret that attaches to your ship and fires independently.
Robust Enemy & Damage System:
Enemies come in varied waves with intelligent behavior, and each hit registers damage accurately, creating a challenging bullet hell experience.

Dynamic Visual Effects:
Advanced particle systems, LineRenderers, and custom shaders deliver explosive, immersive effects for each weapon, including the charged beam.

Seamless Gameplay:
A single control scheme lets you focus on movement and dodging, while weapon switching and automatic firing keep the action relentless and engaging.

Cross-Platform Polish:
Developed with a clean codebase and optimized performance for both PC and web platforms.

Game Overview
In Space Bullet Hell, you play as an isolated astronaut piloting a damaged ship deep in space. As enemy waves intensify, you must survive by switching between a comprehensive array of weapons and expertly dodging the relentless barrage of enemy fire. Every weapon is meticulously balanced, providing unique firing patterns and effects to keep the gameplay fresh and challenging from start to finish.

Installation
Prerequisites
Unity (6000 LTS or later)
Steps
Download the Project:
Download the project archive from the provided link or copy the repository from your private source.

Open in Unity:
Open the project using Unity Hub and select your desired Unity version.

Setup the Scene:
Open the main scene (e.g., Main.unity).
Ensure the Player GameObject has the PlayerController and PlayerRotation scripts attached.
Assign the weapon prefabs (Laser, Spread Shot, Homing Missile, Charged Beam, etc.) to the respective fields in the Inspector.

Build and Run:
Configure your target platform in Build Settings and click Build and Run to play the game.

Gameplay
Movement:
Use WASD or arrow keys to navigate your ship through space.

Weapon Switching:
Press keys 1 through 8 to instantly change between the various weapons in your arsenal.
Each weapon has its own automatic firing and distinct visual effects.

Objective:
Survive against increasingly challenging waves of enemy ships and projectiles. Master weapon switching and dodging to achieve the highest score possible.

Technical Details
PlayerController:
Manages movement, weapon switching, and automatic firing. Each weapon type has a dedicated shooting method and unique cooldown timer for balanced gameplay.

Weapon Prefabs:
Each weapon (Laser, Spread Shot, Homing Missile, etc.) is implemented as a prefab with its own projectile behavior.
The Homing Missile includes target detection and smooth tracking of enemy ships.
The Charged Beam effect is achieved using both Particle Systems and LineRenderers for a dynamic, continuous beam effect.

Enemy & Damage System:
Enemies are designed with integrated health and damage feedback. Every hit from your weapons registers correctly to provide a satisfying arcade experience.

Optimized & Modular Code:
The codebase is organized for ease of modification, with clearly separated systems for movement, shooting, and weapon management.

Credits
Space Bullet Hell was developed as a complete, polished arcade experience by Nyaksha Games. All assets, codes, and designs are proprietary.

License
This game is a complete, finished product. All rights reserved © 2025 Nyaksha Games.
