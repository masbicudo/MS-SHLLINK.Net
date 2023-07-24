using ShellLink.Internals;
using System;
using System.Collections.Generic;

namespace ShellLink.Parameters
{
    public struct CheckParams<TItem>
    {
        public TItem item;
        public List<Exception> errors;
        public IOptions options;

        public CheckParams(TItem item, List<Exception> errors, IOptions options)
        {
            this.item = item;
            this.errors = errors;
            this.options = options;
        }

        public CheckParams<TOtherItem> ConvertTo<TOtherItem>()
        {
            return new()
            {
                item = (TOtherItem)(object)this.item,
                errors = this.errors
            };
        }
    }
}