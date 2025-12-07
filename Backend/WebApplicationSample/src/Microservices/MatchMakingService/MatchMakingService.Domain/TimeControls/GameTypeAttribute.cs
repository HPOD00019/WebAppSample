using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakingService.Domain.TimeControls
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GameTypeAttribute : Attribute
    {
        public string Type { get; }
        public GameTypeAttribute(string type)
        {
            Type = type;
        }
    }

    public static class TimeControlExtensions
    {
        public static string GetGameType(this TimeControl timeControl)
        {
            var field = timeControl.GetType().GetField(timeControl.ToString());
            var attribute = field?.GetCustomAttribute<GameTypeAttribute>();
            return attribute?.Type ?? throw new Exception("game attribute type unknown!");
        }
    }
}
