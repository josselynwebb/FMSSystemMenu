using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FmsSystemMenu.Additional_Libraries
{
    public class IniFileParser
    {
        private Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();

        public IniFileParser(string path)
        {
            try
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";") || line.StartsWith("#"))
                        continue;

                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        var section = line.Trim('[', ']');
                        data[section] = new Dictionary<string, string>();
                    }
                    else
                    {
                        var keyValue = line.Split(new[] { '=' }, 2);
                        if (keyValue.Length == 2)
                        {
                            var section = data.Keys.Last();
                            data[section][keyValue[0].Trim()] = keyValue[1].Trim();
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public string GetValue(string section, string key)
        {
            return data.ContainsKey(section) && data[section].ContainsKey(key) ? data[section][key] : null;
        }

        public void Save(string path)
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (var section in data)
                {
                    writer.WriteLine($"[{section.Key}]");
                    foreach (var kvp in section.Value)
                    {
                        writer.WriteLine($"{kvp.Key}={kvp.Value}");
                    }
                    writer.WriteLine();
                }
            }
        }

        public void SetValue(string section, string key, string value)
        {
            if (!data.ContainsKey(section))
            {
                data[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }
            data[section][key] = value;
        }
    }
}
