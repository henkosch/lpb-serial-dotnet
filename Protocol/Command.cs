using System;
using System.Collections.Generic;

namespace LpbSerialDotnet.Protocol
{
    public class Command
    {
        public uint            Cmd { get; set; }         // the command or fieldID
        public CommandCategory Category { get; set; }    // the menu category
        public ValueType       Type { get; set; }        // the message type
        public ushort          Line { get; set; }        // parameter number
        public string          Desc { get; set; }        // description test

        public class CommandList : Dictionary<uint, Command>
        {
            public void Add(
                uint cmd,
                CommandCategory category,
                ValueType type,
                ushort line,
                string desc)
            {
                Add(cmd, new Command
                {
                    Cmd = cmd,
                    Category = category,
                    Type = type,
                    Line = line,
                    Desc = desc
                });
            }
        }

        public static readonly CommandList Commands = new CommandList {
            { 0x0500021Fu, CommandCategory.CAT_DIAG_VERBRAUCHER, ValueType.VT_TEMP, 8700, "Outside temperature" },
            { 0x0500021du, CommandCategory.CAT_DIAG_VERBRAUCHER, ValueType.VT_TEMP, 8700, "Heater temperature" }
        };
    }
}
