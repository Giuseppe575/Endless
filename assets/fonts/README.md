# Fonts

This directory contains all font assets for the Endless Runner game.

## Structure

```
fonts/
├── ui/              # UI fonts
├── gameplay/        # In-game text fonts
└── special/         # Special purpose fonts
```

## Asset Guidelines

### File Formats

- **TrueType Font (TTF)**: Widely supported, recommended
- **OpenType Font (OTF)**: Also supported, good for advanced features
- TextMeshPro compatible formats

### Naming Convention

```
fontname_variant.ext

Examples:
robotomono_regular.ttf
robotomono_bold.ttf
gameplay_score.ttf
```

## Font Requirements

### UI Font
- **Purpose**: Menus, buttons, settings
- **Requirements**:
  - High readability at small sizes
  - Multiple weights (Regular, Bold)
  - Complete character set
  - Numbers clearly distinguishable

### Gameplay Font
- **Purpose**: Score, distance, multipliers
- **Requirements**:
  - Extremely readable at glance
  - Clear numbers
  - Bold/thick for visibility
  - Works well with outline/shadow

### Recommendations
- Avoid overly decorative fonts for critical info
- Test readability on small mobile screens
- Ensure proper licensing for commercial use

## TextMeshPro Setup

Unity's TextMeshPro is recommended for better text rendering:

1. Import font into Unity
2. Window > TextMeshPro > Font Asset Creator
3. Configure settings:
   - Source Font: Your .ttf file
   - Sampling: Auto
   - Atlas Resolution: 512x512 or 1024x1024
   - Character Set: ASCII or custom
4. Generate Font Atlas
5. Use TMP_Text components instead of Text

## Licensing

Ensure fonts have appropriate licenses:
- **Free for commercial use**: ✓
- **Attribution required**: Document in credits
- **Paid license**: Keep receipt/proof of purchase

## Font List

### Needed Fonts

**UI Font**
- [ ] Menu/button font
- [ ] Settings/info font
- [ ] Subtitle font (if needed)

**Gameplay Font**
- [ ] Score display
- [ ] Combo/multiplier
- [ ] Distance/metrics

## Free Font Resources

### Recommended Sources
- Google Fonts (free, open source)
- Font Squirrel (100% free for commercial use)
- DaFont (check license per font)
- 1001 Fonts (check license per font)

### Popular Game Fonts
- Roboto (clean, modern)
- Montserrat (geometric, bold)
- Bebas Neue (tall, impactful)
- Orbitron (futuristic)
- Press Start 2P (retro gaming)

## Git LFS

Font files are tracked with Git LFS:

```bash
git lfs install
git lfs track "*.ttf"
git lfs track "*.otf"
```

## Unity Import Settings

1. Import font file to Unity
2. In Inspector:
   - Font Size: Auto
   - Rendering Mode: Smooth
   - Character: Dynamic (if needed)
   - Font Names: Add font name

## TextMeshPro Settings

For TMP Font Assets:
- **Atlas Resolution**: 512x512 (UI), 1024x1024 (large text)
- **Padding**: 5-9
- **Packing Method**: Optimum
- **Include Font Features**: As needed

## Best Practices

### Performance
- Use TextMeshPro for better performance
- Create font atlases for common characters
- Avoid generating characters at runtime
- Reuse font materials

### Readability
- Test fonts on actual devices
- Ensure contrast with backgrounds
- Add outlines/shadows for clarity
- Size appropriately for viewing distance

### Localization
- Ensure character set includes all needed languages
- Plan for text expansion (some languages are longer)
- Test with actual translated text

## Testing Checklist

- [ ] Font renders clearly on mobile devices
- [ ] Numbers are easily distinguishable
- [ ] Readability at various sizes
- [ ] Works with TextMeshPro
- [ ] Proper licensing documentation
- [ ] Font atlases generated correctly
- [ ] No missing characters
- [ ] Performance is acceptable

## Credits

Document all fonts used:

```
Font Name: [Name]
Author: [Author/Foundry]
License: [License Type]
Source: [URL]
```

Example:
```
Font Name: Roboto
Author: Google
License: Apache License 2.0
Source: https://fonts.google.com/specimen/Roboto
```
