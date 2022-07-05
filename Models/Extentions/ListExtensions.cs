
using System.Collections.Generic;
using System.Linq;

namespace Models.Extentions
{
    public static class ListExtensions
    {
        public static string GetNextCommand(this List<string> commands)
        {
            if(commands == null || commands.Count == 0) return null;    
            string command = commands.FirstOrDefault();
            commands.RemoveAt(0);

            return command.ToUpper();
        }
    }
}
