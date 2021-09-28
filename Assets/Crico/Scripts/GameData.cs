using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Crico
{
    public class GameData : MonoBehaviour
    {
        static public GameData instance { get; private set; } = null;

        private const string FILENAME = "/SaveData.dat";

        [SerializeField] int _numStages = 1;

        public int numStages { get => _numStages; private set => _numStages = value; }

        public SaveData saveData { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
            
            LoadSaveData();
        }

        private void LoadSaveData()
        {
            if (File.Exists(Application.persistentDataPath
                   + FILENAME))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file =
                           File.Open(Application.persistentDataPath
                           + FILENAME, FileMode.Open);
                saveData = (SaveData)bf.Deserialize(file);
                file.Close();
            }
            else
            {
                saveData = new SaveData();
                SaveSaveData();
            }
        }

        public void SaveSaveData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath
                         + FILENAME);
            bf.Serialize(file, saveData);
            file.Close();
        }

    }

}
