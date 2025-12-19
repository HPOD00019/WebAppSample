using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineService.Domain.TimeControls

{
    public enum TimeControl
    {
        Minutes_3_increment_2_seconds,

        Minutes_5_increment_5_seconds,

        Minutes_5_increment_none,

        Minutes_10_inrement_none,

        Minutes_15_increment_10_seconds,

        Minutes_45_increment_none,

        Minutes_60_increment_30_seconds,
    }
    public static class TimeControlTransitions
    {
        public static bool TryParseTimeControl(TimeControl timeControl, out TimeSpan time, out TimeSpan increment)
        {
            time = TimeSpan.Zero;
            increment = TimeSpan.Zero;

            try
            {
                switch (timeControl)
                {
                    case TimeControl.Minutes_3_increment_2_seconds:
                        time = TimeSpan.FromMinutes(3);
                        increment = TimeSpan.FromSeconds(2);
                        break;

                    case TimeControl.Minutes_5_increment_5_seconds:
                        time = TimeSpan.FromMinutes(5);
                        increment = TimeSpan.FromSeconds(5);
                        break;

                    case TimeControl.Minutes_5_increment_none:
                        time = TimeSpan.FromMinutes(5);
                        increment = TimeSpan.Zero;
                        break;

                    case TimeControl.Minutes_10_inrement_none:
                        time = TimeSpan.FromMinutes(10);
                        increment = TimeSpan.Zero;
                        break;

                    case TimeControl.Minutes_15_increment_10_seconds:
                        time = TimeSpan.FromMinutes(15);
                        increment = TimeSpan.FromSeconds(10);
                        break;

                    case TimeControl.Minutes_45_increment_none:
                        time = TimeSpan.FromMinutes(45);
                        increment = TimeSpan.Zero;
                        break;

                    case TimeControl.Minutes_60_increment_30_seconds:
                        time = TimeSpan.FromMinutes(60);
                        increment = TimeSpan.FromSeconds(30);
                        break;

                    default:
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
