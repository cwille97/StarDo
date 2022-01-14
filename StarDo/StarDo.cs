using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace StarDo
{
    public class ModEntry : Mod
    {
        /// <summary>Save specific mod configuration.</summary>
        private static ModData Data;
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.GameLoop.SaveCreated += this.OnSaveCreated;
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
        }

        private void SetupData()
        {
            Data = this.Helper.Data.ReadJsonFile<ModData>($"{Constants.SaveFolderName}.json");
            if (Data == null)
            {
                Data = new ModData();
                this.Helper.Data.WriteJsonFile($"{Constants.SaveFolderName}.json", Data);
            }
        }

        private void OnSaveCreated(object sender, SaveCreatedEventArgs e)
        {
            this.SetupData();
        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            this.SetupData();
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (e.Button == Data.KeyBinding)
            {
                Game1.activeClickableMenu = new TaskMenu(this.UpdateData);
            }
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            // clear checked items when the day starts
        }

        private void UpdateData()
        {
            // Save the Data back to the JSON file
            return;
        }
    }
}
