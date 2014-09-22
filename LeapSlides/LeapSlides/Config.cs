// Copyright (c) 2014 shobomaru
// This software is released under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeapSlides
{
    public class ConfigValue
    {
        public bool IsEnabled { get; set; }
        
        public ConfigValue()
        {
            IsEnabled = true;
        }
    }

    public static class Config
    {
        public static ConfigValue _value = new ConfigValue();
    }
}
