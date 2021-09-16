using System;

namespace ShellLink.DLTW.DataObjects
{
    public class DomainRelativeObjId
    {
        CDomainRelativeObjId inner;
        public Guid VolumeId
        {
            get => inner._volume._volume;
            set { inner._volume._volume = value; }
        }
        public Guid ObjectId
        {
            get => inner._object._object;
            set { inner._object._object = value; }
        }
    }
}