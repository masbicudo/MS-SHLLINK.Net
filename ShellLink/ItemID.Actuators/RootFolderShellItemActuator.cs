using ShellLink.Internals;
using ShellLink.Parameters;
using System;

namespace ShellLink.ItemID.Actuators
{
    public sealed class RootFolderShellItemActuator : ShellItemIdActuatorBase<RootFolderShellItem>,
        IActuator<RootFolderShellItem>
    {
        protected override bool CheckData(CheckParams<RootFolderShellItem> arguments)
        {
            if (arguments.item.TypeIndicator != ItemIDTypeIndicator.RootFolder)
            {
                arguments.errors.Add(new WrongTypeIndicatorException(
                        validTypeIndicator: ItemIDTypeIndicator.RootFolder,
                        wrongTypeIndicator: arguments.item.TypeIndicator
                    ));
            }
            return true;
        }

        protected override uint GetDataLength(LengthParams<RootFolderShellItem> arguments)
        {
            // https://github.com/libyal/libfwsi/blob/main/documentation/Windows%20Shell%20Item%20format.asciidoc#32-root-folder-shell-item
            var totalSize = 18u;
            totalSize += this.GetExtensionBlockLength(arguments);
            return totalSize;
        }
        protected uint GetExtensionBlockLength(LengthParams<RootFolderShellItem> arguments)
        {
            var extensionBlockActuator = arguments.options.Get<ExtensionBlock_0xbeef0017Actuator>();
            if (extensionBlockActuator == null)
                return 0;
            var extBlockLength = extensionBlockActuator.GetLength(new(arguments.item.ExtensionBlock, arguments.options));
            return extBlockLength;
        }

        protected override bool ReadData(ReadParams<RootFolderShellItem> arguments)
        {
            arguments.item.TypeIndicator = (ItemIDTypeIndicator)arguments.reader.ReadByte();
            arguments.item.SortIndex = arguments.reader.ReadByte();
            arguments.item.ShellFolderIdentifier = arguments.reader.ReadGuid();

            if (arguments.item.ItemIDSize <= 20)
                arguments.item.ExtensionBlock = null;
            else
                arguments.item.ExtensionBlock = new ExtensionBlock_0xbeef0017();

            if (arguments.item.ExtensionBlock != null)
            {
                var extBlockActuator = arguments.options.Get<IActuator<ExtensionBlock_0xbeef0017>>();
                if (!extBlockActuator.Read(new(arguments.item.ExtensionBlock, arguments.reader, arguments.options)))
                    return false;
            }

            return true;
        }

        protected override bool RepairData(RepairParams<RootFolderShellItem> arguments)
        {
            arguments.item.TypeIndicator = ItemIDTypeIndicator.RootFolder;
            return true;
        }

        protected override bool WriteData(WriteParams<RootFolderShellItem> arguments)
        {
            throw new NotImplementedException();
        }
    }
}