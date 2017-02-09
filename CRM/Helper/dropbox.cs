using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DropboxRestAPI;
using CRMHub.Helper;

namespace CRM.Helper
{
    public class dropbox
    {
        public List<Content> getDropboxContent(string ClientId, string ClientSecret, string AccessToken)
        {
            List<Content> lstContent = new List<Content>();
            try
            {
                var options = new Options
                {
                    ClientId = ClientId, //get from dropbox app console
                    ClientSecret = ClientSecret, //get from dropbox app console
                    AccessToken = AccessToken,  //get from dropbox app console
                    RedirectUri = "https://api.dropbox.com/1/oauth/request_token"
                };

                // Initialize a new Client (without an AccessToken)
                var client = new Client(options);
                // Get the OAuth Request Url
                var authRequestUrl = client.Core.OAuth2.Authorize("code");

                // TODO: Navigate to authRequestUrl using the browser, and retrieve the Authorization Code from the response
                var authCode = "...";

                // Exchange the Authorization Code with Access/Refresh tokens
                var token = client.Core.OAuth2.TokenAsync(authCode);

                // Get root folder without content
                var rootFolder = client.Core.Metadata.MetadataAsync("/", list: false);
                //Console.WriteLine("Root Folder: {0} (Id: {1})", rootFolder.Result.Name, rootFolder.Result.path);

                // Get root folder with content
                rootFolder = client.Core.Metadata.MetadataAsync("/", list: true);
                foreach (var folder in rootFolder.Result.contents)
                {
                    //Console.WriteLine(" -> {0}: {1} (Id: {2})",
                    //    folder.is_dir ? "Folder" : "File", folder.Name, folder.path);
                    if (folder.is_dir)
                    {
                        Content content = new Content();
                        content.bytes = folder.bytes;
                        content.client_mtime = folder.client_mtime;
                        content.contents = Convert.ToString(folder.contents);
                        content.Extension = folder.Extension;
                        content.hash = folder.hash;
                        content.icon = folder.icon;
                        content.is_deleted = Convert.ToString(folder.is_deleted);
                        content.is_dir = Convert.ToString(folder.is_dir);
                        content.mime_type = folder.mime_type;
                        content.modified = folder.modified;
                        content.modifier = Convert.ToString(folder.modifier);
                        content.Name = folder.Name;
                        content.parent_shared_folder_id = folder.parent_shared_folder_id;
                        content.path = folder.path;
                        content.photo_info = Convert.ToString(folder.photo_info);
                        content.read_only = Convert.ToString(folder.read_only);
                        content.rev = folder.rev;
                        content.revision = Convert.ToString(folder.revision);
                        content.root = folder.root;
                        content.shared_folder = Convert.ToString(folder.shared_folder);
                        content.size = folder.size;
                        content.thumb_exists = Convert.ToString(folder.thumb_exists);
                        content.video_info = Convert.ToString(folder.video_info);
                        lstContent.Add(content);
                    }
                }

                // Find a file in the root folder
                var file = rootFolder.Result.contents.FirstOrDefault(x => x.is_dir == false);

                // Download a file
                var tempFile = @"D:\" + file.Name;
                using (var fileStream = System.IO.File.OpenWrite(tempFile))
                {
                    client.Core.Metadata.FilesAsync(file.path, fileStream);
                }
            }
            catch (Exception ex)
            {

            }
            return lstContent;
        }
    }
}