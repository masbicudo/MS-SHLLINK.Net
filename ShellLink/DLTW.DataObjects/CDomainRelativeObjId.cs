namespace ShellLink.DLTW.DataObjects
{
    /// <summary>
    /// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-dltw/80cbf3f6-964d-456a-b08e-6f20c7c86921">
    /// 2.2.3 CDomainRelativeObjId
    /// </seealso>
    /// </summary>
    public struct CDomainRelativeObjId
    {
        public CVolumeId _volume;
        public CObjId _object;
    }
}