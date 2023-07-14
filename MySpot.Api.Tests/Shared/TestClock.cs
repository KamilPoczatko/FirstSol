using MySpot.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Api.Tests.Shared
{
    internal class TestClock : IClock
    {
        public DateTime Current() => new DateTime(22, 08, 11);
    }
}
