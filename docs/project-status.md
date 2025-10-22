# 🎮 ENDLESS RUNNER - STATUS PROGETTO

**Ultima Modifica:** 22 Ottobre 2025
**Versione:** 0.2 - Player e Ostacoli Funzionanti

---

## ✅ COMPLETATO

### 1. Setup Iniziale
- [x] Progetto Unity creato
- [x] Struttura cartelle Assets organizzata
- [x] Git e .gitignore configurati
- [x] Script base creati (PlayerMovement, ObstacleMovement, ObstacleSpawner)

### 2. Player Controller
- [x] Player GameObject creato (Capsule blu)
- [x] Script PlayerMovement funzionante
- [x] Movimento tra corsie (← →) implementato
- [x] Player rimane fermo (non si muove in avanti)
- [x] Material blu applicato al player
- [x] Posizione: (0, 1, 0)

**Script PlayerMovement.cs - VERSIONE FUNZIONANTE:**
```csharp
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    private int currentLane = 1;
    private Vector3 targetPosition;

    void Start()
    {
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
}
```

### 3. Ostacoli
- [x] Obstacle GameObject creato (Cube rosso)
- [x] Script ObstacleMovement funzionante
- [x] Ostacolo si muove verso il player
- [x] Material rosso (ObstacleMaterial) applicato
- [x] Box Collider presente
- [x] Posizione iniziale: (0, 0.5, 20)
- [x] Scale: (2, 1, 1) - ostacolo largo

**Script ObstacleMovement.cs - VERSIONE FUNZIONANTE:**
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

        // Muovi l'ostacolo verso il player (indietro lungo l'asse Z)
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // Distruggi l'ostacolo quando esce dalla visuale
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

### 4. Ambiente
- [x] Plane (terreno) creato
- [x] Camera configurata
- [x] Illuminazione base (Directional Light)

---

## 🚧 IN CORSO - PROSSIMO STEP

### Creare Sistema di Spawn Automatico

**Cosa fare ORA:**

#### STEP 1: Crea Cartella Prefabs
```
1. Nel Project panel → Assets
2. Click destro → Create → Folder
3. Nomina: "Prefabs"
```

#### STEP 2: Crea Prefab dell'Ostacolo
```
1. Nella Hierarchy, seleziona "Obstacle"
2. Trascinalo nella cartella Assets/Prefabs
3. L'icona diventerà blu (è un prefab!)
4. Elimina l'Obstacle dalla Hierarchy (il prefab rimane)
```

#### STEP 3: Crea ObstacleSpawner GameObject
```
1. Hierarchy → Click destro → Create Empty
2. Rinomina: "ObstacleSpawner"
3. Transform Position: (0, 0, 0)
```

#### STEP 4: Aggiungi Script ObstacleSpawner
```
1. Seleziona ObstacleSpawner
2. Inspector → Add Component
3. Cerca "ObstacleSpawner"
4. Aggiungilo
```

#### STEP 5: Configura ObstacleSpawner
```
Nell'Inspector dello ObstacleSpawner:

- Obstacle Prefab: [Trascina prefab Obstacle qui]
- Spawn Distance: 50
- Min Spawn Interval: 1.5
- Max Spawn Interval: 3
- Lane Distance: 3
- Number Of Lanes: 3
```

**Script ObstacleSpawner.cs - CODICE COMPLETO:**
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

---

## 📋 TODO - FEATURE DA IMPLEMENTARE

### Priorità Alta (Fare Subito)
- [ ] **Sistema di Spawn Automatico** (in corso - vedi sopra)
- [ ] **Tag "Obstacle"** - Creare e assegnare al prefab
- [ ] **Collisioni** - Testare collisione Player-Obstacle
- [ ] **Game Over** - Fermare gioco quando player colpito

### Priorità Media
- [ ] **Sistema di Salto** - Aggiungere jump al PlayerMovement
- [ ] **Sistema di Slide** - Aggiungere slide per ostacoli bassi
- [ ] **Monete/Collectibles** - Oggetti da raccogliere
- [ ] **Sistema di Score** - Conteggio punti e distanza
- [ ] **UI Base** - Score display, game over screen

### Priorità Bassa (Polish)
- [ ] **Suoni** - SFX per jump, collisioni, monete
- [ ] **Musica di Background**
- [ ] **Particelle** - Effetti per collisioni e monete
- [ ] **Multiple Obstacle Types** - Ostacoli alti/bassi/mobili
- [ ] **Power-ups** - Shield, magnet, speed boost
- [ ] **Menu Principale**
- [ ] **Settings Menu** - Audio, graphics

### Future (Post-Release)
- [ ] **Multiplayer/Leaderboard**
- [ ] **Daily Challenges**
- [ ] **Character Skins**
- [ ] **Different Environments**
- [ ] **Shop System**

---

## 🏗️ STRUTTURA PROGETTO

```
Assets/
├── Scenes/
│   └── SampleScene.unity (scena principale)
├── Scripts/
│   ├── PlayerMovement.cs ✅
│   ├── ObstacleMovement.cs ✅
│   └── ObstacleSpawner.cs ✅
├── Prefabs/
│   └── Obstacle.prefab (DA CREARE)
├── Materials/
│   ├── Lit.mat (blu - player)
│   └── ObstacleMaterial.mat (rosso - ostacoli)
└── ... (altre cartelle)
```

---

## 🎯 CONFIGURAZIONE ATTUALE

### Player
```
GameObject: Player
Position: (0, 1, 0)
Components:
- Transform
- Capsule Mesh Filter
- Capsule Collider (radius: 0.5, height: 2)
- Mesh Renderer (Material: Lit - blu)
- PlayerMovement (Script)
  - Lane Distance: 3
  - Lane Change Speed: 10
```

### Obstacle (Template per Prefab)
```
GameObject: Obstacle
Position: (0, 0.5, 20) iniziale
Scale: (2, 1, 1)
Components:
- Transform
- Cube Mesh Filter
- Box Collider
- Mesh Renderer (Material: ObstacleMaterial - rosso)
- ObstacleMovement (Script)
  - Move Speed: 10
  - Destroy Z Position: -10
Tag: Obstacle (DA ASSEGNARE)
```

### ObstacleSpawner (DA CREARE)
```
GameObject: ObstacleSpawner
Position: (0, 0, 0)
Components:
- Transform
- ObstacleSpawner (Script)
  - Obstacle Prefab: [Obstacle prefab]
  - Spawn Distance: 50
  - Min Spawn Interval: 1.5
  - Max Spawn Interval: 3
  - Lane Distance: 3
  - Number Of Lanes: 3
```

---

## 🐛 PROBLEMI RISOLTI

### 1. Script Missing Error
**Problema:** "The referenced script (Unknown) on this Behaviour is missing!"
**Causa:** File .meta corrotti o script con errori di compilazione
**Soluzione:**
- Eliminare script vecchi/rotti dai GameObject
- Ricreare script da zero se necessario
- Verificare che nome file = nome classe

### 2. Script Non Trovato in Add Component
**Problema:** Unity non trova lo script quando cerco di aggiungerlo
**Causa:** Errori di compilazione nello script
**Soluzione:**
- Controllare Console per errori rossi
- Correggere errori di sintassi
- Aspettare ricompilazione di Unity

### 3. Player Si Muove in Avanti
**Problema:** In un endless runner il player non dovrebbe avanzare
**Soluzione:** Rimosso movimento forward dal PlayerMovement, solo movimento laterale

### 4. Ostacolo Non Si Muove
**Problema:** Ostacolo rimane fermo
**Causa:** Script ObstacleMovement non attaccato
**Soluzione:** Aggiunto script ObstacleMovement al GameObject Obstacle

---

## 🔧 TROUBLESHOOTING COMUNI

### Unity Non Compila
1. Controlla Console per errori rossi
2. Verifica che tutti gli script abbiano `using UnityEngine;`
3. Verifica che nome file = nome classe (case-sensitive!)
4. Chiudi e riapri Unity se necessario

### GameObject Non Visibile in Scena
1. Seleziona GameObject in Hierarchy
2. Premi "F" per centrare la camera su di esso
3. Verifica Position in Transform
4. Verifica che Mesh Renderer sia abilitato

### Script Non Si Attacca
1. Controlla che non ci siano errori di compilazione
2. Prova Drag & Drop dal Project panel
3. Verifica che lo script sia un MonoBehaviour

### Collisioni Non Funzionano
1. Verifica che entrambi GameObject abbiano Collider
2. Verifica che almeno uno abbia Rigidbody o CharacterController
3. Verifica che i Tag siano assegnati correttamente
4. Controlla che i Layer collision matrix siano corretti

---

## 💡 BEST PRACTICES

### Naming Conventions
- **GameObject:** PascalCase (es: ObstacleSpawner, Player)
- **Script:** PascalCase, nome file = nome classe
- **Variables:** camelCase (es: moveSpeed, currentLane)
- **Public variables:** camelCase con [SerializeField]

### Organization
- Usa cartelle separate per Scripts, Prefabs, Materials
- Metti script correlati nella stessa cartella
- Usa commenti per codice complesso
- Mantieni Inspector organizzato con [Header]

### Performance
- Usa Object Pooling per oggetti che spawnano frequentemente
- Distruggi GameObject quando escono dalla visuale
- Evita GetComponent() in Update()
- Usa tag invece di nomi per identificare GameObject

---

## 📚 RISORSE UTILI

### Unity Documentation
- Character Controller: https://docs.unity3d.com/Manual/class-CharacterController.html
- Coroutines: https://docs.unity3d.com/Manual/Coroutines.html
- Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html

### Tutorial Consigliati
- Unity Endless Runner Tutorial (YouTube)
- Brackeys - How to make a Mobile Game
- Code Monkey - Unity Beginner Tutorials

---

## 🎮 CONTROLLI ATTUALI

### Keyboard
- **←** o **A**: Muovi corsia sinistra
- **→** o **D**: Muovi corsia destra

### Da Implementare
- **↑** o **W** o **Space**: Salto
- **↓** o **S**: Slide
- **Touch Controls**: Per mobile

---

## ✍️ NOTE DI SVILUPPO

### Decisioni di Design
1. **Player Fermo:** Scelta di lasciare il player fermo e muovere gli ostacoli verso di lui (standard endless runner)
2. **3 Corsie:** Numero ottimale per mobile, facile da navigare
3. **Spawner Procedurale:** Difficoltà aumenta col tempo

### Cose da Ricordare
- Il player NON deve avere Rigidbody (usa CharacterController)
- Gli ostacoli devono autodistruggersi quando escono dalla visuale
- Lo spawner deve essere posizionato a (0,0,0) per coordinare correttamente le corsie

---

## 📞 QUANDO RIPRENDI IL PROGETTO

### Checklist Rapida
1. [ ] Apri Unity
2. [ ] Controlla Console - no errori?
3. [ ] Premi Play - funziona?
4. [ ] Controlla questo file per il prossimo step
5. [ ] Continua da "🚧 IN CORSO"

### Prossima Sessione: Implementare ObstacleSpawner
**Obiettivo:** Far spawnare ostacoli automaticamente e continuamente

**Step da Seguire:**
1. Crea cartella Prefabs
2. Crea prefab Obstacle
3. Crea GameObject ObstacleSpawner
4. Configura spawner con prefab
5. Testa con Play - dovrebbero spawnare ostacoli infiniti!

---

## 🎯 MILESTONE

### Milestone 1: Movimento Base ✅ COMPLETATO
- Player che si muove tra corsie
- Ostacolo singolo funzionante

### Milestone 2: Spawn Automatico 🚧 IN CORSO
- Sistema di spawn procedurale
- Ostacoli che appaiono continuamente
- Difficoltà che aumenta col tempo

### Milestone 3: Gameplay Completo (Prossimo)
- Sistema di collisione e game over
- Sistema di score
- UI base

### Milestone 4: Polish (Futuro)
- Suoni e musica
- Effetti particellari
- Menu e settings

---

**ULTIMO UPDATE:** 22 Ottobre 2025, 15:30
**STATO:** ✅ Player e Ostacoli Funzionanti - Pronto per Spawner Automatico
**PROSSIMO STEP:** Creare Prefab e ObstacleSpawner (vedi sezione "🚧 IN CORSO")
