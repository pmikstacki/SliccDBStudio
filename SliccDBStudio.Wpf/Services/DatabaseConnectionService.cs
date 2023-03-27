using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
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

    public void OpenDatabase()
    {

        OpenFileDialog openFile = new OpenFileDialog();
        openFile.Filter = "SliccDB | *.sliccdb";
        if (openFile.ShowDialog() == true)
        {
            _databaseConnection = new DatabaseConnection(openFile.FileName, true);
            OnDatabaseStateChanged?.Invoke();
        }
    }

    public void CreateDatabase()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "SliccDB | *.sliccdb";
        if (saveFileDialog.ShowDialog() == true)
        {
            _databaseConnection = new DatabaseConnection(saveFileDialog.FileName, true);
            OnDatabaseStateChanged?.Invoke();
        }
    }

    public void SaveDatabase()
    {
       _databaseConnection.SaveDatabase();
    }

    public NetworkData GetDatabaseEntities()
    {
        return _graphDisplayService.GetGraphResult(_databaseConnection.Entities());
    }
}