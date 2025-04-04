﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingHotel.Model
{
    internal class Tourist
    {
        public int OrderId;
        public int TouristCount;
        public int OrderedDays;

        private static int _nextId = 1;

        public Tourist(int touristCount, int orderedDays) 
        {
            OrderId = _nextId++;
            TouristCount = touristCount;
            OrderedDays = orderedDays;
        }
    }
}
