using Google.Apis.Drive.v3.Data;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.GoogleDriveService
{
    internal class GoogleUtility
    {
        DriveService driveService;
        private GoogleService _googleService;

        public GoogleUtility(IConfiguration configuration)
        {
            _googleService = new GoogleService(configuration);
            driveService = _googleService.GetService();
        }


        Permission permission = new Permission()
        {
            Type = "anyone",
            Role = "Reader"
        };
        public string CheckIfFolderExists(string folderName, string parentFolderId)
        {
            var FileList = driveService.Files.List();
            FileList.Q = "mimeType='application/vnd.google-apps.folder' and trashed=false and name='" + folderName + "'";

            if (!string.IsNullOrEmpty(parentFolderId))
            {
                FileList.Q += $" and '{parentFolderId}' in parents";
            }

            var fileList = FileList.Execute();
            if (fileList.Files.Count > 0)
            {
                var existingFolder = fileList.Files.First();
                return existingFolder.Id;
            }

            return string.Empty;
        }
        public string CreateFolder(string folderName, string ParentId)
        {
            if (ParentId.Equals(string.Empty))
            {
                var Folder = new Google.Apis.Drive.v3.Data.File();
                Folder.Name = folderName;
                Folder.MimeType = "application/vnd.google-apps.folder";
                var Request = driveService.Files.Create(Folder);
                Request.Fields = "id";
                var File = Request.Execute();
                driveService.Permissions.Create(permission, File.Id).Execute();
                return File.Id;
            }
            else
            {
                var Folder = new Google.Apis.Drive.v3.Data.File();
                Folder.Name = folderName;
                Folder.MimeType = "application/vnd.google-apps.folder";
                var Request = driveService.Files.Create(Folder);
                Folder.Parents = new[] { ParentId };
                Request.Fields = "id";
                var File = Request.Execute();
                driveService.Permissions.Create(permission, File.Id).Execute();
                return File.Id;
            }

        }

        public string CreateFile(Stream myFile, string FileName, string ContentType, string folderId)
        {
            //var driveService = _googleService.GetService();
            var driveFile = new Google.Apis.Drive.v3.Data.File();
            driveFile.Name = FileName;
            driveFile.MimeType = ContentType;
            driveFile.Parents = new string[] { folderId };
            var request = driveService.Files.Create(driveFile, myFile, ContentType);
            request.Fields = "id";
            var response = request.Upload();
            if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
                throw response.Exception;
            return request.ResponseBody.Id;

        }

        public string GetFolderId(string MEMBER_NO_AND_MEMBER_NAME)
        {
            string parentFolderId = string.Empty;
            string folderId = string.Empty;

            parentFolderId = CheckIfFolderExists("NoboNil", string.Empty);
            if (parentFolderId.Equals(string.Empty))
            {
                parentFolderId = CreateFolder("NoboNil", string.Empty);
            }
            folderId = CheckIfFolderExists(MEMBER_NO_AND_MEMBER_NAME, parentFolderId);
            if (folderId.Equals(string.Empty))
            {
                folderId = CreateFolder(MEMBER_NO_AND_MEMBER_NAME, parentFolderId);
            }

            return folderId;
        }

        public void MoveFile(string fileId, string newFolderId)
        {

            var file = driveService.Files.Get(fileId).Execute();
            var updateRequest = driveService.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
            updateRequest.Fields = "parents";
            updateRequest.AddParents = newFolderId;
            updateRequest.Execute();
        }

        public string GetFilesByte(string FileId)
        {
            var request = driveService.Files.Get(FileId);
            var streamDownload = new MemoryStream();
            request.Download(streamDownload);
            byte[] fileBytes = streamDownload.ToArray();
            string base64String = Convert.ToBase64String(fileBytes);
            return base64String;
        }
    }
}
