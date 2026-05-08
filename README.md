# Sci-Fi Shooter Game – Unity Project

A sci-fi third-person shooter game built in Unity as a university media interaction project.

The project focuses on connecting player actions to media responses such as animation, sound, UI feedback, video, and visual effects.

## Project Overview

This game takes place inside a sci-fi space station. The player progresses through different stages by destroying targets, defeating enemies, surviving moving enemies, and fighting a final boss.

The game includes a skippable intro video, player movement, aiming, shooting, enemy AI, health systems, healing pickups, level progression, sound effects, animations, and UI feedback.

## Game Progression

Level 1: Destroy 5 targets  
Level 2: Defeat 3 stationary enemies  
Level 3: Defeat 3 moving enemies  
Boss Fight: Defeat the final boss

## Features

- Third-person player movement
- Camera control using Cinemachine
- Aiming and scope system
- Crosshair aiming
- Shooting with bullets and raycasting
- Muzzle flash and hit effects
- Gun sound effects
- Player health bar
- Enemy health bars
- Damage sound feedback
- Healing pickups
- Level progress UI
- Stationary enemies
- Moving enemies using NavMesh
- Boss fight
- Skippable intro video
- Sci-fi environment and 3D models
- Player and enemy animations

## Media Used

This project uses multiple media types:

- 3D models and sci-fi environment assets
- UI elements such as health bars, level text, and progress counters
- Sound effects for shooting, damage, healing, and actions
- Animations for player movement, shooting, rolling, enemies, and boss
- Video intro using Unity Video Player
- Visual effects such as muzzle flash and hit effects

## Interaction Design

### Player Movement

The player can move through the sci-fi environment using keyboard input. Movement is connected to character animation and camera movement.

### Aiming and Shooting

The player can aim using right click. The camera zooms in, a dark scope overlay appears, and the crosshair remains visible for accurate aiming.

Shooting uses bullets, raycasting, gun sounds, muzzle flash, and hit detection.

### Health and Damage

When enemies shoot the player, the player health bar decreases and a damage sound plays.

Enemies also have red health bars that decrease when they take damage.

### Healing Pickups

Healing items are placed around the map. When the player touches a healing pickup, the player gains health and the pickup disappears.

### Level Progression

The game tracks progress using a UI counter. After each objective is completed, the next stage begins automatically.

Targets destroyed -> Level 2 starts  
Stationary enemies defeated -> Level 3 starts  
Moving enemies defeated -> Boss fight starts  
Boss defeated -> Game completed

### Boss Fight

After Level 3, the boss fight begins. The UI changes to "BOSS FIGHT" and the player must defeat the final mech boss.

## Technologies Used

- Unity 6
- C#
- Cinemachine
- Unity NavMesh
- TextMeshPro
- Unity UI
- Unity Video Player
- Unity Animator
- Unity AudioSource

## Screenshots

### Level 1 Targets
![Level 1 Targets](screenshots/01-level-1-targets.png)

### Gameplay Overview
![Gameplay Overview](screenshots/02-gameplay-overview.png)

### Aiming Scope
![Aiming Scope](screenshots/03-aiming-scope.png)

### Level 1 Progress
![Level 1 Progress](screenshots/04-level-1-progress.png)

### Level 2 Stationary Enemies
![Level 2 Stationary Enemies](screenshots/05-level-2-stationary-enemies.png)

### Level 3 Moving Enemies
![Level 3 Moving Enemies](screenshots/06-level-3-moving-enemies.png)

### Boss Fight
![Boss Fight](screenshots/07-boss-fight.png)

### Boss Enemy Visible
![Boss Enemy Visible](screenshots/08-boss-enemy-visible.png)

### Map Overview
![Map Overview](screenshots/09-map-overview.png)

## Screenshot Captions

| Screenshot | Description |
|---|---|
| 01-level-1-targets.png | Level 1 target tutorial with progress counter. |
| 02-gameplay-overview.png | Third-person sci-fi gameplay environment with targets visible in Level 1. |
| 03-aiming-scope.png | Aiming mode with zoom, dark scope overlay, and crosshair for accurate shooting. |
| 04-level-1-progress.png | Level 1 progress counter updating after destroying targets. |
| 05-level-2-stationary-enemies.png | Level 2 showing stationary enemies activated after completing the target tutorial. |
| 06-level-3-moving-enemies.png | Level 3 showing moving enemies with health bars and combat interaction. |
| 07-boss-fight.png | Boss fight stage with boss objective counter and healing pickup nearby. |
| 08-boss-enemy-visible.png | Boss fight stage showing the final mech boss inside the boss room. |
| 09-map-overview.png | Full sci-fi map overview showing the connected level areas and environment layout. |

## Gameplay Video

Watch the gameplay demo here:

[Gameplay Video](https://drive.google.com/file/d/1meTVTtq_VRZ_7kDgvs2TjoKT_vurUQZe/view?usp=drive_link)

## My Contribution

I worked on the gameplay programming and interaction systems, including:

- Player movement
- Player shooting
- Aiming and scope system
- Player health system
- Enemy shooting
- Enemy health bars
- Level progression
- Moving enemies using NavMesh
- Boss fight logic
- Healing pickups
- UI progress system
- Sound and animation integration
- Skippable intro video

## How to Play

W / A / S / D = Move  
Mouse = Look around  
Left Click = Shoot  
Right Click = Aim / Scope  
R = Reload  
Q = Roll  
Space / Esc = Skip intro video

## Repository Note

Due to GitHub web upload limits, this repository includes the main project scripts, selected assets, screenshots, and documentation.

Generated Unity folders such as `Library`, `Temp`, `Obj`, `Logs`, and build folders were intentionally excluded because they can be recreated by Unity.

## Project Type

University project for Introduction to Media Informatics.
