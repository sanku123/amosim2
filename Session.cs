using AmoSim2.Player;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace AmoSim2
{
    public static class Session 
    {
       

        public static string FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "Player_Data.json";

        public static void SavePlayerData(string path, Model obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            File.WriteAllText(path, json);
        }

        public static Model LoadPlayerData(string path, Model obj)
        {
            if (new FileInfo(path).Length != 0)
            {
                string json = File.ReadAllText(path);
                JsonConvert.PopulateObject(json, obj);
                return obj;
            }
            return obj;
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