namespace IntegratingPartyContext.SalesDocuments.FileOperations

module FtpClient =

    open nsoftware.IPWorks
    open System
    open IntegratingPartyContext.SalesDocuments.FileOperations
    open System.Collections.Generic

    let nsoftwareRuntimeKey = "31504E39414131535542524131535542324543453436303300000000000000000000000000000000483232563536554B000032304B4636573937475544520000"

    let Connect (host: string, username: string, password: string) : Ftp = 
        let ftp = new Ftp (nsoftwareRuntimeKey)
        ftp.RemoteHost <- host
        ftp.User <- username
        ftp.Password <- password
        ftp
    
    let Disconnect(ftp: Ftp) =
        ftp.Logoff()
    
    let DownloadFile (ftp: Ftp, remoteFile: FileInfo, localFile: string) : unit =
        ftp.TransferMode <- FtpTransferModes.tmBinary
        ftp.RemoteFile <- sprintf "%s/%s" remoteFile.Path remoteFile.Name
        ftp.LocalFile <- localFile
        ftp.Download ()

    let rec ListFiles (ftp: Ftp, folder: string) : List<FileInfo> = 
        ftp.RemotePath <- folder
        ftp.ListDirectoryLong ()

        let dirEntries = ftp.DirList |> List
        
        for dir in folder.Split ('/') do
            if not (String.IsNullOrEmpty(dir.Trim ())) then
               ftp.RemotePath <- ".."

        let files = new List<FileInfo>()

        for dirEntry in dirEntries do
            if dirEntry.IsDir then 
                printf ""
                files.AddRange (ListFiles (ftp, sprintf "%s/%s" folder dirEntry.FileName))
            else
                files.Add({
                    Path = folder 
                    Name = dirEntry.FileName
                    LastModifiedDatetime = RegularExpressions.DatetimeParse dirEntry.FileTime
                    Size = dirEntry.FileSize
                })
        files