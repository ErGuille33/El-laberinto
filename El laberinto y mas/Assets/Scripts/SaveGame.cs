using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SaveGame : MonoBehaviour
{
    protected SaveData saveData;
    HashData hash;

    private void Awake()
    {
        if (_instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPacks(int nPacks)
    {
        hash = new HashData();
        hash.packs = new int[nPacks];
    }

    public void saveLevel(int hints, int[] packsLevel)
    {
        hash.hints = hints;
        hash.packs = packsLevel;

        string saveGameData = JsonUtility.ToJson(saveData);


        hash.json = saveGameData;
        hash.hash = saveGameData.GetHashCode();

        string auxHash = JsonUtility.ToJson(hash);
        PlayerPrefs.SetString("Save", auxHash);
    }

    public void loadLevels(out int hints, out int[] packsLevel)
    {
        
        string verify = PlayerPrefs.GetString("Save","Nada");

        if (PlayerPrefs.HasKey("Save"))
        {
            HashData hash = JsonUtility.FromJson<HashData>(verify);

            if (hash.hash == hash.json.GetHashCode())
            {
                SaveData saveGameData = JsonUtility.FromJson<SaveData>(verify);
                hints = hash.hints;

                packsLevel = hash.packs;
                
            }
            else
            {
                hints = 0;
                packsLevel = new int[1500];

                for (int i = 0; i < 1500; i++)
                {
                    packsLevel[i] = -1;
                }
            }
        }
        else
        {
            hints = 0;
            packsLevel = new int[1500];

            for (int i = 0; i < 1500; i++)
            {
                packsLevel[i] = -1;
            }
            saveLevel(hints, packsLevel);
        }
  


    }

    [Serializable]
    protected class SaveData
    {
        //Un maximo de 1500 packs posibles nos parece más que aceptable
   
        
    }

    [Serializable]
    protected class HashData
    {
        //Un maximo de 1500 packs posibles nos parece más que aceptable
        public int hash;
        public string json;
        public int[] packs = new int[1500];
        public int hints;

    }
    static SaveGame _instance;
}
