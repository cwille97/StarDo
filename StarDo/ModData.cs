using StardewModdingAPI;
using System.Collections;

namespace StarDo
{
    class ModData
    {
        /// <summary>The list of task data.</summary>
        public ArrayList Tasks = new();

        /// <summary>The key which shows the task menu.</summary>
        public SButton KeyBinding { get; set; } = SButton.F2;
    }
}
