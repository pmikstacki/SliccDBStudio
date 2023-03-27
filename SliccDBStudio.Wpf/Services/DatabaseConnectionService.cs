using System.Collections.Immutable;
using CommunityToolkit.Maui.Storage;
using SliccDB.Fluent;
using SliccDB.Serialization;
using VisNetwork.Blazor.Models;

namespace SliccDBStudio.Services;

public class DatabaseConnectionService
{
    private DatabaseConnection _databaseConnection;
    public DatabaseConnection DatabaseConnection => _databaseConnection;
    public bool IsDatabaseOpen => _databaseConnection != null;
    private readonly GraphDisplayService _graphDisplayService;
    public Action OnDatabaseStateChanged;
    public DatabaseConnectionService(GraphDisplayService graphDisplayService)
    {
        _graphDisplayService = graphDisplayService;
    }

    public async Task OpenDatabaseAsync()
    {
        var result = await FilePicker.Default.PickAsync(new PickOptions()
        {
            FileTypes = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>()
                {
                    { DevicePlatform.WinUI, new[] { "*.sliccdb" }}
                })
        });
        if (result != null)
        {
            _databaseConnection = new DatabaseConnection(result.FullPath, true);
            OnDatabaseStateChanged?.Invoke();

        }

    }

    public async Task CreateDatabaseAsync()
    {
        var result = await FileSaver.Default.SaveAsync("database.sliccdb", new MemoryStream(new byte[1]), CancellationToken.None);
        if (string.IsNullOrEmpty(result))
        {
            _databaseConnection = new DatabaseConnection(result, true);
            OnDatabaseStateChanged?.Invoke();
        }

    }

    public NetworkData GetDatabaseEntities()
    {
        return _graphDisplayService.GetGraphResult(_databaseConnection.Entities());
    }
}