using AmoSim2.Battle;
using AmoSim2.Player;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace AmoSim2
{
    public static class Session
    {
        public static string FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "Player_Data.json";

        public static void SavePlayerData(PlayerModel obj)
        {
            File.WriteAllText(@FILE_PATH, JsonConvert.SerializeObject(obj));
        }

        public static PlayerModel LoadPlayerData(PlayerModel obj)
        {
            if (new FileInfo(FILE_PATH).Length != 0)
            {
                string json = File.ReadAllText(@FILE_PATH);
                JsonConvert.PopulateObject(json, obj);
                return obj;
            }
            return obj;
        }

        public static void SaveBattleData(ObservableCollection<BattleResults> obj)
        {
            string FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "Battle_Data.json";
            File.WriteAllText(@FILE_PATH, JsonConvert.SerializeObject(obj));
        }

        public static ObservableCollection<BattleResults> LoadBattleData(ObservableCollection<BattleResults> obj)
        {
            string FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "Battle_Data.json";

            if (new FileInfo(FILE_PATH).Length == 0)
            {
                return obj;
            }
            else
            {
                string json = File.ReadAllText(@FILE_PATH);
                JsonConvert.PopulateObject(json, obj);
                return obj;
            }
        }

        public class PropertyCopier<TParent, TChild> where TParent : class
                                              where TChild : class
        {
            public static void Copy(TParent parent, TChild child)
            {
                var parentProperties = parent.GetType().GetProperties();
                var childProperties = child.GetType().GetProperties();

                foreach (var parentProperty in parentProperties)
                {
                    foreach (var childProperty in childProperties)
                    {
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            childProperty.SetValue(child, parentProperty.GetValue(parent));
                            break;
                        }
                    }
                }
            }
        }
    }
}