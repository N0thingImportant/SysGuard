using System;

public class SysFile
{
    public int ID { get; set; }
    public string Path { get; set; }
    public bool IsRelative { get; set; }

    public string FullPath => IsRelative ? Environment.ExpandEnvironmentVariables(Path) : Path;

    public override string ToString()
    {
        return $"{nameof(SysFile)}[{FullPath}]";
    }
}

public class SusArg
{
    public int ID { get; set; }
    public string Executable { get; set; }
    public string CommandLine { get; set; }
    public bool IsRegex { get; set; }

    public override string ToString()
    {
        if (IsRegex)
            return $"{nameof(SusArg)}[{Executable} /{CommandLine}/]";
        else
            return $"{nameof(SusArg)}[{Executable} *{CommandLine}*]";
    }
}

public class ProcInfo
{
    public int ID { get; set; }
    public string Executable { get; set; }
    public string CommandLine { get; set; }

    public override string ToString()
    {
        return $"{nameof(ProcInfo)}[{Executable} {(CommandLine?.Length > 100 ? CommandLine.Remove(100) + "..." : CommandLine)}]";
    }
}

public enum ActionType
{
    None,
    Start,
    Stop,
    SysFile,
    SusArgs,
}

public class Log
{
    public int ID { get; set; }
    public ProcInfo Process { get; set; }
    public int PID { get; set; }
    public ProcInfo Parent { get; set; }
    public int? ParentPID { get; set; }
    public DateTime DateTime { get; set; }
    public ActionType Action { get; set; }
    public virtual SysFile File { get; set; }

    public virtual SusArg Args { get; set; }

    public override string ToString()
    {
        return $"{nameof(Log)}[{PID}:{Process.Executable} {DateTime} {Action} {(Action == ActionType.SusArgs ? Args.ToString() : Action == ActionType.SysFile ? File.ToString() : "")}";
    }
}
