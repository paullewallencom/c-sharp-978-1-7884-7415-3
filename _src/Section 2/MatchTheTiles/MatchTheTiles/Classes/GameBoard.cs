//
//  GameBoard.cs
//  Sets up our Game Board and Game Tiles and randomly shuffles our Game Tile 
//  images within our Game Board.
//
//  Created by Steven F. Daniel on 04/01/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Collections.Generic;
using CoreGraphics;
using MatchTheTiles.Classes;
using MatchTheTiles.iOS.Interfaces;
using UIKit;

namespace MatchTheTiles.iOS.Classes
{
    public class GameBoard : IGameBoard
    {
        // Define the properties that will be used by our class
        public float BoardWidth { get; set; }
        public UIView GameBoardView { get; set; }
        public List<UIImageView> TilesImagesArray { get; set; }

        // Variables for Board Grid Size and Game View Width
        int boardGridSize;
        float gameViewWidth;

        GameTile[,] tiles = new GameTile[4, 4];
        List<CGPoint> GameTileCoords { get; } = new List<CGPoint>();

        // GameBoard Class Constructor
        public GameBoard(int boardGridSize, float gameViewWidth)
        {
            this.boardGridSize = boardGridSize;
            this.gameViewWidth = gameViewWidth;
        }

        // Instance method to create our Game Board and Game Tiles
        public void CreateGameBoard()
        {
            // TODO: Create Game Board Implementation
        }

        // Instance method to randomly shuffle our game tiles
        public void ShuffleBoardTiles()
        {
            // TODO: Shuffle our Game Board Tiles Implementation
        }
    }
}
