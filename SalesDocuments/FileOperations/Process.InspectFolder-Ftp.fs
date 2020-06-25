namespace IntegratingPartyContext.SalesDocuments.FileOperations

module Process = 

    open nsoftware.IPWorks
    open System.Collections.Generic
    open IntegratingPartyContext.SalesDocuments.FileOperations
    open System

    let InspectFolder (ftp: Ftp, folder: string) : List<ContainerAvailable> =
        
        let mutable containers = new List<ContainerAvailable>()

        (FtpClient.ListFiles (ftp, folder)).ForEach (fun file -> containers.Add ({ File = file; ContainerIdentifiedDatetime = DateTime.Now }))
        containers