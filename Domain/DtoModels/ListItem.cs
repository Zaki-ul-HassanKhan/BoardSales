﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoModels
{
    public class ListItem<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
