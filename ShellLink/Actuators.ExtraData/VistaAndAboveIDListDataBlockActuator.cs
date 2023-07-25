using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using ShellLink.ItemID.Actuators;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class VistaAndAboveIDListDataBlockActuator : ExtraDataBlockActuator<VistaAndAboveIDListDataBlock>
    {
        public override int MinBlockSize { get; } = 0x0000000A;
        public override int MaxBlockSize { get; } = int.MaxValue;
        public override int StandardBlockSignature { get; } = unchecked((int)0xA000000C);

        protected override void WriteDataTo(VistaAndAboveIDListDataBlock edb, BinaryWriter writer, IOptions options)
        {
            foreach (var itemId in edb.IDList.ItemIDList)
            {
                var actuator = options.GetActuatorForShellItemId(itemId.GetType());
                if (!actuator.Write(new(itemId, writer, options)))
                    return;
            }
        }

        protected override bool LoadData(VistaAndAboveIDListDataBlock edb, BinaryReader reader, IOptions options)
        {
            var provider = options.Get<ItemIDProvider>();
            while (true)
            {
                var itemid = provider.Read(reader);

                if (itemid == null)
                    break;

                edb.IDList.ItemIDList.Add(itemid);
            }
            return true;
        }

        protected override void CheckData(VistaAndAboveIDListDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
        }

        protected override void RepairData(VistaAndAboveIDListDataBlock edb)
        {
        }
    }
}