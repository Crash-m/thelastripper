//
//  
//  TheLastRipper
//
//  Created by Rene Josefsen on 11/08/07.
//  Copyright 2007 __MyCompanyName__. All rights reserved.
//
//

using System;
using System.Collections.Generic;
using System.Text;
using Cocoa;

namespace TheLastRipper
{
	class TheLastRipper
	{
        static void Main(string[] args) 
        {
            TheLastRipper main = new TheLastRipper();
            main.Run();
        }

        public void Run() 
        {
            Application.Init();
            Application.LoadNib("Main.nib");
            Application.Run();
        }
    }
}
