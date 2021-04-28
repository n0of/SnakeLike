using UnityEngine;

public static class PlayerPrefsHandler
{
    private static readonly string _recordKey = "record";

    public static int GetRecord()
    {
        return PlayerPrefs.HasKey(_recordKey) ? PlayerPrefs.GetInt(_recordKey) : 0;
    }

    public static void SetRecord(int newRecord)
    {
        PlayerPrefs.SetInt(_recordKey, newRecord);
    }
}
