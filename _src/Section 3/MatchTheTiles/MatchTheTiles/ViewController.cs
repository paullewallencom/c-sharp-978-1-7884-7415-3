//
//  ViewController.cs
//  Main game logic for the Match The Tiles Memory game
//
//  Created by Steven F. Daniel on 04/01/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using MatchTheTiles.Classes;
using MatchTheTiles.iOS.Classes;
using UIKit;

namespace MatchTheTiles
{
    public partial class ViewController : UIViewController
    {
        #region 1 - Declare our game variables for our game
        float gameViewWidth;
        int gridCellSize = 4;
        int gameBoardSize = 0;
        int gameTimerCounter = 60;

        // Declare our variables for handling image comparisions, etc
        bool IsComparing = false;
        bool selAllowed = true;
        int indexOfFirstTile;
        int indexOfSecondTile;
        int totalFound = 0;
        int currentScore = 0;

        // Declare game Timer, Game Tile Backgrounds, etc
        NSTimer gameTimer;
        UIImageView firstTileImage;
        UIImageView secondTileImage;

        // Declare our variables for our game Images and Tile Indexes arrays
        List<UIImageView> gameTileImagesArray = new List<UIImageView>();
        #endregion

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }

        #region 2 - Layout our Game Board and load our game tiles
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Make sure Game View is Laid Out
            gameBoardView.LayoutIfNeeded();
            gameViewWidth = (float)gameBoardView.Frame.Size.Width;

            GameTile tile = new GameTile();
            tile.LoadGameBoardTiles();

            // Call our instance method to start a new game
            resetGameVariables();
            startNewGame();
        }
        #endregion

        #region 3 - Handling touch events within the Game Board
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            // Get the touch that was activated within the view
            UITouch myTouch = (UITouch)touches.AnyObject;

            if (gameTileImagesArray.Contains(myTouch.View) && selAllowed)
            {
                // Get the image at the position within the view
                UIImageView gameBoardTile = (UIImageView)myTouch.View;

                // Get the index of the image within our array
                int tileIndex = gameTileImagesArray.IndexOf(gameBoardTile);
                int imageIndex = (tileIndex < gameBoardSize ? tileIndex :
                                  tileIndex - gameBoardSize);

                // call our instance method to handle flipping of the chosen tiles
                flipGameBoardTiles(gameBoardTile, tileIndex, imageIndex);
            }
        }
        #endregion

        #region 4 - Instance method to flip our game tiles within our Game Board
        void flipGameBoardTiles(UIImageView gameBoardTile, int tileIndex, int imageIndex)
        {
            UIView.Transition(gameBoardTile, .5,
                              UIViewAnimationOptions.TransitionFlipFromRight,
           () => // animation begin
           {
               // Get the image from our array that was chosen
               gameBoardTile.Image = GameTile.GameBoardObjects[imageIndex].Image;
           }, () => // animation completion
           {
               // Check to see if we are performing a comparison
               if (IsComparing)
               {
                   // Get the index of the second tile that was selected
                   indexOfSecondTile = tileIndex;
                   secondTileImage = gameBoardTile;
                   doGameTilesMatch();

                   // If we have matched all tiles within our Game Board
                   if (totalFound == gameBoardSize)
                   {
                       gameTimer.Invalidate();
                       startNewGame();
                   }
                   IsComparing = false;
               }
               else
               {
                   // Get the index of the first tile that was selected
                   indexOfFirstTile = tileIndex;
                   firstTileImage = gameBoardTile;
                   IsComparing = true;
               }
           });
        }
        #endregion

        #region 5 - Instance method to check to see if our Game Tiles match
        void doGameTilesMatch()
        {
            int chosenImageIndex = Math.Abs(indexOfFirstTile - indexOfSecondTile);

            if (chosenImageIndex == gameBoardSize)
            {
                // Remove the matched tiles from the views superview
                firstTileImage.RemoveFromSuperview();
                secondTileImage.RemoveFromSuperview();

                // If we have matched a tile, increment our total count found
                totalFound++;
                currentScore += 10;
                scoreLabel.Text = "Score: " + currentScore.ToString();
            }
            else
            {
                // If we have matched an incorrect tile
                firstTileImage.Image = UIImage.FromFile("front_tile.png");
                secondTileImage.Image = UIImage.FromFile("front_tile.png");
            }
        }
        #endregion

        #region 6 - Instance method to end the current game and start a new game
        void startNewGame()
        {
            // Remove reminants of our ImageViews from our GameBoard
            foreach (UIView any in gameBoardView.Subviews)
            {
                any.RemoveFromSuperview();
            }

            // Clear out our game Tile Images array
            gameTileImagesArray.Clear();
            gridCellSize = 4;
            gameBoardSize = (gridCellSize * gridCellSize) / 2;
            selAllowed = true;
            IsComparing = false;

            // Instantiate a new instance of our GameBoard class
            var gameBoard = new GameBoard(gridCellSize, gameViewWidth);

            // Pass in values for each of the properties
            gameBoard.GameBoardView = gameBoardView;
            gameBoard.TilesImagesArray = gameTileImagesArray;
            gameBoard.CreateGameBoard();
            gameBoard.ShuffleBoardTiles();

            setupGameTimer();
        }
        #endregion

        #region 7 - Instance method to reset our Game Timer, Score and Time
        void resetGameVariables()
        {
            gameTimerCounter = 60;
            scoreLabel.Text = "Score: 0";
            timeLabel.Text = "Time: 0";
        }
        #endregion

        #region 8 - Instance method that sets up our game timer
        void setupGameTimer()
        {
            // Create a game Timer Countdown that repeats the count every second
            gameTimer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(1.0),
                                                               delegate
                                                               {
                                                                   startTimerCountDown();
                                                               });
        }
        #endregion

        #region 9 - Instance method that starts our game countdown timer
        void startTimerCountDown()
        {
            // Decrement our seconds counter, then update our countDown Label
            gameTimerCounter--;
            String timeString = "Time: " + gameTimerCounter + " sec's";
            timeLabel.Text = timeString;

            // Check to see if our countdown timer has reached zero.
            if (gameTimerCounter == 0)
            {
                // Stop our timer control from going negative
                gameTimer.Invalidate();

                // Set up our UIAlertViewController as well as the Action methods
                UIApplication.SharedApplication.InvokeOnMainThread(new Action(() =>
                            {
                                var alert = UIAlertController.Create("Times Up!",
                                                                     "Your time is up! You got a score of " + currentScore + " points.",
                                                                     UIAlertControllerStyle.Alert);
                                // set up button event handlers
                                alert.AddAction(UIAlertAction.Create("Play Again?",
                                                        UIAlertActionStyle.Default, a =>
                                                        {
                                                            resetGameVariables();
                                                            startNewGame();
                                                        }));
                                alert.AddAction(UIAlertAction.Create("Cancel",
                                                                     UIAlertActionStyle.Default,
                                                                     null));

                                // Display the UIAlertView to the current view
                                this.ShowViewController(alert, this);
                            }));
            }
        }
        #endregion

        #region 10 - Instance method that will reset the current game in progress
        partial void ResetGame_Clicked(UIButton sender)
        {
            // Set up our UIAlertViewController as well as the Action methods
            UIApplication.SharedApplication.InvokeOnMainThread(new Action(() =>
            {
                var alert = UIAlertController.Create("Reset Game", "Are you sure you want to start again?",
                                                     UIAlertControllerStyle.Alert);
                // set up button event handlers
                alert.AddAction(UIAlertAction.Create("OK",
                                                     UIAlertActionStyle.Default, a =>
                                                     {
                                                         gameTimer.Invalidate();
                                                         resetGameVariables();
                                                         startNewGame();
                                                     }));
                alert.AddAction(UIAlertAction.Create("Cancel",
                                                     UIAlertActionStyle.Default,
                                                     null));

                // Display the UIAlertView to the current view
                this.ShowViewController(alert, sender);
            }));
        }
        #endregion

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
