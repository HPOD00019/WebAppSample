using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineService.Domain.Chess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace GameEngineService.Domain.Chess
    {
        public interface ITimeManager
        {
            public TimeSpan WhiteTime { get; }
            public TimeSpan BlackTime { get; }
            public TimeSpan Increment { get; }
            public TimeSpan OnWhiteMove();
            public TimeSpan OnBlackMove();
            public void OnGameStart();
        }
    }

}
