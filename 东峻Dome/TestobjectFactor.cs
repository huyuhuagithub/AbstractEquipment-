
using BaseModule.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace 东峻Dome
{
   public class TestobjectFactor
    {
        public static T Create<T>() where T : test
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"ConfigFiles\\");

            string pathlog = Path.Combine(path,$"{typeof(T).Name}.json");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string info = File.ReadAllText(pathlog);
            T model = JsonHelper.JsonToObj<T>(info);
            return model;
        }
    }
}