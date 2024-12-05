using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;

public class Song
            {
                    public string Title { get; set; }
                    public string Album { get; set; }
                    public string Lyrics { get; set; }
            
        // Parameterless constructor for deserialization
    public Song() { }


    private readonly IHostEnvironment _env;
    public Song(IHostEnvironment env)
    {
        _env = env;
    }

    public string GetFilePath()
    {
        string filePath;

        if (_env.IsDevelopment())
        {
            // Use the current directory for development (localhost)
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "songs.json");
        }
        else
        {
            // Use D:/home for Azure (production)
            filePath = Path.Combine("D:", "home", "site", "songs.json");
        }

        return filePath;
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
