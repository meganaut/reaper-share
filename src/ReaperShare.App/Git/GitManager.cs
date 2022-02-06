using LibGit2Sharp;

internal class GitManager : IDisposable
{
    private readonly Repository? _repo;

    public GitManager(string path)
    {
        try
        {
            _repo = new Repository(path);
        }
        catch (Exception ex)
        {
            _repo = null;
            throw;
        }
    }

    public bool DirectoryIsGitRepo()
    {
        return _repo != null;
    }

    public Status GetStatus()
    {
        var status = _repo.RetrieveStatus();
        return new Status(
            HasChanges: status.IsDirty,
            HasUpstream: _repo.Head.IsRemote);

        //TODO check we have an upstream. set one up if the repo is configured
    }

    public void Update(Config config)
    {
        var remote = _repo.Network.Remotes["origin"];
        var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
        Commands.Fetch(_repo, remote.Name, refSpecs, null, null);


    }

    public void Dispose()
    {
        _repo?.Dispose();
    }

    public record Status(bool HasChanges, bool HasUpstream);
}
