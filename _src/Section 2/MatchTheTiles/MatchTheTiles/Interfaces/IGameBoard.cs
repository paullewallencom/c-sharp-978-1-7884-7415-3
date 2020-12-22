//
//  IGameBoard.cs
//  Interface class for the GameBoard class
//
//  Created by Steven F. Daniel on 04/01/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
namespace MatchTheTiles.iOS.Interfaces
{
    public interface IGameBoard
    {
        // Instance method to create our Game Board and Game Tiles
        void CreateGameBoard();

        // Instance method to randomly shuffle our game tiles
        void ShuffleBoardTiles();
    }
}