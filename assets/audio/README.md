# Audio

This directory contains all audio assets for the Endless Runner game.

## Structure

Organize audio files by category:

```
audio/
├── music/           # Background music tracks
├── sfx/            # Sound effects
│   ├── player/     # Player-related sounds
│   ├── ui/         # UI feedback sounds
│   ├── collectibles/  # Coin/power-up sounds
│   └── ambient/    # Environmental sounds
└── voice/          # Voice-overs (if any)
```

## Asset Guidelines

### File Formats

**Music**
- Format: OGG Vorbis (preferred for mobile)
- Alternative: MP3 (good compatibility)
- Sample Rate: 44.1 kHz
- Bitrate: 128-192 kbps (balance quality/size)

**Sound Effects**
- Format: WAV (for short sounds) or OGG
- Sample Rate: 22.05 kHz or 44.1 kHz
- Bit Depth: 16-bit
- Keep files short and optimized

### Naming Convention

```
category_name_variation.ext

Examples:
music_gameplay_loop_01.ogg
sfx_jump_light.wav
sfx_coin_collect.wav
sfx_powerup_shield_activate.ogg
ui_button_click.wav
```

### Audio Guidelines

**Music**
- Loopable background tracks
- Seamless loop points
- Consistent volume levels
- Dynamic range appropriate for mobile

**Sound Effects**
- Short duration (typically <2 seconds)
- Clean start and end (no pops/clicks)
- Normalized volume
- Variation sets for repetitive sounds

## File Size Optimization

- Compress music files (OGG recommended)
- Use appropriate bitrates
- Mono for non-directional sounds
- Keep total audio assets under 20MB if possible

## Git LFS

Audio files are tracked with Git LFS:

```bash
git lfs install
git lfs track "*.wav"
git lfs track "*.mp3"
git lfs track "*.ogg"
```

## Unity Import Settings

**Music**
```
Load Type: Streaming
Compression Format: Vorbis
Quality: 70-100
Sample Rate Setting: Preserve Sample Rate
```

**Short SFX**
```
Load Type: Decompress On Load
Compression Format: ADPCM or PCM
Force To Mono: Yes (for non-positional)
```

**Longer SFX**
```
Load Type: Compressed In Memory
Compression Format: Vorbis
Quality: 70
```

## Audio List

### Music Tracks Needed
- [ ] Main menu theme
- [ ] Gameplay loop (upbeat)
- [ ] Game over theme

### Sound Effects Needed

**Player Actions**
- [ ] Jump
- [ ] Land
- [ ] Slide
- [ ] Footsteps (loopable)

**Collectibles**
- [ ] Coin collect
- [ ] Power-up pickup
- [ ] Power-up activate

**UI**
- [ ] Button click
- [ ] Menu navigate
- [ ] Purchase success
- [ ] Achievement unlock

**Game Events**
- [ ] Collision/hit
- [ ] Game over
- [ ] Level up/milestone
- [ ] Speed increase

## Attribution

If using third-party audio:
- Document source and license
- Ensure commercial use is permitted
- Provide attribution as required

## Resources

### Free Audio Sources
- Freesound.org
- OpenGameArt.org
- Incompetech (Kevin MacLeod)
- Purple Planet Music
- Bensound

### Paid Audio Sources
- AudioJungle
- Epidemic Sound
- Artlist
- Soundstripe

## Testing Checklist

- [ ] All audio plays without pops/clicks
- [ ] Volume levels are balanced
- [ ] Music loops seamlessly
- [ ] SFX timing feels responsive
- [ ] No audio artifacts or distortion
- [ ] Works on both iOS and Android
