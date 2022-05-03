namespace Tests.Api.Interfaces;

public interface IUsersInfoProvider
{
    public Task<IEnumerable<string>> GetStudentNames();
}