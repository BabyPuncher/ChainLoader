using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ChainLoader
{
    public class IniFile 
    {
        private string Path;
        private string ExecutingAssembly = Assembly.GetExecutingAssembly().GetName().Name;
        
        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? ExecutingAssembly + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var keyValue = new StringBuilder(255);
            GetPrivateProfileString(Section ?? ExecutingAssembly, Key, "", keyValue, 255, Path);
            return keyValue.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? ExecutingAssembly, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? ExecutingAssembly);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? ExecutingAssembly);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}