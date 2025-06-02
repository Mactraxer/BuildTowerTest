public struct DropResult
{
    public bool Success;
    public DropError DropError;

    public DropResult(bool success, DropError dropError = DropError.None)
    {
        Success = success;
        DropError = dropError;
    }
}

public enum DropError
{
    None,
    HeightLimit,
    MissToTower,
    MissFromTower,
}