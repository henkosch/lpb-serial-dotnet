using System;
using System.Collections.Generic;

namespace LpbSerialDotnet.Protocol
{
    public class CommandType
    {
        public uint       CommandId { get; set; }         // the command or fieldID
        public ValueType  ValueType { get; set; }        // the message type
        public string     Description { get; set; }        // description

        public class CommandList : Dictionary<uint, CommandType>
        {
            public void Add(
                uint cmd,
                ValueType type,
                string desc)
            {
                Add(cmd, new CommandType
                {
                    CommandId = cmd,
                    ValueType = type,
                    Description = desc
                });
            }
        }

        public static CommandType FromId(uint commandId)
        {
            if (CommandType.Commands.TryGetValue(commandId, out var command))
            {
                return command;
            }
            else
            {
                return null;
            }
        }

        public static readonly CommandList Commands = new CommandList {
            { 0x0500021fu, ValueType.VT_TEMP, "Outside temperature" },
            { 0x0500021du, ValueType.VT_TEMP, "Heater temperature" },
            { 0x1500020au, ValueType.VT_UNKNOWN, "None" } 
        };
    }
}
