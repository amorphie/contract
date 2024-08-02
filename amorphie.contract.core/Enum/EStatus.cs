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
        Rejected = 7,

        //Before document validation flow
        TemporarilyApproved = 8,
        Original = 9
    }
}