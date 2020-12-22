//
//  GameTile.cs
//  Creates each of our tile images and loads our game images into an array.
//
//  Created by Steven F. Daniel on 04/01/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Collections.Generic;
using MatchTheTiles.iOS.Classes;
using MatchTheTiles.iOS.Interfaces;
using UIKit;

namespace MatchTheTiles.Classes
{
    public class GameTile : UIImageView, IGameTile
    {
        // Constant variable for total number of GAME IMAGES
        const int MAX_GAME_IMAGES = 9;

        // GameTile Class Constructor
        public GameTile()
        {
        }

        // Overload GameTile Class Constructor
        public GameTile(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        // Instance method to load our game tiles
        public void LoadGameBoardTiles()
        {
            for (int imageNo = 1; imageNo <= MAX_GAME_IMAGES; imageNo++)
            {
                var boardImage = new UIImageView(new UIImage("img_" + imageNo + ".png"));
                GameBoardObjects.Add(boardImage);
            }
        }

        // Instance method to create a circle from an ImageView
        public UIImageView CreateCircle(float tileWidth, float tileHeight)
        {
            // Instantiate an instance of our ImageCircle class
            var tileImage = new ImageCircle(tileWidth, tileHeight);
            tileImage.ImageName = this.Image;
            tileImage.ImageCenter = this.Center;
            return tileImage.CreateImageCircle();
        }

        // Define the properties that will be used by our class
        public int Row { private set; get; }
        public int Col { private set; get; }

        // Property to hold our Memory Game Board Images
        public static List<UIImageView> GameBoardObjects { get; } = new List<UIImageView>();
    }
}