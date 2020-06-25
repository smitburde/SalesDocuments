namespace IntegratingPartyContext.SalesDocuments.FileOperations

open System

     type FileInfo = 
        {
            Path: string
            Name: string
            LastModifiedDatetime: DateTime
            Size: int64
        }
