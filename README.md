# Futuristic 3D Earth Interface – Unity Technical Challenge

A Unity project showcasing a glowing, interactive 3D globe with sci-fi shader effects, dynamic Points of Interest (POIs), and a sleek UI — all fully data-driven and modular.

---

## Summary

This project was developed for the "Futuristic 3D Earth Interface" coding test. It demonstrates:

- A glowing 3D Earth with a tech-style shader overlay.
- Interactive POIs that open animated info panels with data visualizations.
- Smooth camera controls and transitions.
- Data-driven architecture using external JSON.
- Built in **Unity 6000.0.40 LTS (URP)**.

---

## How to Run

1. **Unity Version**: Open in Unity 6 (URP project).
2. **Scene**: Open `Globe_UI.unity`.
3. **Controls**:
   - **Left Mouse**: Orbit the globe
   - **Scroll**: Zoom in/out
   - **Click POI**: Focus and open info panel
   - **Close Button**: Hide the info panel
4. **Customizing POIs**:  
   Edit `poi_data.json` in `/StreamingAssets` to change POI locations, categories, colors, or chart values.

---

## Project Structure

```
Art/            # 3D models, textures, shaders, skybox
Audio/          # (Placeholder) Audio files
Prefabs/        # Prefab objects for globe & POIs
Scenes/         # Main scene (Globe_UI.unity)
Scripts/        # Organized scripts:
  └─ Camera/    # Camera control logic
  └─ Light/     # Day/night cycle
  └─ POI/       # POI data & interaction logic
  └─ UI/        # Info panels & data chart UIs
StreamingAssets/ # External JSON POI data
TextMesh Pro/   # Fonts for sci-fi UI
```

---

## What's Complete

- **3D Futuristic Globe**
  - Emissive Earth shader (Shader Graph)
  - Transparent hex/tech grid (shader + texture)
- **Camera System**
  - Orbit, zoom, tilt lock
  - Smooth zoom-to-POI
- **POI System**
  - Data from external JSON
  - POI categories, colors, pulsing glow
  - Animated info panel with bar charts
- **Zoom Logic**
  - POIs shown/hidden based on zoom level
  - Smooth scale transitions
- **Visual/UX**
  - Sci-fi glowing UI
  - Animated POI transitions
- **Bonus**
  - Day/night directional light rotation
  - Starfield skybox
  - Background audio w/ teleport SFX

---

## What's Incomplete

- **Holographic Beams & Rings** – Planned, not implemented
- **High-tech Particle Effects** – Not yet added

---

## Core Features

### 1. 3D Futuristic Globe
- Glowing emissive shader
- Shader-based hex grid overlay

### 2. Camera System
- Orbit/zoom/tilt controls
- Smooth POI focus on click

### 3. POI System
- JSON-driven data (easy expansion)
- Clickable, glowing POIs with panel + chart

### 4. Zoom Logic
- POIs filter by zoom level (continent > country > city)

---

## Visual & UX

- Sci-fi glowing UI panels & markers
- Bar chart UI driven by JSON
- Animated POI reveals

---

## Data Handling

All POI and chart data is stored in `poi_data.json` (in `/StreamingAssets`).

---

## Thanks

Thank you for reviewing my submission!  
I'm happy to walk through the code or design decisions in an interview.
