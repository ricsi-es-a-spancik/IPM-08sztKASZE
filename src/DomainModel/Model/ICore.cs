﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model
{
    public interface ICore
    {
        void register(IGameApp game, String GameName);
    }
}
