//
//  IGameTile.cs
//  Interface class for the GameTile class
//
//  Created by Steven F. Daniel on 04/01/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using UIKit;

namespace MatchTheTiles.iOS.Interfaces
{
    public interface IGameTile
    {
        // Instance method to load our GameBoard Tiles into an array
        void LoadGameBoardTiles();

        // Instance method to create a circle from an ImageView
        UIImageView CreateCircle(float tileWidth, float tileHeight);
    }
}
