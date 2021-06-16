using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

//Script cuya función es el guardado de partida
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
    //El número de paquetes de niveles a guardar
    public void setPacks(int nPacks)
    {
        hash = new HashData();
        hash.packs = new int[nPacks];
    }
    //GUardado con hash
    public void saveLevel(int hints, int[] packsLevel)
    {
        //Este código es para resetear la hoja de guardado en caso necesario
        /* 
        hash.hints = 0;
        for(int i = 0; i < packsLevel.Length; i++)
        {
            packsLevel[i] = 00;
        }*/
        hash.hints = hints;
        hash.packs = packsLevel;

        string saveGameData = JsonUtility.ToJson(saveData);


        hash.json = saveGameData;
        hash.hash = saveGameData.GetHashCode();

        string auxHash = JsonUtility.ToJson(hash);
        PlayerPrefs.SetString("Save", auxHash);
    }
    //Carga de niveles con la key
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

                for (int i = 1; i < 1500; i++)
                {
                    packsLevel[i] = 0;
                }
            }
        }
        else
        {
            hints = 0;
            packsLevel = new int[1500];

            for (int i = 1; i < 1500; i++)
            {
                packsLevel[i] = 0;
            }
            saveLevel(hints, packsLevel);
        }

    }

    [Serializable]
    protected class SaveData
    {
        //Clase vacía intermedia para guardado de datos
   
        
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
