using System;

namespace ShellLink.DLTW.DataObjects
{
    public class DomainRelativeObjId
    {
        private CDomainRelativeObjId inner;
        public Guid VolumeId
        {
            get => this.inner._volume._volume;
            set { this.inner._volume._volume = value; }
        }
        public Guid ObjectId
        {
            get => this.inner._object._object;
            set { this.inner._object._object = value; }
        }
    }
}