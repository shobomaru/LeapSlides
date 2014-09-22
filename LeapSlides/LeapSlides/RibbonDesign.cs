// Copyright (c) 2014 shobomaru
// This software is released under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace LeapSlides
{
    public partial class RibbonDesign
    {
        private void RibbonDesign_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void toggleButtonOnOff_Click(object sender, RibbonControlEventArgs e)
        {
            Config._value.IsEnabled = toggleButtonOnOff.Checked;
        }
    }
}
