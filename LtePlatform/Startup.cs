﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(LtePlatform.Startup))]

namespace LtePlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
