using CrossCutting.Core.Logging;
using CrossCutting.MainModule.IOC;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using Undead.Music.Application;
using Undead.Music.Entities;

namespace Undead.Music.Scanner
{
    class Program
    {
        private static ILogService _logService;

        static void Main(string[] args)
        {
            var container = new IocUnityContainer();
            _logService = container.Resolve<ILogService>();

            if (ConfigurationManager.AppSettings["MusicFolderPath"] == null)
            {
                ConsoleLog("Error:: The required MusicFolderPath configuration key is missing");
                return;
            }

            var songService = container.Resolve<ISongService>();

            var folderPath = ConfigurationManager.AppSettings["MusicFolderPath"];

            int totalFiles = 0;
            int errorCount = 0;
            ConsoleLog(string.Format("Scan started: {0}", DateTime.Now.ToString("hh\\:mm\\:ss")));
            DateTime scanTime = DateTime.Now;

            if (Directory.Exists(folderPath))
            {
                ConsoleLog("Computing hash of files...");
                StringDictionary filesHash = ComputeFolderHash(folderPath);
                ConsoleLog("Processing files...");
                var filePaths = Directory.GetFiles(folderPath, "*.mp3");
                foreach (var filePath in filePaths)
                {
                    try
                    {
                        var fileName = Path.GetFileName(filePath).ToLower(CultureInfo.InvariantCulture);
                        var fileHash = filesHash[fileName];
                        if (!songService.SongExistsByHash(fileHash))
                        {
                            var musicTrack = Id3Reader.Instance.GetMusicTrackFromId3(filePath);
                            if (musicTrack == null)
                            {
                                continue;
                            }

                            var song = SongFromMusicTrack(musicTrack, fileHash, fileName);

                            songService.AddSong(song);
                            ConsoleLog(string.Format("{0} : added to the database", fileName));
                        }
                        else
                        {
                            ConsoleLog(string.Format("{0} : already exists in database", fileName));
                        }
                    }
                    catch (Exception ex)
                    {
                        var innerException = ex.InnerException != null ? "InnerException::" + ex.InnerException.Message : "";
                        ConsoleLog(string.Format("ERROR:: {0} : {1}", filePath, ex.Message + "StackTrace::" + ex.StackTrace + innerException));
                        errorCount += 1;
                    }

                    totalFiles += 1;
                }

            }

            ConsoleLog(string.Format("Scan finished. Total time: {0}. Total {1} files scanned. Total Errors {2} (see application file log for more details)", DateTime.Now.Subtract(scanTime).ToString("hh\\:mm\\:ss"), totalFiles, errorCount));
        }

        private static StringDictionary ComputeFolderHash(string folderPath)
        {
            var dir = new DirectoryInfo(folderPath);
            var files = dir.GetFiles("*.mp3");
            var filesHash = new StringDictionary();
            var currentFileIndex = 1;

            foreach (FileInfo info in files)
            {
                ConsoleLog(string.Format("Compute hash for file {0} ({1} of {2})", info.Name, currentFileIndex, files.Length));
                filesHash.Add(info.Name, Sha2Calculator.ComputeFileHash(info));
                currentFileIndex += 1;
            }

            return filesHash;
        }

        private static Song SongFromMusicTrack(MusicTrack musicTrack, string fileHash, string fileName)
        {
            var song = new Song
            {
                Artist = musicTrack.Artist != null ? musicTrack.Artist : "Unknown",
                Album = musicTrack.Album != null ? musicTrack.Album : "Unknown",
                Title = musicTrack.Title != null ? musicTrack.Title : "Unknown",
                Artwork = musicTrack.Artwork,
                Bitrate = musicTrack.Bitrate,
                Duration = musicTrack.Duration,
                Genre = musicTrack.Genre,
                Year = musicTrack.Year,
                Hash = fileHash,
                FileName = fileName
            };

            return song;
        }

        private static void ConsoleLog(string msg)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy.dd.MM-hh:mm:ss") + " - " + msg);
            _logService.Info(msg);
        }
    }
}