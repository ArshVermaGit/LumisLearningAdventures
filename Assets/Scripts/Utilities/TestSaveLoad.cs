using UnityEngine;

public class TestSaveLoad : MonoBehaviour
{
    [ContextMenu("Test Save System")]
    void TestSave()
    {
        SaveData testData = new SaveData
        {
            totalStars = 50,
            totalCoins = 100,
            totalPlayTime = 3600 // Example: 1 hour in seconds
        };
        SaveLoadSystem.SaveGame(testData);

        // Also test settings save
        SaveLoadSystem.SaveSettings(0.8f, 0.7f, true, true);

        // Verify
        if (SaveLoadSystem.SaveFileExists())
        {
            Debug.Log("✓ Save file created successfully");
            Debug.Log(SaveLoadSystem.GetSaveFileInfo());
        }
    }

    [ContextMenu("Test Load System")]
    void TestLoad()
    {
        SaveData loadedData = SaveLoadSystem.LoadGame();
        if (loadedData != null)
        {
            Debug.Log($"✓ Load successful - Stars: {loadedData.totalStars}, Coins: {loadedData.totalCoins}, Play Time: {loadedData.totalPlayTime}s");
        }
        else
        {
            Debug.Log("No save data to load");
        }

        // Also test settings load
        SettingsData settings = SaveLoadSystem.LoadSettings();
        Debug.Log($"Settings - Music: {settings.musicVolume}, SFX: {settings.sfxVolume}");
    }

    [ContextMenu("Test Backup System")]
    void TestBackup()
    {
        SaveLoadSystem.CreateBackup();
    }
}