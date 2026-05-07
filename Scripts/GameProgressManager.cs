using UnityEngine;
using TMPro;

public class GameProgressManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text levelTitleText;
    public TMP_Text progressCounterText;

    [Header("Level 1 - Targets")]
    public Target[] level1Targets;

    [Header("Level 2 - Unmovable Enemies")]
    public GameObject[] level2Enemies;

    [Header("Level 3 - Moving Enemies")]
    public GameObject[] level3MovingEnemies;

    [Header("Boss")]
    public GameObject boss;
    public GameObject bossDoor;

    private int currentLevel = 1;

    private int level1Destroyed = 0;
    private int level2Destroyed = 0;
    private int level3Destroyed = 0;
    private int bossDefeated = 0;

    void Start()
    {
        StartLevel1();
    }

    void StartLevel1()
    {
        currentLevel = 1;
        level1Destroyed = 0;

        SetLevelText("LEVEL 1");
        SetCounter(level1Destroyed, level1Targets.Length);

        SetObjectsActive(level2Enemies, false);
        SetObjectsActive(level3MovingEnemies, false);

        if (boss != null)
            boss.SetActive(false);
    }

    public void Level1TargetDestroyed()
    {
        if (currentLevel != 1) return;

        level1Destroyed++;
        SetCounter(level1Destroyed, level1Targets.Length);

        if (level1Destroyed >= level1Targets.Length)
        {
            StartLevel2();
        }
    }

    void StartLevel2()
    {
        currentLevel = 2;
        level2Destroyed = 0;

        SetLevelText("LEVEL 2");
        SetCounter(level2Destroyed, level2Enemies.Length);

        SetObjectsActive(level2Enemies, true);
    }

    public void Level2EnemyDestroyed()
    {
        if (currentLevel != 2) return;

        level2Destroyed++;
        SetCounter(level2Destroyed, level2Enemies.Length);

        if (level2Destroyed >= level2Enemies.Length)
        {
            StartLevel3();
        }
    }

    void StartLevel3()
    {
        currentLevel = 3;
        level3Destroyed = 0;

        SetLevelText("LEVEL 3");
        SetCounter(level3Destroyed, level3MovingEnemies.Length);

        SetObjectsActive(level3MovingEnemies, true);
    }

    public void Level3EnemyDestroyed()
    {
        if (currentLevel != 3) return;

        level3Destroyed++;
        SetCounter(level3Destroyed, level3MovingEnemies.Length);

        if (level3Destroyed >= level3MovingEnemies.Length)
        {
            StartBossFight();
        }
    }

    void StartBossFight()
    {
        currentLevel = 4;
        bossDefeated = 0;

        SetLevelText("BOSS FIGHT");
        SetCounter(bossDefeated, 1);

        if (bossDoor != null)
            bossDoor.SetActive(false); // door opens by disappearing

        if (boss != null)
            boss.SetActive(true);
    }

    public void BossDefeated()
    {
        if (currentLevel != 4) return;

        bossDefeated = 1;
        SetCounter(1, 1);

        SetLevelText("GAME COMPLETED");
    }

    void SetLevelText(string text)
    {
        if (levelTitleText != null)
            levelTitleText.text = text;
    }

    void SetCounter(int defeated, int total)
    {
        if (progressCounterText != null)
            progressCounterText.text = defeated + "/" + total;
    }

    void SetObjectsActive(GameObject[] objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
                obj.SetActive(active);
        }
    }
}