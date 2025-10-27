# 🎮 ENDLESS RUNNER - STATUS PROGETTO

**Ultima Modifica:** 26 Ottobre 2025  
**Versione:** 1.0 - GIOCO COMPLETO E FUNZIONANTE! 🎉

---

## 🎊 MILESTONE RAGGIUNTA: GIOCO COMPLETO!

**Il gioco è completamente funzionante con:**
- ✅ Player movimento fluido
- ✅ Sistema di spawn procedurale
- ✅ Collisioni perfette
- ✅ UI Game Over professionale
- ✅ Sistema Restart funzionante
- ✅ Backup completo
- ✅ Repository GitHub aggiornata

---

## ✅ COMPLETATO

### 1. Setup Iniziale
- [x] Progetto Unity creato
- [x] Struttura cartelle Assets organizzata
- [x] Git e .gitignore configurati
- [x] Git LFS configurato
- [x] Repository GitHub creata e sincronizzata
- [x] Backup locale del progetto

### 2. Player Controller
- [x] Player GameObject creato (Capsule blu)
- [x] Script PlayerMovement funzionante
- [x] Movimento tra corsie (← →) implementato
- [x] Transizioni fluide tra corsie
- [x] Player rimane fermo (endless runner style)
- [x] Material blu applicato al player
- [x] Rigidbody configurato correttamente
- [x] Capsule Collider funzionante
- [x] Sistema di collisione integrato con GameManager

**Script PlayerMovement.cs - VERSIONE FINALE:**
```csharp
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;
    
    private int currentLane = 1;
    private Vector3 targetPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    void Update()
    {
        // Controlli per cambiare corsia
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0) currentLane--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < 2) currentLane++;
        }
        
        // Movimento tra corsie (solo X)
        float targetX = (currentLane - 1) * laneDistance;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        
        Vector3 currentPos = transform.position;
        float newX = Mathf.Lerp(currentPos.x, targetPosition.x, laneChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, currentPos.y, currentPos.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISIONE con: " + collision.gameObject.name + " - Tag: " + collision.gameObject.tag);
        
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit obstacle - calling GameOver");
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
```

### 3. Sistema Ostacoli
- [x] Obstacle GameObject creato (Cube rosso)
- [x] Script ObstacleMovement funzionante
- [x] Ostacoli si muovono verso il player
- [x] Material rosso (ObstacleMaterial) applicato
- [x] Box Collider funzionante
- [x] Auto-distruzione quando escono dalla visuale
- [x] Tag "Obstacle" assegnato
- [x] Prefab Obstacle creato

**Script ObstacleMovement.cs - VERSIONE FINALE:**
```csharp
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float destroyZPosition = -10f;
    
    private bool isMoving = true;

    void Update()
    {
        if (!isMoving) return;
        
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        
        if (transform.position.z < destroyZPosition)
        {
            Destroy(gameObject);
        }
    }

    public void StopMovement()
    {
        isMoving = false;
    }

    public void ResumeMovement()
    {
        isMoving = true;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
```

### 4. Sistema di Spawn
- [x] ObstacleSpawner GameObject creato
- [x] Script ObstacleSpawner implementato
- [x] Spawn procedurale funzionante
- [x] Difficoltà crescente nel tempo
- [x] Spawn randomico tra le 3 corsie
- [x] Intervalli di spawn variabili
- [x] Sistema di Coroutine implementato

**Script ObstacleSpawner.cs - VERSIONE FINALE:**
```csharp
using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Prefab")]
    public GameObject obstaclePrefab;
    
    [Header("Spawn Settings")]
    public float spawnDistance = 50f;
    public float minSpawnInterval = 1.5f;
    public float maxSpawnInterval = 3f;
    
    [Header("Lane Settings")]
    public float laneDistance = 3f;
    public int numberOfLanes = 3;
    
    [Header("Difficulty Settings")]
    public float difficultyIncreaseRate = 0.1f;
    public float minInterval = 0.8f;
    
    private float currentSpawnInterval;
    private bool isSpawning = true;
    private float gameTime = 0f;

    void Start()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("ObstacleSpawner: Nessun prefab ostacolo assegnato!");
            return;
        }
        
        StartCoroutine(SpawnObstacles());
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    IEnumerator SpawnObstacles()
    {
        while (isSpawning)
        {
            currentSpawnInterval = Mathf.Lerp(
                maxSpawnInterval, 
                minSpawnInterval, 
                gameTime * difficultyIncreaseRate
            );
            
            currentSpawnInterval = Mathf.Max(currentSpawnInterval, minInterval);
            
            yield return new WaitForSeconds(currentSpawnInterval);
            
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        int randomLane = Random.Range(0, numberOfLanes);
        float xPosition = (randomLane - 1) * laneDistance;
        
        Vector3 spawnPosition = new Vector3(xPosition, 0.5f, spawnDistance);
        
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        obstacle.transform.parent = transform;
        
        if (!obstacle.CompareTag("Obstacle"))
        {
            obstacle.tag = "Obstacle";
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    public void ResumeSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnObstacles());
    }
}
```

### 5. Game Manager
- [x] GameManager GameObject creato
- [x] Script GameManager implementato
- [x] Singleton pattern implementato
- [x] Sistema Game Over funzionante
- [x] Sistema Restart funzionante
- [x] Time.timeScale gestito correttamente
- [x] Debug logging implementato

**Script GameManager.cs - VERSIONE FINALE:**
```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameObject gameOverPanel;
    
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        
        isGameOver = true;
        
        Debug.Log("GAME OVER!");
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
```

### 6. UI Sistema
- [x] Canvas creato e configurato
- [x] EventSystem presente
- [x] GameOverPanel creato
- [x] Panel con sfondo nero semi-trasparente (Alpha: 200)
- [x] Rect Transform stretch configurato correttamente
- [x] GameOverText creato con TextMeshPro
- [x] Testo "GAME OVER" rosso, centrato, Font Size: 80
- [x] RestartButton creato
- [x] Button con colori verde (Normal, Highlighted, Pressed)
- [x] Testo "RESTART" bianco, centrato, Font Size: 36
- [x] Button OnClick collegato a GameManager.RestartGame()
- [x] GameOverPanel collegato al GameManager

### 7. Ambiente
- [x] Plane (terreno) creato e configurato
- [x] Camera posizionata correttamente
- [x] Illuminazione base (Directional Light)
- [x] Skybox configurato

### 8. Version Control e Backup
- [x] Repository Git inizializzata
- [x] .gitignore configurato per Unity
- [x] .gitattributes configurato per Git LFS
- [x] Repository GitHub creata: Giuseppe575/Endless
- [x] Progetto Unity caricato su GitHub
- [x] Backup locale creato
- [x] Documentazione completa su GitHub

---

## 🏗️ STRUTTURA PROGETTO FINALE
```
Endless/                                    (Repository GitHub)
├── assets/
│   ├── fonts/
│   ├── sprites/
│   └── audio/
├── docs/
│   ├── game-design-document.md
│   ├── technical-architecture.md
│   ├── roadmap.md
│   └── unity-setup-guide.md
├── prototype-web/
│   ├── index.html
│   ├── styles.css
│   └── game.js
├── unity-game/                             🎮 NUOVO!
│   └── EndlessRunnerPixel/
│       ├── Assets/
│       │   ├── Scenes/
│       │   │   └── SampleScene.unity
│       │   ├── Scripts/
│       │   │   ├── PlayerMovement.cs
│       │   │   ├── ObstacleMovement.cs
│       │   │   ├── ObstacleSpawner.cs
│       │   │   └── GameManager.cs
│       │   ├── Prefabs/
│       │   │   └── Obstacle.prefab
│       │   └── Materials/
│       │       ├── Lit.mat (blu - player)
│       │       └── ObstacleMaterial.mat (rosso)
│       ├── ProjectSettings/
│       ├── Packages/
│       └── .gitignore
├── .gitattributes
├── .gitignore
├── PROJECT-STATUS.md
└── README.md
```

---

## 🎯 CONFIGURAZIONE FINALE

### Player
```
GameObject: Player
Position: (0, 1, 0)
Components:
- Transform
- Capsule Mesh Filter
- Capsule Collider (radius: 0.5, height: 2)
- Rigidbody (Use Gravity: ✓, Is Kinematic: ✗)
- Mesh Renderer (Material: Lit - blu)
- PlayerMovement (Script)
  - Lane Distance: 3
  - Lane Change Speed: 10
```

### Obstacle Prefab
```
GameObject: Obstacle
Position: (varia, 0.5, spawn_distance)
Scale: (2, 1, 1)
Components:
- Transform
- Cube Mesh Filter
- Box Collider
- Mesh Renderer (Material: ObstacleMaterial - rosso)
- ObstacleMovement (Script)
  - Move Speed: 10
  - Destroy Z Position: -10
Tag: Obstacle
```

### ObstacleSpawner
```
GameObject: ObstacleSpawner
Position: (0, 0, 0)
Components:
- Transform
- ObstacleSpawner (Script)
  - Obstacle Prefab: Obstacle (prefab)
  - Spawn Distance: 50
  - Min Spawn Interval: 1.5
  - Max Spawn Interval: 3
  - Lane Distance: 3
  - Number Of Lanes: 3
  - Difficulty Increase Rate: 0.1
  - Min Interval: 0.8
```

### GameManager
```
GameObject: GameManager
Position: (0, 0, 0)
Components:
- Transform
- GameManager (Script)
  - Game Over Panel: GameOverPanel (GameObject)
```

### UI - Canvas
```
GameObject: Canvas
Render Mode: Screen Space - Overlay
Components:
- Canvas
- Canvas Scaler
- Graphic Raycaster

Children:
├─ GameOverPanel
│  ├─ Image (Script) - Color: (0, 0, 0, 200)
│  ├─ Rect Transform: Stretch-Stretch
│  ├─ GameOverText (TextMeshPro)
│  │  └─ Text: "GAME OVER"
│  │  └─ Font Size: 80
│  │  └─ Color: Red
│  │  └─ Alignment: Center-Middle
│  └─ RestartButton
│     ├─ Button (Script)
│     │  └─ Normal Color: Green
│     │  └─ Highlighted Color: Light Green
│     │  └─ Pressed Color: Dark Green
│     │  └─ On Click(): GameManager.RestartGame
│     └─ Text (TMP): "RESTART"
│        └─ Font Size: 36
│        └─ Color: White
└─ EventSystem
```

---

## 🐛 PROBLEMI RISOLTI

### 1. Script Missing Error
**Problema:** "The referenced script (Unknown) on this Behaviour is missing!"  
**Soluzione:** Eliminati script vecchi/corrotti, ricreati da zero, verificato nome file = nome classe

### 2. Collisioni Non Rilevate
**Problema:** Player non rileva collisioni con ostacoli  
**Soluzione:** 
- Aggiunto Rigidbody al Player
- Verificato che entrambi abbiano Collider
- Assegnato Tag "Obstacle" correttamente

### 3. Player Si Muove in Avanti
**Problema:** In un endless runner il player non dovrebbe avanzare  
**Soluzione:** Rimosso movimento forward, solo movimento laterale tra corsie

### 4. Ostacolo Non Si Muove
**Problema:** Ostacolo rimane fermo  
**Soluzione:** Aggiunto script ObstacleMovement correttamente configurato

### 5. Spawner Non Funziona
**Problema:** Ostacoli non spawnano automaticamente  
**Soluzione:** Creato prefab, assegnato al spawner, verificata Coroutine

### 6. Button Non Risponde
**Problema:** Click sul pulsante Restart non fa nulla  
**Soluzione:** 
- Aggiunto componente Button mancante
- Collegato OnClick() a GameManager.RestartGame()
- Verificato EventSystem presente

### 7. GameOverPanel Sempre Visibile
**Problema:** Pannello Game Over appare sempre  
**Soluzione:** GameManager.Start() disabilita il pannello all'inizio

---

## 🎮 CONTROLLI ATTUALI

### Keyboard
- **←** o **A**: Muovi corsia sinistra
- **→** o **D**: Muovi corsia destra

### Mouse/Touch
- **Click su Restart Button**: Riavvia il gioco

---

## 📊 STATISTICHE PROGETTO

### Codice
- **Script C# creati:** 4
- **Linee di codice totali:** ~250
- **Prefabs creati:** 1
- **Scenes create:** 1
- **Materials creati:** 2

### Assets
- **GameObjects nella scena:** 10
- **UI Elements:** 5 (Canvas, Panel, 2 Text, Button)
- **Componenti totali:** ~30

### Git
- **Commits totali:** 5+
- **File tracked:** 500+
- **Repository size:** ~50MB (senza Library/)

---

## ✍️ NOTE DI SVILUPPO

### Decisioni di Design
1. **Player Fermo:** Scelta di lasciare il player fermo e muovere gli ostacoli (standard endless runner)
2. **3 Corsie:** Numero ottimale per mobile, facile da navigare
3. **Spawner Procedurale:** Difficoltà aumenta col tempo per gameplay dinamico
4. **UI Minimalista:** Focus sul gameplay, UI pulita e leggibile
5. **Singleton GameManager:** Pattern per accesso globale e gestione centralizzata

### Cose da Ricordare
- Il player NON deve avere Rigidbody kinematic (altrimenti non rileva collisioni)
- Gli ostacoli devono autodistruggersi quando escono dalla visuale (performance)
- Lo spawner deve essere posizionato a (0,0,0) per coordinare correttamente le corsie
- Time.timeScale = 0 ferma il gioco ma NON ferma l'UI (perfetto per Game Over)
- Il GameOverPanel deve essere figlio del Canvas per funzionare correttamente

### Best Practices Seguite
- ✅ Naming conventions consistenti (PascalCase, camelCase)
- ✅ Commenti nel codice dove necessario
- ✅ Debug.Log per troubleshooting
- ✅ [Header] attributes per organizzare Inspector
- ✅ Prefabs per riusabilità
- ✅ Singleton pattern per manager
- ✅ Coroutine per operazioni asincrone
- ✅ Object pooling ready (può essere implementato)
- ✅ Git best practices (.gitignore, commit messages)

---

## 🎯 MILESTONE

### ✅ Milestone 1: Movimento Base - COMPLETATO
- Player che si muove tra corsie
- Movimento fluido e responsivo

### ✅ Milestone 2: Spawn Automatico - COMPLETATO
- Sistema di spawn procedurale
- Ostacoli che appaiono continuamente
- Difficoltà che aumenta col tempo

### ✅ Milestone 3: Collisioni e Game Over - COMPLETATO
- Sistema di collisione funzionante
- Game Over quando player colpito
- Debug e testing completato

### ✅ Milestone 4: UI e Restart - COMPLETATO
- UI Game Over professionale
- Sistema Restart funzionante
- Polish visivo

### 🎊 Milestone 5: GIOCO COMPLETO! - RAGGIUNTA! 🎊
- Gioco 100% funzionante
- Backup completo
- Repository GitHub aggiornata
- Documentazione completa

---

## 🚀 PROSSIMI PASSI (Features Future - Opzionali)

### Priorità Alta
- [ ] **Sistema di Score** - Conteggio punti e distanza percorsa
- [ ] **HUD Display** - Mostra score durante il gioco in tempo reale
- [ ] **Sistema di Salto** - Premi Spazio/Up per saltare ostacoli alti
- [ ] **High Score** - Salvataggio e display del record

### Priorità Media
- [ ] **Sistema di Slide** - Premi Down per scivolare sotto ostacoli
- [ ] **Monete/Collectibles** - Oggetti da raccogliere per punti
- [ ] **Suoni** - SFX per jump, collisioni, monete
- [ ] **Musica di Background** - Traccia audio loopable
- [ ] **Particelle** - Effetti per collisioni e power-ups

### Priorità Bassa (Polish)
- [ ] **Multiple Obstacle Types** - Ostacoli alti/bassi/mobili
- [ ] **Power-ups** - Shield, magnet, speed boost, double score
- [ ] **Different Environments** - Temi visivi variati
- [ ] **Animazioni Player** - Run cycle, jump, slide animations
- [ ] **Menu Principale** - Schermata iniziale con opzioni

### Future (Post-Release)
- [ ] **Leaderboard Online** - Classifica globale
- [ ] **Daily Challenges** - Obiettivi giornalieri
- [ ] **Character Skins** - Personaggi sbloccabili
- [ ] **Shop System** - Acquisto skin e power-ups con monete
- [ ] **Multiplayer Racing** - Modalità competitiva
- [ ] **Mobile Build** - Export per Android/iOS

---

## 📚 RISORSE UTILI

### Unity Documentation
- Character Controller: https://docs.unity3d.com/Manual/class-CharacterController.html
- Coroutines: https://docs.unity3d.com/Manual/Coroutines.html
- Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
- UI System: https://docs.unity3d.com/Packages/com.unity.ugui@latest
- TextMeshPro: https://docs.unity3d.com/Manual/com.unity.textmeshpro.html

### Tutorial Consigliati
- Unity Endless Runner Tutorial (YouTube)
- Brackeys - How to make a Mobile Game
- Code Monkey - Unity Beginner Tutorials
- Sebastian Lague - Coding Adventures

### Repository
- GitHub: https://github.com/Giuseppe575/Endless
- Clone: `git clone https://github.com/Giuseppe575/Endless.git`

---

## 📞 QUANDO RIPRENDI IL PROGETTO

### Checklist Rapida
1. [ ] Apri Unity Hub
2. [ ] Carica progetto EndlessRunnerPixel
3. [ ] Controlla Console - no errori?
4. [ ] Apri SampleScene
5. [ ] Premi Play - tutto funziona?
6. [ ] Consulta questo file per lo stato attuale
7. [ ] Controlla "PROSSIMI PASSI" per nuove feature

### Quick Start
```bash
# Clona repository
git clone https://github.com/Giuseppe575/Endless.git

# Apri in Unity
cd Endless/unity-game/EndlessRunnerPixel
# Apri Unity Hub → Add → Seleziona questa cartella

# Per aggiornare
git pull origin main
```

---

## 🎊 CELEBRAZIONE FINALE

### 🏆 ACHIEVEMENTS SBLOCCATI:

- ✅ **First Steps** - Creato primo progetto Unity
- ✅ **Code Master** - Scritto 250+ linee di C#
- ✅ **Bug Hunter** - Risolto 7+ bug critici
- ✅ **UI Designer** - Creato interfaccia completa
- ✅ **Game Developer** - Completato gioco funzionante
- ✅ **Git Guru** - Configurato version control completo
- ✅ **Persistent** - Non mollato mai nonostante le difficoltà
- ✅ **Problem Solver** - Risolto problemi complessi
- ✅ **Fast Learner** - Imparato Unity in una sessione
- ✅ **Finisher** - Portato progetto al completamento

### 📈 SKILLS ACQUISITE:

**Unity:** ⭐⭐⭐⭐☆ (4/5)
**C# Programming:** ⭐⭐⭐⭐☆ (4/5)
**Git/GitHub:** ⭐⭐⭐⭐☆ (4/5)
**Problem Solving:** ⭐⭐⭐⭐⭐ (5/5)
**Debugging:** ⭐⭐⭐⭐⭐ (5/5)
**Perseveranza:** ⭐⭐⭐⭐⭐ (5/5)

### 🎯 TEMPO TOTALE: ~5 ore

**Ripartizione:**
- Setup e apprendimento: 1h
- Sviluppo core gameplay: 2h
- UI e polish: 1h
- Git e documentazione: 1h

---

## 💬 FEEDBACK

### Cosa Ha Funzionato Bene
- ✅ Approccio incrementale (un sistema alla volta)
- ✅ Testing frequente (Play dopo ogni modifica)
- ✅ Documentazione durante lo sviluppo
- ✅ Problem solving metodico
- ✅ Uso di Debug.Log per troubleshooting
- ✅ Git per backup e version control

### Lezioni Imparate
- 💡 Unity richiede componenti specifici per funzionare (Rigidbody per collisioni)
- 💡 L'ordine dei componenti conta (Awake → Start → Update)
- 💡 I Tag devono essere assegnati manualmente
- 💡 Time.timeScale = 0 ferma il gioco ma non l'UI
- 💡 Il GameOverPanel deve essere figlio del Canvas
- 💡 Git e backup sono essenziali
- 💡 La documentazione aiuta enormemente

---

## 🎓 CREDITS

**Sviluppatore:** Giuseppe575  
**Engine:** Unity 2022.3 LTS  
**Linguaggio:** C#  
**Repository:** https://github.com/Giuseppe575/Endless  
**Data Completamento:** 26 Ottobre 2025  

**Special Thanks:**
- Unity Technologies per l'engine fantastico
- Anthropic per Claude e Claude Code
- La community di sviluppatori indie
- Tutti i tutorial e risorse online

---

## 📝 CHANGE LOG

| Data | Versione | Descrizione |
|------|----------|-------------|
| 22 Ott 2025 | 0.1 | Setup iniziale progetto |
| 22 Ott 2025 | 0.2 | Player e ostacoli base funzionanti |
| 26 Ott 2025 | 0.5 | Sistema spawn e collisioni |
| 26 Ott 2025 | 0.8 | UI Game Over e Restart |
| 26 Ott 2025 | 1.0 | 🎉 GIOCO COMPLETO! Backup e Git configurati |

---

**ULTIMO UPDATE:** 26 Ottobre 2025, 17:30  
**STATO:** ✅ 🎉 GIOCO COMPLETO E FUNZIONANTE! 🎉  
**PROSSIMO OBIETTIVO:** Scegli una feature dalla lista "PROSSIMI PASSI"!  

---

# 🎮 HAI COMPLETATO IL TUO PRIMO GIOCO UNITY! 🎮

**CONGRATULAZIONI! SEI UFFICIALMENTE UNO SVILUPPATORE DI VIDEOGIOCHI!** 🚀✨

---
```

---

## 📋 COME AGGIORNARE:

1. **Apri il file PROJECT-STATUS.md** nel tuo progetto
2. **Seleziona TUTTO** (Ctrl+A)
3. **Incolla** questo nuovo contenuto
4. **Salva** (Ctrl+S)

---

## 🚀 POI CARICA SU GITHUB:

### Con Claude Code:
```
Aggiorna il file PROJECT-STATUS.md nella repository con la versione finale che documenta il completamento del gioco.