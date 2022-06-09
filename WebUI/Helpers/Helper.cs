﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Helpers
{
    public class Helper
    {
        public static string GetPath(string root, params string[] folders)
        {
            string resultPath = root;
            foreach (var folder in folders)
            {
                resultPath = Path.Combine(resultPath, folder);
            }
            return resultPath;
        }
    }
}
