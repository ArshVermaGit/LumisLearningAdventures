using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Handles all save/load operations for the game
/// Uses JSON for complex data and PlayerPrefs for simple settings
/// </summary>
public static class SaveLoadSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/lumi_save.json";
    private static string settingsFilePath = Application.persistentDataPath + "/lumi_settings.json";
   
    // Encryption key (in production, use a more secure method)
    private static string encryptionKey = "lumi_educational_game_2024";
   
    #region Main Save/Load Methods
   
    public static void SaveGame(SaveData data)
    {
        try
        {
            string jsonData = JsonUtility.ToJson(data, true);
            string encryptedData = SimpleEncryptDecrypt(jsonData);
           
            File.WriteAllText(saveFilePath, encryptedData);
           
            // Also save critical data to PlayerPrefs for quick access
            PlayerPrefs.SetInt("TotalStars", data.totalStars);
            PlayerPrefs.SetInt("TotalCoins", data.totalCoins);
            PlayerPrefs.SetInt("TotalPlayTime", data.totalPlayTime);
            PlayerPrefs.Save();
           
            Debug.Log($"Game saved to: {saveFilePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Save failed: {e.Message}");
        }
    }
   
    public static SaveData LoadGame()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                string encryptedData = File.ReadAllText(saveFilePath);
                string jsonData = SimpleEncryptDecrypt(encryptedData);
               
                SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
                Debug.Log("Game loaded successfully");
                return data;
            }
            else
            {
                Debug.Log("No save file found");
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Load failed: {e.Message}");
            return null;
        }
    }
   
    #endregion
   
    #region Settings Management
   
    public static void SaveSettings(float musicVol, float sfxVol, bool musicOn, bool sfxOn)
    {
        try
        {
            SettingsData settings = new SettingsData
            {
                musicVolume = musicVol,
                sfxVolume = sfxVol,
                musicEnabled = musicOn,
                sfxEnabled = sfxOn,
                language = "en",
                subtitlesEnabled = true
            };
           
            string jsonData = JsonUtility.ToJson(settings, true);
            File.WriteAllText(settingsFilePath, jsonData);
           
            // Also save to PlayerPrefs for quick access
            PlayerPrefs.SetFloat("MusicVolume", musicVol);
            PlayerPrefs.SetFloat("SFXVolume", sfxVol);
            PlayerPrefs.SetInt("MusicEnabled", musicOn ? 1 : 0);
            PlayerPrefs.SetInt("SFXEnabled", sfxOn ? 1 : 0);
            PlayerPrefs.Save();
           
            Debug.Log("Settings saved");
        }
        catch (Exception e)
        {
            Debug.LogError($"Settings save failed: {e.Message}");
        }
    }
   
    public static SettingsData LoadSettings()
    {
        try
        {
            if (File.Exists(settingsFilePath))
            {
                string jsonData = File.ReadAllText(settingsFilePath);
                SettingsData settings = JsonUtility.FromJson<SettingsData>(jsonData);
                return settings;
            }
            else
            {
                // Return default settings
                return new SettingsData();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Settings load failed: {e.Message}");
            return new SettingsData();
        }
    }
   
    #endregion
   
    #region Backup and Cloud Save Methods
   
    public static void CreateBackup()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                string backupPath = Application.persistentDataPath + $"/lumi_backup_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                File.Copy(saveFilePath, backupPath);
                Debug.Log($"Backup created: {backupPath}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Backup failed: {e.Message}");
        }
    }
   
    public static bool RestoreFromBackup(string backupPath)
    {
        try
        {
            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, saveFilePath, true);
                Debug.Log("Backup restored successfully");
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Debug.LogError($"Restore failed: {e.Message}");
            return false;
        }
    }
   
    // Cloud save simulation (for actual cloud save, integrate with platform-specific APIs)
    public static string ExportSaveData()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                string data = File.ReadAllText(saveFilePath);
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(data));
            }
            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"Export failed: {e.Message}");
            return null;
        }
    }
   
    public static bool ImportSaveData(string base64Data)
    {
        try
        {
            string data = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Data));
            File.WriteAllText(saveFilePath, data);
            Debug.Log("Save data imported successfully");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Import failed: {e.Message}");
            return false;
        }
    }
   
    #endregion
   
    #region Utility Methods
   
    public static bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }
   
    public static DateTime GetLastSaveTime()
    {
        if (File.Exists(saveFilePath))
        {
            return File.GetLastWriteTime(saveFilePath);
        }
        return DateTime.MinValue;
    }
   
    public static void DeleteSaveData()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            }
           
            // Clear PlayerPrefs
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
           
            Debug.Log("All save data deleted");
        }
        catch (Exception e)
        {
            Debug.LogError($"Delete failed: {e.Message}");
        }
    }
   
    public static string GetSaveFileInfo()
    {
        if (File.Exists(saveFilePath))
        {
            FileInfo info = new FileInfo(saveFilePath);
            return $"Size: {info.Length} bytes, Last Modified: {info.LastWriteTime}";
        }
        return "No save file found";
    }
   
    #endregion
   
    #region Encryption (Simple XOR - for educational games, more complex encryption may be needed for commercial)
   
    private static string SimpleEncryptDecrypt(string data)
    {
        // For production, use proper encryption like AES
        // This is a simple XOR encryption for basic obfuscation
        System.Text.StringBuilder result = new System.Text.StringBuilder();
       
        for (int i = 0; i < data.Length; i++)
        {
            result.Append((char)(data[i] ^ encryptionKey[i % encryptionKey.Length]));
        }
       
        return result.ToString();
    }
   
    #endregion
}

// Save data structure
[System.Serializable]
public class SaveData
{
    public int totalStars = 0;
    public int totalCoins = 0;
    public int totalPlayTime = 0;
   
    public SaveData() { }
}

// Settings data structure
[System.Serializable]
public class SettingsData
{
    public float musicVolume = 0.8f;
    public float sfxVolume = 0.8f;
    public bool musicEnabled = true;
    public bool sfxEnabled = true;
    public string language = "en";
    public bool subtitlesEnabled = true;
    public bool hapticFeedback = true;
   
    public SettingsData() { }
}