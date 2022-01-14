using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace StarDo
{
    class TaskMenu: IClickableMenu
    {
        /// <summary>The OK button to draw.</summary>
        private ClickableTextureComponent OkButton;

        /// <summary>The callback to invoke when the birthday value changes.</summary>
        private readonly Action OnChanged;

        public bool alllFinished;

        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary
        public TaskMenu(Action onChanged)
            : base(Game1.viewport.Width / 2 - (632 + IClickableMenu.borderWidth * 2) / 2, Game1.viewport.Height / 2 - (600 + IClickableMenu.borderWidth * 2) / 2 - Game1.tileSize, 632 + IClickableMenu.borderWidth * 2, 600 + IClickableMenu.borderWidth * 2 + Game1.tileSize)
        {
            this.OnChanged = onChanged;
            this.setUpPositions();
        }

        /// <summary>The method called when the game window changes size.</summary>
        /// <param name="oldBounds">The former viewport.</param>
        /// <param name="newBounds">The new viewport.</param>
        public override void gameWindowSizeChanged(Rectangle oldBounds, Rectangle newBounds)
        {
            base.gameWindowSizeChanged(oldBounds, newBounds);
            this.xPositionOnScreen = Game1.viewport.Width / 2 - (632 + IClickableMenu.borderWidth * 2) / 2;
            this.yPositionOnScreen = Game1.viewport.Height / 2 - (600 + IClickableMenu.borderWidth * 2) / 2 - Game1.tileSize;
            this.setUpPositions();
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Regenerate the UI.</summary>
        private void setUpPositions()
        {
            this.OkButton = new ClickableTextureComponent("OK", new Rectangle(this.xPositionOnScreen + this.width - IClickableMenu.borderWidth - IClickableMenu.spaceToClearSideBorder - Game1.tileSize, this.yPositionOnScreen + this.height - IClickableMenu.borderWidth - IClickableMenu.spaceToClearTopBorder + Game1.tileSize / 4, Game1.tileSize, Game1.tileSize), "", null, Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 46), 1f);


        }

        /// <summary>Handle a button click.</summary>
        /// <param name="name">The button name that was clicked.</param>
        private void handleButtonClick(string name)
        {
            if (name == null)
                return;

            switch (name)
            {
                // OK button
                case "OK":
                    this.alllFinished = true;
                    this.OnChanged();
                    Game1.exitActiveMenu();
                    break;

                default:
                    Game1.activeClickableMenu = new TaskMenu(this.OnChanged);
                    break;
            }
        }

        /// <summary>The method invoked when the player left-clicks on the menu.</summary>
        /// <param name="x">The X-position of the cursor.</param>
        /// <param name="y">The Y-position of the cursor.</param>
        /// <param name="playSound">Whether to enable sound.</param>
        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            if (this.OkButton.containsPoint(x, y))
            {
                this.handleButtonClick(this.OkButton.name);
                this.OkButton.scale -= 0.25f;
                this.OkButton.scale = Math.Max(0.75f, this.OkButton.scale);
            }
        }

        /// <summary>The method invoked when the player right-clicks on the lookup UI.</summary>
        /// <param name="x">The X-position of the cursor.</param>
        /// <param name="y">The Y-position of the cursor.</param>
        /// <param name="playSound">Whether to enable sound.</param>
        public override void receiveRightClick(int x, int y, bool playSound = true) { }

        /// <summary>The method invoked when the player hovers the cursor over the menu.</summary>
        /// <param name="x">The X-position of the cursor.</param>
        /// <param name="y">The Y-position of the cursor.</param>
        public override void performHoverAction(int x, int y)
        {
            this.OkButton.scale = this.OkButton.containsPoint(x, y)
                ? Math.Min(this.OkButton.scale + 0.02f, this.OkButton.baseScale + 0.1f)
                : Math.Max(this.OkButton.scale - 0.02f, this.OkButton.baseScale);
        }

        /// <summary>Draw the menu to the screen.</summary>
        /// <param name="b">The sprite batch.</param>
        public override void draw(SpriteBatch b)
        {
            // draw menu box
            Game1.drawDialogueBox(this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height, false, true);

            // draw OK button
            this.OkButton.draw(b);
            this.OkButton.draw(b, Color.Black * 0.5f, 0.97f);

            // draw cursor
            this.drawMouse(b);
        }

        public override bool readyToClose()
        {
            return this.alllFinished;
        }

        public override void update(GameTime time)
        {
        }
    }
}
