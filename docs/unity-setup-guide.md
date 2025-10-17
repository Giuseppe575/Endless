# Unity Setup Guide - Endless Runner

This guide will walk you through setting up the Unity project for the Endless Runner mobile game.

---

## 1. Prerequisites

### Required Software
- **Unity Hub**: Latest version
- **Unity Editor**: 2022.3 LTS or later
- **Visual Studio Code** or **Visual Studio 2022** (IDE)
- **Git**: Version control
- **Git LFS**: For large asset files

### Platform SDKs
- **Android**: Android Studio, Android SDK, JDK
- **iOS**: Xcode (macOS only), iOS SDK

---

## 2. Unity Installation

### Step 1: Install Unity Hub
1. Download Unity Hub from [unity.com](https://unity.com/download)
2. Install and launch Unity Hub
3. Sign in with your Unity ID (create one if needed)

### Step 2: Install Unity Editor
1. In Unity Hub, go to **Installs** tab
2. Click **Install Editor**
3. Select **Unity 2022.3 LTS** (or latest LTS version)
4. Add modules during installation:
   - **Android Build Support**
     - Android SDK & NDK Tools
     - OpenJDK
   - **iOS Build Support** (macOS only)
   - **Visual Studio** or **Visual Studio Code Editor**
   - **Documentation**

### Step 3: Verify Installation
1. Check that Unity appears in the Installs tab
2. Verify version includes mobile build modules

---

## 3. Project Setup

### Option A: Create New Project

1. Open Unity Hub
2. Click **New Project**
3. Select Unity version (2022.3 LTS)
4. Choose **2D Core** or **3D Mobile** template
5. Set project name: **EndlessRunner**
6. Choose project location
7. Click **Create Project**

### Option B: Clone Existing Repository

```bash
# Clone the repository
git clone <repository-url>
cd Endless

# Install Git LFS
git lfs install

# Pull LFS assets
git lfs pull

# Open project in Unity Hub
# Add project via Unity Hub > Projects > Add
```

---

## 4. Project Settings Configuration

### Build Settings

1. Go to **File > Build Settings**
2. Select target platform:
   - **Android** or **iOS**
3. Click **Switch Platform**

### Player Settings

Navigate to **Edit > Project Settings > Player**

#### Common Settings
```
Company Name: [Your Company Name]
Product Name: Endless Runner
Version: 0.1.0
Default Icon: [Set your icon]
```

#### Android Settings

1. **Other Settings**
   ```
   Package Name: com.[yourcompany].endlessrunner
   Version: 0.1.0
   Bundle Version Code: 1
   Minimum API Level: Android 7.0 (API 24)
   Target API Level: Automatic (highest installed)
   Scripting Backend: IL2CPP
   Target Architectures: ARM64 (check both ARM64 and ARMv7 for compatibility)
   ```

2. **Quality Settings**
   ```
   Graphics API: OpenGL ES 3 / Vulkan
   Multithreaded Rendering: Enabled
   ```

3. **Keystore Setup** (for release builds)
   - Create keystore via Unity or Android Studio
   - Store securely (never commit to version control)

#### iOS Settings

1. **Other Settings**
   ```
   Bundle Identifier: com.[yourcompany].endlessrunner
   Version: 0.1.0
   Build: 1
   Target minimum iOS Version: 12.0
   Architecture: ARM64
   ```

2. **Camera Usage Description**: "Required for AR features" (if needed)
3. **Requires ARKit support**: NO (unless using AR)

### Quality Settings

Navigate to **Edit > Project Settings > Quality**

Create custom quality levels for mobile:

#### Low Quality
```
Texture Quality: Half Res
Anisotropic Textures: Disabled
Anti Aliasing: Disabled
Soft Particles: Disabled
Shadows: Disable
Shadow Resolution: Low
```

#### Medium Quality
```
Texture Quality: Full Res
Anisotropic Textures: Enabled
Anti Aliasing: 2x
Soft Particles: Enabled
Shadows: Hard Shadows
Shadow Resolution: Medium
```

#### High Quality (for high-end devices)
```
Texture Quality: Full Res
Anisotropic Textures: Enabled
Anti Aliasing: 4x
Soft Particles: Enabled
Shadows: Soft Shadows
Shadow Resolution: High
```

### Graphics Settings

Navigate to **Edit > Project Settings > Graphics**

```
Render Pipeline: Built-in (or URP for better performance)
Transparency Sort Mode: Perspective
```

### Physics Settings

Navigate to **Edit > Project Settings > Physics**

```
Default Contact Offset: 0.01
Sleep Threshold: 0.005
Queries Hit Triggers: Enabled
Layer Collision Matrix: Configure as needed
```

### Input Settings

Navigate to **Edit > Project Settings > Input Manager**

Or use the new **Input System** package:
1. Open Package Manager
2. Install **Input System**
3. Enable both old and new input (for compatibility)

### Time Settings

Navigate to **Edit > Project Settings > Time**

```
Fixed Timestep: 0.02 (50 FPS physics)
Maximum Allowed Timestep: 0.1
```

---

## 5. Package Manager Setup

Navigate to **Window > Package Manager**

### Recommended Packages

#### Essential Packages
- **TextMeshPro**: Modern text rendering
- **Cinemachine**: Advanced camera system
- **Input System**: New input handling
- **Unity UI**: UI system (usually included)

#### Optional but Useful
- **2D Sprite**: If using 2D sprites
- **Analytics**: Unity Analytics
- **Advertisements**: Unity Ads
- **In-App Purchasing**: Unity IAP

### Installing Packages

1. Open Package Manager
2. Select package from list
3. Click **Install**
4. Wait for installation to complete

---

## 6. Project Structure Setup

Create the following folder structure in the **Assets** directory:

```
Assets/
├── Scenes/
├── Scripts/
│   ├── Core/
│   ├── Player/
│   ├── Obstacles/
│   ├── Managers/
│   ├── PowerUps/
│   ├── UI/
│   └── Utils/
├── Prefabs/
│   ├── Player/
│   ├── Obstacles/
│   ├── PowerUps/
│   └── UI/
├── Materials/
├── Textures/
├── Models/
├── Audio/
│   ├── Music/
│   └── SFX/
├── Animations/
├── Fonts/
└── Resources/
```

---

## 7. Version Control Setup

### Initialize Git (if not already done)

```bash
# Initialize repository
git init

# Add remote
git remote add origin <repository-url>

# Add .gitignore (should already exist)
# Add .gitattributes for Git LFS (should already exist)
```

### Configure Git LFS

```bash
# Install Git LFS
git lfs install

# Track large file types (already configured in .gitattributes)
git lfs track "*.psd"
git lfs track "*.png"
git lfs track "*.jpg"
git lfs track "*.fbx"
git lfs track "*.mp3"
git lfs track "*.wav"
```

### First Commit

```bash
git add .
git commit -m "Initial Unity project setup"
git push -u origin main
```

---

## 8. Editor Configuration

### Preferences

Navigate to **Edit > Preferences**

#### External Tools
```
External Script Editor: Visual Studio Code (or your preferred IDE)
```

#### Colors
- Adjust Playmode tint for visual clarity

#### GI Cache
- Clear cache if needed for disk space

### Layout

1. Create custom layout for mobile development
2. Add Game view with common mobile resolutions:
   - 1920x1080 (16:9)
   - 2340x1080 (19.5:9)
   - 2436x1125 (iPhone X)
3. Save layout: **Window > Layouts > Save Layout**

---

## 9. Testing Setup

### Unity Remote (for quick testing)

1. Download **Unity Remote 5** app on iOS/Android
2. Connect device via USB
3. Enable USB debugging (Android)
4. In Unity: **Edit > Project Settings > Editor**
5. Set **Device** to your connected device
6. Press Play to stream to device

### Build and Run

1. Connect device via USB
2. Go to **File > Build Settings**
3. Click **Build and Run**
4. Wait for build to complete
5. App launches on device automatically

---

## 10. Performance Profiling

### Enable Profiler

1. **Window > Analysis > Profiler**
2. Build Development Build
3. Enable **Autoconnect Profiler**
4. Run on device
5. Monitor CPU, GPU, Memory, Rendering

### Optimize for Mobile

- Use Profiler to identify bottlenecks
- Optimize draw calls (batching)
- Reduce overdraw
- Optimize script execution
- Use object pooling

---

## 11. Common Issues & Solutions

### Issue: Build Fails on Android

**Solution**:
- Verify Android SDK/NDK paths in **Edit > Preferences > External Tools**
- Update to latest SDK build tools
- Check Java version compatibility

### Issue: iOS Build Requires Xcode

**Solution**:
- Xcode is required for iOS builds (macOS only)
- Update to latest Xcode version
- Accept Xcode license agreement

### Issue: App Crashes on Device

**Solution**:
- Check device logs via Logcat (Android) or Xcode console (iOS)
- Build with Development Build + Script Debugging
- Use try-catch blocks for error handling

### Issue: Poor Performance

**Solution**:
- Use Profiler to identify issues
- Reduce draw calls via batching
- Implement object pooling
- Lower quality settings
- Optimize physics calculations

---

## 12. Useful Resources

### Official Documentation
- [Unity Manual](https://docs.unity3d.com/Manual/index.html)
- [Unity Scripting Reference](https://docs.unity3d.com/ScriptReference/)
- [Mobile Optimization Guide](https://docs.unity3d.com/Manual/MobileOptimization.html)

### Community Resources
- Unity Forums
- Unity Discord
- Stack Overflow
- Reddit r/Unity3D

### Learning Resources
- Unity Learn Platform
- YouTube tutorials
- Udemy/Coursera courses

---

## 13. Next Steps

After completing this setup:

1. ✅ Verify project builds successfully for target platform
2. ✅ Create initial scene (MainMenu, Game)
3. ✅ Implement basic player controller
4. ✅ Review **game-design-document.md** for features to implement
5. ✅ Review **technical-architecture.md** for system design
6. ✅ Follow **roadmap.md** for development timeline

---

## Support

For project-specific questions:
- Check existing documentation in `/docs`
- Review code comments
- Consult team lead or senior developer

For Unity-specific issues:
- Unity Forums
- Unity Answers
- Official documentation
