﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Tokens;

namespace SNAPME.Services.Interfaces
{
    public interface IProductService
    {
        ProductToken GetById(string id);
    }
}
