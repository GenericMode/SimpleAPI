using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.Logging;

public class Song
            {
                    public string Title { get; set; }
                    public string Album { get; set; }
                    public string Lyrics { get; set; }
            
        // Parameterless constructor for deserialization
    public Song() { }


    private readonly IHostEnvironment _env;
    private readonly ILogger<Song> _logger;  // Declare the logger

    public Song(IHostEnvironment env, ILogger<Song> logger)
    {
        _env = env;
        _logger = logger;

    }

    public string GetFilePath()
    {
        string filePath;

        _logger.LogInformation($"ContentRootPath: {_env.ContentRootPath}");

        if (_env.IsDevelopment())
        {
            // Use the current directory for development (localhost)
             filePath = Path.Combine(_env.ContentRootPath, "songs.json");
        }
        else
        {
            // Use D:/home for Azure (production)
            filePath = ("D:/home/site/songs.json");
        }
        _logger.LogInformation($"File path used: {filePath}");
        return filePath;
    }
    public void EnsureSongsFileExists()
    {
        var filePath = GetFilePath();

        // Check if the file exists in the D:/home/ directory (Azure App Service)
        if (!System.IO.File.Exists(filePath)) and (!_env.IsDevelopment()) 
        {
            _logger.LogInformation($"File not found in {filePath}, copying from the application directory.");

            // Get the path to the deployed file (it should be copied during deployment)
            var deployedFilePath = Path.Combine(_env.ContentRootPath, "songs.json");

             _logger.LogInformation($"Deployed file path: {deployedFilePath}");

            // Copy the file from the application directory (project directory) to the Azure directory
            if (System.IO.File.Exists(deployedFilePath))
            {
                System.IO.File.Copy(deployedFilePath, filePath);
                _logger.LogInformation($"File copied successfully from {deployedFilePath} to {filePath}");
            }
            else
            {
                _logger.LogError($"Deployed songs.json file not found in {deployedFilePath}. Please ensure the file is copied correctly.");
            }
        }
    }
    public void SaveSongsToFile(List<Song> songs)
    {
        var filePath = GetFilePath();  // Get the correct file path based on the environment

        // Ensure the directory exists and create it if necessary
        var directoryPath = Path.GetDirectoryName(filePath);  // Get the directory path from file path

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);  // Create the directory if it doesn't exist
        }

        // Serialize the songs list to JSON and write to the file
        var json = JsonSerializer.Serialize(songs, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(filePath, json);
    }           

            }
