# Web Prototype - Endless Runner

This directory contains the HTML/CSS/JavaScript prototype for the Endless Runner game. This prototype serves as a quick proof-of-concept to test core mechanics before full Unity development.

## Purpose

The web prototype allows for:
- Rapid iteration on game mechanics
- Quick testing of core gameplay concepts
- Easy sharing with stakeholders for feedback
- Low-friction playtesting without builds
- Validation of game feel before committing to full development

## Current Features

- Basic player movement (lane switching)
- Obstacle spawning and collision detection
- Simple scoring system
- Touch/mouse input support

## How to Run

Simply open `index.html` in a web browser:

```bash
# Option 1: Direct file open
# Double-click index.html in file explorer

# Option 2: Local server (recommended for mobile testing)
# Using Python 3
python -m http.server 8000

# Using Node.js (if you have http-server installed)
npx http-server

# Using PHP
php -S localhost:8000
```

Then navigate to:
```
http://localhost:8000
```

## Mobile Testing

To test on mobile device:
1. Run a local server (see above)
2. Find your computer's local IP address
   - Windows: `ipconfig`
   - Mac/Linux: `ifconfig`
3. On mobile browser, navigate to: `http://[YOUR_IP]:8000`
4. Ensure mobile and computer are on same network

## File Structure

```
prototype-web/
├── index.html          # Main HTML file
├── styles.css          # Styling
├── game.js             # Game logic
└── README.md           # This file
```

## Technologies Used

- HTML5 Canvas for rendering
- Vanilla JavaScript for game logic
- CSS for UI styling
- No external dependencies

## Next Steps

Once core mechanics are validated:
1. Review gameplay feel
2. Gather feedback from stakeholders
3. Document learnings
4. Begin Unity implementation based on prototype

## Known Limitations

This is a prototype only and intentionally lacks:
- Advanced graphics/animations
- Sound effects/music
- Complex game systems
- Performance optimizations
- Mobile-specific features

These will be implemented in the Unity version.

## Feedback

To provide feedback on the prototype:
1. Document what works well
2. Note any issues with game feel
3. Suggest improvements to mechanics
4. Share with team for discussion

## Transition to Unity

Key lessons learned from this prototype should inform the Unity implementation:
- Player movement speed and responsiveness
- Obstacle spacing and difficulty
- Lane width and count
- Game pace and feel
- Input responsiveness

Refer to `docs/game-design-document.md` and `docs/technical-architecture.md` for Unity implementation details.
