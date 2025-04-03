using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingHotel.Model
{
    internal class Tourist
    {
        public int orderId;
        public int touristCount;
        public int orderedDays;

        private int _nextId = 1;
    }
}
