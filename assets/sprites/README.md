# Sprites

This directory contains all sprite assets for the Endless Runner game.

## Structure

Organize sprites by category:

```
sprites/
├── characters/       # Player character sprites
├── obstacles/        # Obstacle sprites
├── collectibles/     # Coins, power-ups
├── environment/      # Background elements, platforms
├── ui/              # UI icons and elements
└── effects/         # Visual effect sprites
```

## Asset Guidelines

### File Format
- Use PNG format with transparency
- Source files (PSD, AI) should be stored separately

### Naming Convention
```
category_name_variant_state.png

Examples:
character_runner_idle_01.png
obstacle_barrier_static.png
collectible_coin_gold.png
ui_button_play_normal.png
```

### Resolution
- Design at 2x resolution (for retina displays)
- Unity will handle downscaling for lower-res devices
- Typical sprite sizes: 128x128, 256x256, 512x512

### Color Depth
- Use appropriate color depth
- Keep file sizes reasonable for mobile

## Git LFS

Large sprite files are tracked with Git LFS. Ensure Git LFS is installed:

```bash
git lfs install
git lfs track "*.png"
```

## Import Settings (Unity)

Recommended Unity import settings:
- Texture Type: Sprite (2D and UI)
- Sprite Mode: Single or Multiple
- Pixels Per Unit: 100
- Filter Mode: Bilinear
- Compression: Normal Quality
- Max Size: Appropriate for usage

## TODO

- [ ] Create character sprite sheets
- [ ] Design obstacle variations
- [ ] Create collectible sprites
- [ ] Design UI elements
- [ ] Add background elements
