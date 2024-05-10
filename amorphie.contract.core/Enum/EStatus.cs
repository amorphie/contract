namespace amorphie.contract.core.Enum
{
    public enum ApprovalStatus : ushort
    {
        InProgress = 2,
        Approved = 3,
        OnHold = 4,
        Canceled = 5,

        //Signed but now it has new version.
        HasNewVersion = 6,
        Rejected = 7
    }

    public enum DefinitionStatus : ushort
    {
        Completed = 3,
        OnHold = 4,
        Canceled = 5,
        Rejected = 7
    }

}