//
//  IImageCircle.cs
//  Interface class for the ImageCircle class
//
//  Created by Steven F. Daniel on 04/01/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using UIKit;

namespace MatchTheTiles.iOS.Interfaces
{
    public interface IImageCircle
    {
        // Instance method to convert an imageView into a Circle
        UIImageView CreateImageCircle();
    }
}
