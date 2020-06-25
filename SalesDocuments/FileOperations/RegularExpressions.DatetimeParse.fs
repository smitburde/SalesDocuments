namespace IntegratingPartyContext.SalesDocuments.FileOperations

module RegularExpressions = 

    open System
    open System.Configuration
    open System.Reflection
    open System.Text

    let DatetimeParse (s: string) : DateTime = 
        let mutable s = s
        let couldParse, dt = DateTime.TryParse(s)

        if couldParse then
            dt
        else
            let configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location)

            let datetimeRegularExpressions = configuration.AppSettings.Settings.Item("DatetimeRegularExpressions").Value

            if  datetimeRegularExpressions = String.Empty then
                raise (new InvalidCastException(sprintf "Failed to parse '%s' as Datetime. Datetime regular expressions not configured." s) :> System.Exception)

            let mutable (sbDateTime: StringBuilder) = new StringBuilder()

            let stringArray = s.Split (' ')

            for (fm: string) in stringArray do
                if not (String.IsNullOrEmpty (fm)) then
                    sbDateTime.Append (fm + " ") |> ignore
                    ()

            s <- (sbDateTime.ToString ()).Trim ()

            //let dfdf (datetimeRegularExpressions1: string) =
            //    for (regex: string) in datetimeRegularExpressions1.Split (';') do
            //        let parse, d = DateTime.TryParseExact(s, regex, Unchecked.defaultof<_>, Unchecked.defaultof<_>)
            //        match parse with
            //        | true -> Some d
            //        | _ -> None

            //        d
                    
           

            //let res : DateTime option = dfdf (datetimeRegularExpressions)
            //if res.IsSome then
            //    res.Value

                //match DateTime.TryParseExact(s, regex, Unchecked.defaultof<_>, Unchecked.defaultof<_>) with
                //| true, d -> Some d
                //| _ -> None

                
                //if DateTime.TryParseExact (s, regex, Unchecked.defaultof<_>, 0, out dt)
                //then dt

            raise (new InvalidCastException(sprintf "Failed to parse '%s' as Datetime using regular expression(s)" datetimeRegularExpressions))
